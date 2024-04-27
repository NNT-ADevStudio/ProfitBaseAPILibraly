using Newtonsoft.Json.Linq;
using ProfitBaseAPILibraly.Classes;
using ProfitBaseAPILibraly.Controllers.AuthControllers.Interfeses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfitBaseAPILibraly.Controllers
{
    public class HouseController : MainController
    {
        public HouseController(IAuth auth) : base(auth) { }

        public async Task<HouseProfit> GetHousesById(int id)
        {
            Dictionary<string, string> keyValues = new Dictionary<string, string>
            {
                { "id", $"{id}" }
            };

            JArray result = await GetResultResponse(
                CreateUrl(keyValues, "house").ToString()).ConfigureAwait(false);

            if (result == null) return null;

            HouseProfit house = result[0]["data"][0].ToObject<HouseProfit>();

            return house;
        }

        public async Task<List<HouseProfit>> GetHousesAll()
        {
            JArray result = await GetResultResponse(
                CreateUrl(null, "house").ToString()).ConfigureAwait(false);

            return ProssesingHouse(result);
        }


        public async Task<List<HouseProfit>> GetHousesByProject(ProjectProfit project)
        {
            if (project == null) return null;

            Dictionary<string, string> keyValues = new Dictionary<string, string>
            {
                { "projectId", $"{project.Id}" }
            };

            JArray result = await GetResultResponse(
                CreateUrl(keyValues, "house").ToString()).ConfigureAwait(false);

            return ProssesingHouse(result);
        }

        public async Task<List<HouseProfit>> GetHousesByProjectId(int projectId)
        {
            Dictionary<string, string> keyValues = new Dictionary<string, string>
            {
                { "projectId", $"{projectId}" }
            };

            JArray result = await GetResultResponse(
                CreateUrl(keyValues, "house").ToString()).ConfigureAwait(false);

            return ProssesingHouse(result);
        }

        private static List<HouseProfit> ProssesingHouse(JArray result)
        {
            if (result == null) return null;

            List<HouseProfit> items = result[0]["data"].ToObject<List<HouseProfit>>();

            return items;
        }
    }
}
