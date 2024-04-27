using Newtonsoft.Json.Linq;
using ProfitBaseAPILibraly.Classes;
using ProfitBaseAPILibraly.Controllers.AuthControllers.Interfeses;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfitBaseAPILibraly.Controllers
{
    public class ProjectController : MainController
    {
        public ProjectController(IAuth auth) : base(auth) { }

        public async Task<List<ProjectProfit>> GetProjectAll(bool isArchive = false)
        {
            List<ProjectProfit> projects = new List<ProjectProfit>();
            Dictionary<string, string> keyValues = new Dictionary<string, string>
            {
                { "isArchive", $"{isArchive}" }
            };

            IJEnumerable<JToken> result = await GetResultResponse(
                CreateUrl(keyValues, "projects").ToString()).ConfigureAwait(false);

            if (result == null) return null;

            projects = result.Select(item => item.ToObject<ProjectProfit>()).ToList();

            return projects;
        }
    }
}
