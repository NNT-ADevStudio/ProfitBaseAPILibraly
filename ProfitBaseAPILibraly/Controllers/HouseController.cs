using LivingComplexLib.Models;
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
                { "projectId", $"{id}" }
            };

            JArray result = await GetResultResponse(
                CreateUrl(keyValues, "house").ToString()).ConfigureAwait(false);

            if (result == null) return null;

            HouseProfit house = new HouseProfit(
                Convert.ToInt32(result[0]["data"][0]["id"], CultureInfo.CurrentCulture),
                Convert.ToInt32(result[0]["data"][0]["projectId"], CultureInfo.CurrentCulture));
            return house;
        }

        public async Task<List<HouseProfit>> GetHousesAll()
        {
            List<HouseProfit> items = new List<HouseProfit>();

            JArray result = await GetResultResponse(
                CreateUrl(null, "house").ToString()).ConfigureAwait(false);

            if (result == null) return items;

            foreach (var item in result[0]["data"])
                items.Add(new HouseProfit(
                    Convert.ToInt32(item["id"], CultureInfo.CurrentCulture),
                    Convert.ToInt32(item["projectId"], CultureInfo.CurrentCulture)));

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

            return items;
        }
    }
}
