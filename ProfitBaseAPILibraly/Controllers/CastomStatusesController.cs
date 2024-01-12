using Newtonsoft.Json.Linq;
using ProfitBaseAPILibraly.Classes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace ProfitBaseAPILibraly.Controllers
{
    public class CastomStatusesController : MainController
    {
        public CastomStatusesController(Auth auth) : base(auth) { }

        public async Task<List<CastomStatus>> GetAll(string crm)
        {
            Dictionary<string, string> keyValues = new Dictionary<string, string>
            {
                { "crm", $"{crm}" }
            };

            JArray result = await GetResultResponse(
                CreateUrl(keyValues, "custom-status/list").ToString()).ConfigureAwait(false);

            List<CastomStatus> collection = new List<CastomStatus>();

            if (result == null) return null;

            foreach (var item in result[0]["data"]["customStatuses"])
            {
                CastomStatus castomStatus = new CastomStatus(
                    Convert.ToInt32(item["id"], CultureInfo.CurrentCulture),
                    Convert.ToString(item["name"], CultureInfo.CurrentCulture),
                    Convert.ToString(item["baseStatus"], CultureInfo.CurrentCulture));

                collection.Add(castomStatus);
            }

            return collection;
        }

        public async Task<CastomStatus> GetById(string crm, int id)
        {
            Dictionary<string, string> keyValues = new Dictionary<string, string>
            {
                { "crm", $"{crm}" },
                { "id", $"{id}" }
            };

            JArray result = await GetResultResponse(
                CreateUrl(keyValues, "custom-status/list").ToString()).ConfigureAwait(false);

            if (result == null) return null;

            CastomStatus castomStatus = new CastomStatus(
                Convert.ToInt32(result[0]["data"]["customStatuses"]["id"], CultureInfo.CurrentCulture),
                Convert.ToString(result[0]["data"]["customStatuses"]["name"], CultureInfo.CurrentCulture),
                Convert.ToString(result[0]["data"]["customStatuses"]["baseStatus"], CultureInfo.CurrentCulture));

            return castomStatus;
        }
    }
}
