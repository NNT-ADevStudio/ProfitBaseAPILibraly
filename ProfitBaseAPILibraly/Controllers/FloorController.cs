using Newtonsoft.Json.Linq;
using ProfitBaseAPILibraly.Classes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace ProfitBaseAPILibraly.Controllers
{
    public class FloorController : MainController
    {
        public FloorController(Auth auth) : base(auth) { }

        public async Task<List<FloorProfit>> GetFloors(HouseProfit house)
        {
            List<FloorProfit> items = new List<FloorProfit>();
            Dictionary<string, string> keyValues = new Dictionary<string, string>
            {
                { "houseId", $"{house.Id}" }
            };

            JArray result = await GetResultResponse(
                CreateUrl(keyValues, "house/get-count-properties-on-floor").ToString()).ConfigureAwait(false);
            if (result == null) return null;

            foreach (var item in result[0]["data"])
            {
                FloorProfit temp = new FloorProfit(
                       Convert.ToInt32(item["floor"], CultureInfo.CurrentCulture),
                       Convert.ToInt32(item["section_id"], CultureInfo.CurrentCulture));
                temp.CountApartament = Convert.ToInt32(item["count"], CultureInfo.CurrentCulture);
                items.Add(temp);
            }

            return items;
        }
    }
}
