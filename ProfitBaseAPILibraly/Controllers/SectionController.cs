using Newtonsoft.Json.Linq;
using ProfitBaseAPILibraly.Classes;
using ProfitBaseAPILibraly.Controllers.AuthControllers.Interfeses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfitBaseAPILibraly.Controllers
{
    public class SectionController : MainController
    {
        public SectionController(IAuth auth) : base(auth) { }

        /// <summary>
        /// Получает список объектов SectionProfit по объекту HouseProfit.
        /// </summary>
        /// <param name="house">Объект HouseProfit.</param>
        /// <returns>Список объектов SectionProfit.</returns>
        public async Task<List<SectionProfit>> GetSectionByHouse(HouseProfit house)
        {
            Dictionary<string, string> keyValues = new Dictionary<string, string>
            {
                { "houseId", $"{house.Id}" }
            };

            JArray result = await GetResultResponse(
                CreateUrl(keyValues, "house/get-count-floors").ToString()).ConfigureAwait(false);
            if (result == null) return null;

            List<SectionProfit> items = result[0]["data"].ToObject<List<SectionProfit>>();

            items.ForEach(x => { x.House = house; x.HouseId = house.Id; });
            house.Sections = items;

            return items;
        }

        public async Task<List<SectionProfit>> GetSectionByHouse(int houseId)
        {
            Dictionary<string, string> keyValues = new Dictionary<string, string>
            {
                { "houseId", $"{houseId}" }
            };

            JArray result = await GetResultResponse(
                CreateUrl(keyValues, "house/get-count-floors").ToString()).ConfigureAwait(false);
            if (result == null) return null;

            List<SectionProfit> items = result[0]["data"].ToObject<List<SectionProfit>>();

            items.ForEach(x => { x.HouseId = houseId; });

            return items;
        }
    }
}
