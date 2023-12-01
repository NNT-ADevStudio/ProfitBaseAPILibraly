using Newtonsoft.Json.Linq;
using ProfitBaseAPILibraly.Classes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfitBaseAPILibraly.Controllers
{
    public class ProfitBaseAPI
    {
        /// <summary>
        /// Кастомные статусы
        /// </summary>
        private ICollection<CastomStatus> CastomStatuses { get; set; }

        /// <summary>
        /// Класс аутентификации
        /// </summary>
        public Auth Auth { get; }

        /// <summary>
        /// Создает новый экземпляр класса ProfitBaseAPI
        /// </summary>
        /// <param name="auth">Класс аутентификации</param>
        public ProfitBaseAPI(Auth auth) => Auth = auth;

        private Uri CreateUrl(Dictionary<string, string> keyValues, string endPoint)
        {
            StringBuilder temp = new StringBuilder();

            if (keyValues != null)
                foreach (KeyValuePair<string, string> item in keyValues)
                    temp.Append($"&{item.Key}={item.Value}");

            var url = new UriBuilder($"{Auth.BaseUrl}{endPoint}")
            {
                Query = $"access_token={Auth.AccessToken}" + temp.ToString()
            };

            return url.Uri;
        }

        public async Task<List<ProjectProfit>> GetProject()
        {
            List<ProjectProfit> projects = new List<ProjectProfit>();
            Dictionary<string, string> keyValues = new Dictionary<string, string>
            {
                { "isArchive", "false" }
            };

            IJEnumerable<JToken> result = await ResponseController.GetResultResponse(
                CreateUrl(keyValues, "projects").ToString()).ConfigureAwait(false);

            if (result == null) return null;

            foreach (var item in result)
            {
                var temp = new ProjectProfit(
                    Convert.ToInt32(item["id"], CultureInfo.CurrentCulture),
                    Convert.ToString(item["title"], CultureInfo.CurrentCulture),
                    Auth.Subdomain);
                temp.Locality = Convert.ToString(item["locality"], CultureInfo.CurrentCulture);
                projects.Add(temp);
            }

            return projects;
        }

        public async Task<List<HouseProfit>> GetHouses(ProjectProfit project)
        {
            if (project == null) return null;

            List<HouseProfit> items = new List<HouseProfit>();
            Dictionary<string, string> keyValues = new Dictionary<string, string>
            {
                { "projectId", $"{project.Id}" }
            };

            JArray result = await ResponseController.GetResultResponse(
                CreateUrl(keyValues, "house").ToString()).ConfigureAwait(false);

            if (result == null) return items;

            foreach (var item in result[0]["data"])
                items.Add(new HouseProfit(
                    Convert.ToInt32(item["id"], CultureInfo.CurrentCulture),
                    Convert.ToString(item["title"], CultureInfo.CurrentCulture), project));

            return items;
        }

        public async Task<List<SectionProfit>> GetSection(HouseProfit house)
        {
            if (house == null) return null;

            List<SectionProfit> items = new List<SectionProfit>();
            Dictionary<string, string> keyValues = new Dictionary<string, string>
            {
                { "houseId", $"{house.Id}" }
            };

            JArray result = await ResponseController.GetResultResponse(
                CreateUrl(keyValues, "house/get-count-floors").ToString()).ConfigureAwait(false);
            if (result == null) return items;

            foreach (var item in result[0]["data"])
                items.Add(new SectionProfit(
                    Convert.ToInt32(item["section_id"], CultureInfo.CurrentCulture),
                    Convert.ToString(item["title"], CultureInfo.CurrentCulture),
                    Convert.ToInt32(item["count"], CultureInfo.CurrentCulture),
                    house));

            return items;
        }

        public async Task<List<ApartamentProfit>> GetApartaments(FloorProfit floor)
        {
            if (floor == null) return null;

            List<ApartamentProfit> items = new List<ApartamentProfit>();
            Dictionary<string, string> keyValues = new Dictionary<string, string>
            {
                { "houseId", $"{((HouseProfit)floor.Section.House).Id}" },
                { "isArchive", $"false" },
                { "full", $"true" },
                { "minFloor", $"{floor.Number}" },
                { "maxFloor", $"{floor.Number}" },
                { "section[]", $"{floor.Section.Title}" },
            };

            JArray result = await ResponseController.GetResultResponse(
                CreateUrl(keyValues, "property").ToString()).ConfigureAwait(false);

            if (result == null) return items;

            foreach (var item in result[0]["data"])
            {
                if (item == null) continue;
                ApartamentProfit temp = await GetInfoApartament(item).ConfigureAwait(false);
                temp.Floor = floor;
                items.Add(temp);
            }

            return items;
        }

        public async Task<ApartamentProfit> GetApartamentById(int id)
        {
            Dictionary<string, string> keyValues = new Dictionary<string, string>
            {
                { "id", $"{id}" },
                { "full", "true" }
            };

            JArray result = await ResponseController.GetResultResponse(
                CreateUrl(keyValues, "property").ToString()).ConfigureAwait(false);

            return await GetInfoApartament(result[0]["data"][0]).ConfigureAwait(false);
        }

        public async Task<ApartamentProfit> GetInfoApartament(JToken result)
        {
            if (result == null) return null;
            JObject data = result.ToObject<JObject>();
            List<CastomProperty> tempCastomProperty = new List<CastomProperty>();
            ApartamentProfit temp = new ApartamentProfit();

            foreach (var field in data["custom_fields"])
            {
                tempCastomProperty.Add(
                new CastomProperty(
                    Convert.ToString(field["id"], CultureInfo.CurrentCulture),
                    Convert.ToString(field["name"], CultureInfo.CurrentCulture),
                    Convert.ToString(field["value"], CultureInfo.CurrentCulture),
                    GetVariableType(field["value"].Type)));
            }

            if (result["status"] != null)
                if (result["status"].Type != JTokenType.Null)
                    temp.ProfitStatus = Convert.ToString(result["status"], CultureInfo.CurrentCulture);

            temp.TotalArea = Convert.ToDouble(result["area"]["area_total"], CultureInfo.CurrentCulture);
            temp.Price = Convert.ToDouble(result["price"]["value"], CultureInfo.CurrentCulture);
            temp.CustomProperties = tempCastomProperty;
            temp.Id = Convert.ToInt32(result["id"], CultureInfo.CurrentCulture);
            temp.Kod = Convert.ToString(tempCastomProperty.FirstOrDefault(p => p.Name == "Код планировки").GetValue(), CultureInfo.CurrentCulture);
            temp.Number = Convert.ToString(tempCastomProperty.FirstOrDefault(p => p.Name == "Номер помещения").GetValue(), CultureInfo.CurrentCulture);

            if (result["bookedAt"] != null)
                if (result["bookedAt"].Type != JTokenType.Null)
                    temp.BookedAt = result["bookedAt"].ToString();

            if (CastomStatuses == null)
                await GetCastomStatus().ConfigureAwait(false);

            temp.Status = CastomStatuses.FirstOrDefault(
                p => p.Id == Convert.ToInt32(result["customStatusId"], CultureInfo.CurrentCulture));

            return temp;
        }

        public async Task<ICollection<CastomStatus>> GetCastomStatus()
        {
            ICollection<CastomStatus> collection = new List<CastomStatus>();
            Dictionary<string, string> keyValues = new Dictionary<string, string>
            {
                { "crm", $"amo" },
            };

            JArray result = await ResponseController.GetResultResponse(
                CreateUrl(keyValues, "custom-status/list").ToString()).ConfigureAwait(false);

            foreach (var item in result[0]["data"]["customStatuses"])
                collection.Add(new CastomStatus(
                    Convert.ToInt32(item["id"], CultureInfo.CurrentCulture),
                    Convert.ToString(item["name"], CultureInfo.CurrentCulture)));
            CastomStatuses = collection;
            return collection;
        }

        private static Type GetVariableType(JTokenType tokenType)
        {
            switch (tokenType)
            {
                case JTokenType.String:
                    return typeof(string);
                case JTokenType.Integer:
                    return typeof(int);
                case JTokenType.Float:
                    return typeof(float);
                case JTokenType.Boolean:
                    return typeof(bool);
                case JTokenType.Null:
                    return null;
                case JTokenType.Array:
                    return typeof(Array);
                default:
                    throw new ArgumentException($"Неподдерживаемый JTokenType: {tokenType}");
            }
        }
    }
}
