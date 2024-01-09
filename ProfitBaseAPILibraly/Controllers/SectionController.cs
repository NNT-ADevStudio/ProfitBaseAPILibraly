using Newtonsoft.Json.Linq;
using ProfitBaseAPILibraly.Classes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace ProfitBaseAPILibraly.Controllers
{
    internal class SectionController : MainController
    {
        public SectionController(Auth auth) : base(auth)
        {
        }

        public async Task<List<SectionProfit>> GetSectionByHouse(HouseProfit house)
        {
            List<SectionProfit> items = new List<SectionProfit>();
            Dictionary<string, string> keyValues = new Dictionary<string, string>
            {
                { "houseId", $"{house.Id}" }
            };

            JArray result = await GetResultResponse(
                CreateUrl(keyValues, "house/get-count-floors").ToString()).ConfigureAwait(false);
            if (result == null) return items;

            foreach (var item in result[0]["data"])
                items.Add(new SectionProfit(
                    Convert.ToInt32(item["section_id"], CultureInfo.CurrentCulture),
                    house,
                    Convert.ToString(item["title"], CultureInfo.CurrentCulture)));

            return items;
        }
    }
}
