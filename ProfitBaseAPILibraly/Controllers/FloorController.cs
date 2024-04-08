using Newtonsoft.Json.Linq;
using ProfitBaseAPILibraly.Classes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ProfitBaseAPILibraly.Controllers
{
    public class FloorController : MainController
    {
        public FloorController(Auth auth) : base(auth) { }

        public async Task<List<FloorProfit>> GetFloors(HouseProfit house)
        {
            List<FloorProfit> items = new List<FloorProfit>();

            for (int i = house.MinFloor; i <= house.MaxFloor; i++)
            {
                List<FloorProfit> tempFloors = new List<FloorProfit>();

                Dictionary<string, string> keyValues = new Dictionary<string, string>
                {
                    { "houseId", $"{house.Id}" },
                    { "floor", $"{i}" },
                };

                JArray result = await GetResultResponse(
                    CreateUrl(keyValues, "house/get-count-properties-on-floor").ToString()).ConfigureAwait(false);

                if (result == null) continue;

                tempFloors = result[0]["data"].ToObject<List<FloorProfit>>();

                if (house.Sections == null || house.Sections.Count == 0) continue;

                tempFloors.ForEach(x =>
                {
                    var section = house.Sections.Find(y => y.Id == x.SectionId);

                    if (section == null) return;

                    x.Section = section;
                    section.Floors.Add(x);
                });

                items.AddRange(tempFloors);
            }

            return items;
        }
    }
}
