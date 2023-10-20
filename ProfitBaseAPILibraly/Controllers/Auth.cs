using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProfitBaseAPILibraly.Controllers
{
    public class Auth
    {
        private const string mediaType = "application/json";

        internal string BaseUrl { get; }
        string ApiKey { get; }
        string Access_token { get; set; }
        public string Subdomain { get; set; }

        string AppData { get; } = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        public Auth(string apiKey, string subdomain)
        {
            ApiKey = apiKey;
            BaseUrl = "https://" + subdomain + ".profitbase.ru/api/v4/json/";
            Subdomain = subdomain;
        }

        public async Task<bool> RefreshAccessToken()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                StringBuilder stringBuilder = new StringBuilder();

                stringBuilder.Append("{\"type\": \"api-app\",\"credentials\": {");
                stringBuilder.Append($"\"pb_api_key\": \"{ApiKey}\"");
                stringBuilder.Append("}}");

                using (StringContent data = new StringContent(stringBuilder.ToString(),
                                             null,
                                             mediaType))
                {
                    using (var response = await httpClient.PostAsync($"{BaseUrl}authentication", data).ConfigureAwait(false))
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        JObject jsonObject = JObject.Parse(result);

                        Access_token = jsonObject["access_token"].ToString();
                        SetTokens(Access_token);
                    }
                }
            }

            return true;
        }

        public string GetAccessToken()
        {
            string json = File.ReadAllText($"{AppData}\\{Subdomain}.json");
            JObject jsonObject = JObject.Parse(json);
            return jsonObject["access_token"].ToString();
        }

        public void SetTokens(string access)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append('{');
            stringBuilder.Append($"\"access_token\": \"{access}\"");
            stringBuilder.Append('}');

            string json = stringBuilder.ToString();

            File.WriteAllText($"{AppData}\\{Subdomain}.json", json);
        }
    }
}
