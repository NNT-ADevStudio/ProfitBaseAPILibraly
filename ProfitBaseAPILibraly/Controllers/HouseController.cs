using Newtonsoft.Json.Linq;
using ProfitBaseAPILibraly.Classes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace ProfitBaseAPILibraly.Controllers
{
    public class HouseController : MainController
    {
        public HouseController(Auth auth) : base(auth) { }

        public async Task<HouseProfit> GetHousesById(int id)
        {
            Dictionary<string, string> keyValues = new Dictionary<string, string>
            {
                { "id", $"{id}" }
            };

            JArray result = await GetResultResponse(
                CreateUrl(keyValues, "house").ToString()).ConfigureAwait(false);

            if (result == null) return null;

            HouseProfit house = new HouseProfit(
                Convert.ToInt32(result[0]["data"][0]["id"], CultureInfo.CurrentCulture),
                Convert.ToInt32(result[0]["data"][0]["projectId"], CultureInfo.CurrentCulture),
                Convert.ToString(result[0]["data"][0]["title"], CultureInfo.CurrentCulture));
            return house;
        }

        public async Task<List<HouseProfit>> GetHousesAll()
        {
            List<HouseProfit> items = new List<HouseProfit>();

            JArray result = await GetResultResponse(
                CreateUrl(string.Empty, "house").ToString()).ConfigureAwait(false);

            if (result == null) return items;

            foreach (var item in result[0]["data"])
                items.Add(new HouseProfit(
                    Convert.ToInt32(item["id"], CultureInfo.CurrentCulture),
                    Convert.ToInt32(item["projectId"], CultureInfo.CurrentCulture),
                    Convert.ToString(item["title"], CultureInfo.CurrentCulture)));

            return items;
        }


        public async Task<List<HouseProfit>> GetHousesByProject(ProjectProfit project)
        {
            if (project == null) return null;

            List<HouseProfit> items = new List<HouseProfit>();
            Dictionary<string, string> keyValues = new Dictionary<string, string>
            {
                { "projectId", $"{project.Id}" }
            };

            JArray result = await GetResultResponse(
                CreateUrl(keyValues, "house").ToString()).ConfigureAwait(false);

            if (result == null) return items;

            foreach (var item in result[0]["data"])
                items.Add(new HouseProfit(Convert.ToInt32(item["id"], CultureInfo.CurrentCulture), project));

            project.HouseList = items;
            return items;
        }

        public async Task<List<HouseProfit>> GetHousesByProjectId(int projectId)
        {
            List<HouseProfit> items = new List<HouseProfit>();
            Dictionary<string, string> keyValues = new Dictionary<string, string>
            {
                { "projectId", $"{projectId}" }
            };

            JArray result = await GetResultResponse(
                CreateUrl(keyValues, "house").ToString()).ConfigureAwait(false);

            if (result == null) return items;

            foreach (var item in result[0]["data"])
                items.Add(new HouseProfit(Convert.ToInt32(item["id"], CultureInfo.CurrentCulture), projectId));

            return items;
        }
    }
}
