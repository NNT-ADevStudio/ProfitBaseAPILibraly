using Newtonsoft.Json.Linq;
using ProfitBaseAPILibraly.Classes;
using ProfitBaseAPILibraly.Controllers.AuthControllers.Interfeses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfitBaseAPILibraly.Controllers
{
    public class ProjectController : MainController
    {
        public ProjectController(IAuth auth) : base(auth) { }

        public async Task<List<ProjectProfit>> GetProjectAll(bool isArchive = false)
        {
            Dictionary<string, string> keyValues = new Dictionary<string, string>
            {
                { "isArchive", $"{isArchive}" }
            };

            JArray result = await GetResultResponse(
                CreateUrl(keyValues, "projects").ToString()).ConfigureAwait(false);

            if (result == null) return null;

            List<ProjectProfit> projects = result.ToObject<List<ProjectProfit>>();

            return projects;
        }
    }
}
