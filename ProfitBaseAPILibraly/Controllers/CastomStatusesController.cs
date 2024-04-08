using Newtonsoft.Json.Linq;
using ProfitBaseAPILibraly.Classes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfitBaseAPILibraly.Controllers
{
    public class CastomStatusesController : MainController
    {
        public CastomStatusesController(Auth auth) : base(auth) { }

        public async Task<List<CastomStatus>> Get(string crm, int? id = null)
        {
            Dictionary<string, string> keyValues = new Dictionary<string, string>
            {
                { "crm", $"{crm}" }
            };

            if(id != null) keyValues.Add("id", $"{id}");

            JArray result = await GetResultResponse(
                CreateUrl(keyValues, "custom-status/list").ToString()).ConfigureAwait(false);

            if (result == null) return null;

            if (result[0]["data"]["customStatuses"] == null) return null;

            var temp = result[0]["data"]["customStatuses"];

            var er = temp[0];

            List<CastomStatus> collection = temp.ToObject<List<CastomStatus>>();

            return collection;
        }
    }
}
