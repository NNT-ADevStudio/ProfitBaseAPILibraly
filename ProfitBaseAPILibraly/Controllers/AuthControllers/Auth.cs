using Newtonsoft.Json.Linq;
using ProfitBaseAPILibraly.Controllers.AuthControllers.Interfeses;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProfitBaseAPILibraly.Controllers.AuthControllers
{
    /// <summary>
    /// Класс, представляющий аутентификацию для API ProfitBase.
    /// </summary>
    public class Auth : IAuth
    {
        public string ApiKey { get; }

        public Uri BaseUrl { get; }

        public string Subdomain { get; set; }

        private string _accessToken;
        public string AccessToken => _accessToken ?? ( _accessToken = string.Empty);

        /// <summary>
        /// Создает новый экземпляр класса Auth.
        /// </summary>
        /// <param name="apiKey">Ключ API.</param>
        /// <param name="subdomain">Поддомен ProfitBase.</param>
        public Auth(string apiKey, string subdomain)
        {
            ApiKey = apiKey;
            BaseUrl = new Uri("https://" + subdomain + ".profitbase.ru/api/v4/json/");
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

                var response = await httpClient.PostAsync(
                    $"{BaseUrl}authentication", null)
                    .ConfigureAwait(false);

                if (!response.IsSuccessStatusCode) 
                {
                    return false;
                }

                string result = response.Content.ReadAsStringAsync().Result;
                JObject jsonObject = JObject.Parse(result);
                _accessToken = jsonObject["access_token"].ToString();
            }

            return true;
        }
    }
}
