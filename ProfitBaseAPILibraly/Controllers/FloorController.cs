using Newtonsoft.Json.Linq;
using ProfitBaseAPILibraly.Classes;
using ProfitBaseAPILibraly.Controllers.AuthControllers.Interfeses;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfitBaseAPILibraly.Controllers
{
    public class FloorController : MainController
    {
        public FloorController(IAuth auth) : base(auth) { }

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

                tempFloors = result[0]["data"].Select(item => item.ToObject<FloorProfit>()).ToList();

                if (house.Sections == null || house.Sections.Count == 0) continue;

                foreach (var floor in tempFloors)
                {
                    var section = house.Sections.Find(x => x.Id == floor.SectionId);

                    if (section == null) continue;

                    floor.Section = section;
                    section.Floors.Add(floor);
                }

                items.AddRange(tempFloors);
            }

            return items;
        }
    }
}
