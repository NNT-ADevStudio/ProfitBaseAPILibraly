using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProfitBaseAPILibraly.Controllers
{
    /// <summary>
    /// Класс, представляющий аутентификацию для API ProfitBase.
    /// </summary>
    public class Auth
    {
        /// <summary>
        /// Путь к папке AppData.
        /// </summary>
        private string AppDataPath { get; } = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        /// <summary>
        /// Ключ API.
        /// </summary>
        public string ApiKey { get; }

        /// <summary>
        /// Базовый URL для API ProfitBase.
        /// </summary>
        public Uri BaseUrl { get; }

        /// <summary>
        /// Поддомен ProfitBase.
        /// </summary>
        public string Subdomain { get; set; }

        /// <summary>
        /// Токен доступа.
        /// </summary>
        public string AccessToken
        {
            get
            {
                string json = File.ReadAllText($"{AppDataPath}\\{Subdomain}.json");
                JObject jsonObject = JObject.Parse(json);
                return jsonObject["access_token"].ToString();
            }
        }

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

        /// <summary>
        /// Создает новый экземпляр класса Auth.
        /// </summary>
        /// <param name="apiKey">Ключ API.</param>
        /// <param name="subdomain">Поддомен ProfitBase.</param>
        public Auth(string apiKey, string subdomain, string path)
        {
            ApiKey = apiKey;
            BaseUrl = new Uri("https://" + subdomain + ".profitbase.ru/api/v4/json/");
            Subdomain = subdomain;
        }

        /// <summary>
        /// Обновляет токен доступа.
        /// </summary>
        /// <returns>Возвращает true, если обновление токена прошло успешно, иначе false.</returns>
        public async Task<bool> RefreshAccessToken()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                StringBuilder stringBuilder = new StringBuilder();

                stringBuilder.Append("{\"type\": \"api-app\",\"credentials\": {");
                stringBuilder.Append($"\"pb_api_key\": \"{ApiKey}\"");
                stringBuilder.Append("}}");

                var data = new StringContent(stringBuilder.ToString(), null, "application/json");
                var response = await httpClient.PostAsync(
                    $"{BaseUrl}authentication", data)
                    .ConfigureAwait(false);
                data.Dispose();
                string result = response.Content.ReadAsStringAsync().Result;
                JObject jsonObject = JObject.Parse(result);
                SetTokens(jsonObject["access_token"].ToString());
            }

            return true;
        }

        /// <summary>
        /// Устанавливает токены доступа.
        /// </summary>
        /// <param name="access">Токен доступа.</param>
        public void SetTokens(string access)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append('{');
            stringBuilder.Append($"\"access_token\": \"{access}\"");
            stringBuilder.Append('}');

            string json = stringBuilder.ToString();

            File.WriteAllText($"{AppDataPath}\\{Subdomain}.json", json);
        }
    }
}
