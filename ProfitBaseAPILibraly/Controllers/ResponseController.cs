using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProfitBaseAPILibraly.Controllers
{
    internal static class ResponseController
    {
        public static async Task<JArray> GetResultResponse(string url)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(url).ConfigureAwait(false))
                {
                    string jsonString = await response.Content.
                        ReadAsStringAsync().ConfigureAwait(false);

                    if (!response.IsSuccessStatusCode)
                    {
                        Debug.WriteLine(jsonString);
                        return null;
                    }

                    try
                    {
                        return JsonConvert.DeserializeObject<JArray>(jsonString);
                    }
                    catch (Exception)
                    {
                        string srt = "[" + jsonString + "]";
                        return JsonConvert.DeserializeObject<JArray>(srt);
                    }
                }
            }
        }

        public static async Task<JObject> GetResultResponse(string url, HttpClient httpClient)
        {
            using (var response = await httpClient.GetAsync(url).ConfigureAwait(false))
            {
                string jsonString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(jsonString);
                    return null;
                }

                return JObject.Parse(jsonString);
            }
        }
    }
}
