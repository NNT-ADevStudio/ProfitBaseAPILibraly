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

        public async Task<List<CastomStatus>> GetAll(string crm, int? id = null)
        {
            Dictionary<string, string> keyValues = new Dictionary<string, string>
            {
                { "crm", $"{crm}" }
            };

            if(id != null) keyValues.Add("id", $"{id}");

            JArray result = await GetResultResponse(
                CreateUrl(keyValues, "custom-status/list").ToString()).ConfigureAwait(false);

            List<CastomStatus> collection = new List<CastomStatus>();

            if (result == null) return null;

            if (result[0]["data"]["customStatuses"] == null) return null;

            //var temp = result[0]["data"]["customStatuses"];

            //var options = new JsonSerializerOptions
            //{
            //    PropertyNameCaseInsensitive = true
            //};

            //collection = JsonSerializer.Deserialize<List<CastomStatus>>(temp.ToString(), options);

            foreach (var item in result[0]["data"]["customStatuses"])
            {
                CastomStatus castomStatus = new CastomStatus(
                    Convert.ToInt32(item["id"], CultureInfo.CurrentCulture),
                    Convert.ToString(item["name"], CultureInfo.CurrentCulture),
                    Convert.ToString(item["baseStatus"], CultureInfo.CurrentCulture),
                    Convert.ToBoolean(item["isProtected"], CultureInfo.CurrentCulture),
                    Convert.ToString(item["alias"], CultureInfo.CurrentCulture));

                collection.Add(castomStatus);
            }

            return collection;
        }
    }
}
