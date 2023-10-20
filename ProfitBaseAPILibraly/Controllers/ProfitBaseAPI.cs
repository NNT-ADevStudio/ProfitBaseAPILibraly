using LivingComplexLib.Models;
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
        ICollection<CastomStatus> CastomStatuses { get; set; }

        public Auth Auth { get; }

        public ProfitBaseAPI(Auth auth)
        {
            Auth = auth;
        }

        public Uri CreateUrl(Dictionary<string, string> keyValues, string endPoint)
        {
            StringBuilder temp = new StringBuilder();

            if (keyValues != null)
                foreach (KeyValuePair<string, string> item in keyValues)
                    temp.Append($"&{item.Key}={item.Value}");

            var url = new UriBuilder($"{Auth.BaseUrl}{endPoint}")
            {
                Query = $"access_token={Auth.GetAccessToken()}" + temp.ToString()
            };

            return url.Uri;
        }

        public async Task<List<Project>> GetProject()
        {
            List<Project> projects = new List<Project>();
            Dictionary<string, string> keyValues = new Dictionary<string, string>
            {
                { "isArchive", "false" }
            };

            IJEnumerable<JToken> result = await ResponseController.GetResultResponse(CreateUrl(keyValues, "projects").ToString()).ConfigureAwait(false);

            if (result == null) return null;

            foreach (var item in result)
                projects.Add(new ProjectProfit(
                    Convert.ToInt32(item["id"], CultureInfo.CurrentCulture),
                    Convert.ToString(item["title"], CultureInfo.CurrentCulture),
                    Auth.Subdomain));

            return projects;
        }

        public async Task<List<House>> GetHouses(ProjectProfit project)
        {
            if (project == null) return null;

            List<House> items = new List<House>();
            Dictionary<string, string> keyValues = new Dictionary<string, string>
            {
                { "projectId", $"{project.Id}" }
            };

            JArray result = await ResponseController.GetResultResponse(CreateUrl(keyValues, "house").ToString()).ConfigureAwait(false);

            if (result == null) return items;

            foreach (var item in result[0]["data"])
                items.Add(new HouseProfit(Convert.ToInt32(item["id"], CultureInfo.CurrentCulture), Convert.ToString(item["title"], CultureInfo.CurrentCulture), project));

            return items;
        }

        public async Task<List<Section>> GetSection(HouseProfit house)
        {
            if (house == null) return null;

            List<Section> items = new List<Section>();
            Dictionary<string, string> keyValues = new Dictionary<string, string>
            {
                { "houseId", $"{house.Id}" }
            };

            JArray result = await ResponseController.GetResultResponse(CreateUrl(keyValues, "house/get-count-floors").ToString()).ConfigureAwait(false);
            if (result == null) return items;

            foreach (var item in result[0]["data"])
                items.Add(new SectionProfit(
                    Convert.ToInt32(item["section_id"], CultureInfo.CurrentCulture),
                    Convert.ToString(item["title"], CultureInfo.CurrentCulture),
                    Convert.ToInt32(item["count"], CultureInfo.CurrentCulture),
                    house));

            return items;
        }

        public async Task<List<Apatrament>> GetApartaments(Floor floor)
        {
            if (floor == null) return null;

            List<Apatrament> items = new List<Apatrament>();
            Dictionary<string, string> keyValues = new Dictionary<string, string>
                {
                    { "houseId", $"{((HouseProfit)floor.Section.House).Id}" },
                    { "isArchive", $"false" },
                    { "full", $"true" },
                    { "minFloor", $"{floor.Number}" },
                    { "maxFloor", $"{floor.Number}" },
                    { "section[]", $"{floor.Section.Title}" },
                };

            IJEnumerable<JToken> result = await ResponseController.GetResultResponse(CreateUrl(keyValues, "property").ToString()).ConfigureAwait(false);

            if (result == null) return items;

            foreach (var item in result[0]["data"])
            {
                if (item == null) continue;

                ApartamentProfit temp = new ApartamentProfit();

                foreach (var field in item["custom_fields"])
                {
                    switch (Convert.ToString(field["name"], CultureInfo.CurrentCulture))
                    {
                        case "Код планировки":
                            temp.Kod = Convert.ToString(field["value"], CultureInfo.CurrentCulture);
                            break;
                        case "Полная цена":
                            temp.Price = Convert.ToDouble(field["value"], CultureInfo.CurrentCulture);
                            break;
                        case "Кол-во комнат":
                            temp.CountRoom = Convert.ToInt32(field["value"], CultureInfo.CurrentCulture);
                            break;
                        case "Номер помещения":
                            temp.Number = Convert.ToString(field["value"], CultureInfo.CurrentCulture);
                            break;
                        case "Площадь, м2":
                            try
                            {
                                temp.TotalArea = Convert.ToDouble(field["value"], CultureInfo.CurrentCulture);
                            }
                            catch (Exception)
                            {
                                temp.TotalArea = 0;
                            }
                            break;
                        case "Цена за ремонт":
                            try
                            {
                                temp.RenovationPrice = Convert.ToDouble(field["value"], CultureInfo.CurrentCulture);
                            }
                            catch
                            {
                                temp.RenovationPrice = 0;
                            }
                            break;
                        case "Площадь лоджии":
                            temp.SummerRoom = Convert.ToString(field["value"], CultureInfo.CurrentCulture);
                            break;
                    }
                }
                temp.Floor = floor;
                temp.Id = Convert.ToInt32(item["id"], CultureInfo.CurrentCulture);
                temp.Status = CastomStatuses.FirstOrDefault(p => p.Id == Convert.ToInt32(item["customStatusId"], CultureInfo.CurrentCulture));
                items.Add(temp);
            }

            return items;
        }

        public async Task<ICollection<CastomStatus>> GetCastomStatus()
        {
            ICollection<CastomStatus> collection = new List<CastomStatus>();
            Dictionary<string, string> keyValues = new Dictionary<string, string>
            {
                    { "crm", $"amo" },
            };

            JArray result = await ResponseController.GetResultResponse(CreateUrl(keyValues, "custom-status/list").ToString()).ConfigureAwait(false);

            foreach (var item in result[0]["data"]["customStatuses"])
            {
                collection.Add(new CastomStatus(Convert.ToInt32(item["id"], CultureInfo.CurrentCulture), Convert.ToString(item["name"], CultureInfo.CurrentCulture)));
            }
            CastomStatuses = collection;
            return collection;
        }
    }
}
