using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProfitBaseAPILibraly.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfitBaseAPILibraly.Controllers
{
    public class ApartamentController : MainController
    {
        private CastomStatusesController CastomStatusesController { get; set; }

        private ICollection<CastomStatus> CastomStatuses { get; set; }

        public ApartamentController(Auth auth) : base(auth)
        {
            CastomStatusesController = new CastomStatusesController(auth);
        }

        public async Task<ApartamentProfit> GetApartamentById(int id)
        {
            Dictionary<string, string> keyValues = new Dictionary<string, string>
            {
                { "id", $"{id}" },
                { "full", "true" }
            };

            JArray result = await GetResultResponse(
                CreateUrl(keyValues, "property").ToString()).ConfigureAwait(false);

            var temp = result[0]["data"][0].ToObject<ApartamentProfit>();
            temp.SummerRoom = (string)temp.CustomProperties.FirstOrDefault(p => p.Name == "Площадь лоджии, м2").GetValue();

            if (CastomStatuses == null)
                await GetCastomStatus().ConfigureAwait(false);

            temp.Status = CastomStatuses.FirstOrDefault(p => p.Id == temp.CustomStatusId);

            return temp;
        }

        public async Task<List<ApartamentProfit>> GetApartamentsByFloor(FloorProfit floor)
        {
            try
            {
                if (floor == null) return null;

                List<ApartamentProfit> items = new List<ApartamentProfit>();

                for (int i = 0; i < floor.CountApartament; i += 100)
                {
                    items = new List<ApartamentProfit>();
                    Dictionary<string, string> keyValues = new Dictionary<string, string>
                    {
                        { "houseId", $"{floor.Section.House.Id}" },
                        { "isArchive", $"false" },
                        { "full", $"true" },
                        { "minFloor", $"{floor.Number}" },
                        { "maxFloor", $"{floor.Number}" },
                        { "section[]", $"{floor.Section.Title}" },
                        { "offset", $"{i}" },
                    };

                    JArray result = await GetResultResponse(
                        CreateUrl(keyValues, "property").ToString()).ConfigureAwait(false);

                    if (result == null) return items;

                    items = result[0]["data"].ToObject<List<ApartamentProfit>>();

                    if (CastomStatuses == null)
                        await GetCastomStatus().ConfigureAwait(false);

                    items.ForEach(x =>
                    {
                        x.Floor = floor;
                        x.FloorId = floor.Id;
                        x.SummerRoom = (string)x.CustomProperties.FirstOrDefault(p => p.Name == "Площадь лоджии, м2").GetValue();
                        x.Status = CastomStatuses.FirstOrDefault(p => p.Id == x.CustomStatusId);
                    });

                    //foreach (var item in result[0]["data"])
                    //{
                    //    if (item == null) continue;
                    //    ApartamentProfit temp = await ProssesingApartament(item).ConfigureAwait(false);
                    //    temp.Floor = floor;
                    //    temp.FloorId = floor.Id;
                    //    items.Add(temp);
                    //}
                }

                floor.Apartaments = items;

                return items;
            }
            catch (Exception ex)
            {
                throw new CustomException(
                    $"[GetApartamentsByFloor]" +
                    $"\nЭтаж: {floor.Id}" +
                    $"\nСекция: {floor.SectionId}" +
                    $"\nДом: {floor.Section.House.Id}" +
                    $"\n\n{ex.Message}" +
                    $"\n{ex.StackTrace}");
            }
        }

        private async Task<ApartamentProfit> ProssesingApartament(JToken result)
        {
            if (result == null) return null;

            JObject data = result.ToObject<JObject>();

            if (data == null) return null;

            ApartamentProfit temp = new ApartamentProfit();

            temp = data.ToObject<ApartamentProfit>();

            if (temp == null) return null;

            temp.SummerRoom = (string)temp.CustomProperties.FirstOrDefault(p => p.Name == "Площадь лоджии, м2").GetValue();

            if (CastomStatuses == null)
                await GetCastomStatus().ConfigureAwait(false);

            temp.Status = CastomStatuses.FirstOrDefault(p => p.Id == temp.CustomStatusId);

            return temp;
        }

        public async Task<bool> Change<T>(int id, T keyValuesBody = default(T), Dictionary<string, string> keyValuesUrl = null)
        {
            string url = CreateUrl(keyValuesUrl, $"properties/{id}").ToString();

            string json = JsonConvert.SerializeObject(keyValuesBody);

            var result = await PatchAsync(url, json).ConfigureAwait(false);

            return result;
        }

        public async Task<ICollection<CastomStatus>> GetCastomStatus()
        {
            ICollection<CastomStatus> collection = await CastomStatusesController.Get("amo");

            CastomStatuses = collection;

            return collection;
        }
    }
}
