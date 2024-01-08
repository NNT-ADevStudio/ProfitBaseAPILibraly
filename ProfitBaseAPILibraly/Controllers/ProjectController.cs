using Newtonsoft.Json.Linq;
using ProfitBaseAPILibraly.Classes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace ProfitBaseAPILibraly.Controllers
{
    public class ProjectController : MainController
    {
        public ProjectController(Auth auth) : base(auth)
        {
        }

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

            foreach (var item in result)
    {
                var temp = new ProjectProfit(
                    Convert.ToInt32(item["id"], CultureInfo.CurrentCulture),
                    Auth.Subdomain);
                temp.Locality = Convert.ToString(item["locality"], CultureInfo.CurrentCulture);
                temp.Title = Convert.ToString(item["title"], CultureInfo.CurrentCulture);
                projects.Add(temp);
            }

            return projects;
        }
    }
}
