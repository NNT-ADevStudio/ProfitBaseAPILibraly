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
                    catch (JsonException)
                    {
                        string srt = "[" + jsonString + "]";
                        return JsonConvert.DeserializeObject<JArray>(srt);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                        throw;
                    }
                }
            }
        }
    }
}
