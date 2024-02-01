using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProfitBaseAPILibraly.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProfitBaseAPILibraly.Controllers
{
    public abstract class MainController
    {
        /// <summary>
        /// Класс аутентификации
        /// </summary>
        public Auth Auth { get; }

        /// <summary>
        /// Создает новый экземпляр класса ProfitBaseAPI
        /// </summary>
        /// <param name="auth">Класс аутентификации</param>
        public MainController(Auth auth) => Auth = auth;

        /// <summary>
        /// Создает URL с query-параметрами из словаря и заданного endpoint.
        /// Токен доступа для аутентификации включается автоматически.
        /// Возвращает сконструированный объект Uri.
        /// </summary>
        /// <param name="keyValues">Словарь пар ключ-значение параметров запроса.</param>
        /// <param name="endPoint">Конечная точка для добавления к базовому URL.</param>
        /// <returns>Объект Uri с полным URL и строкой запроса.</returns>
        protected Uri CreateUrl(Dictionary<string, string> keyValues, string endPoint)
        {
            StringBuilder temp = new StringBuilder();

            if (keyValues != null)
                foreach (KeyValuePair<string, string> item in keyValues)
                    temp.Append($"&{item.Key}={item.Value}");

            var url = new UriBuilder($"{Auth.BaseUrl}{endPoint}")
            {
                Query = $"access_token={Auth.AccessToken}" + temp.ToString()
            };

            return url.Uri;
        }

        /// <summary>
        /// Получает ответ от указанного URL и возвращает десериализованный объект JArray.
        /// </summary>
        /// <param name="url">URL для отправки GET-запроса.</param>
        /// <returns>
        /// Десериализованный объект JArray, представляющий ответ, или null, если ответ
        /// не был успешен.
        /// </returns>
        /// <exception cref="JsonException">
        /// Выбрасывается, если ответ не может быть десериализован в объект JArray.
        /// </exception>
        /// <exception cref="Exception">
        /// Выбрасывается при возникновении любой другой ошибки во время выполнения функции.
        /// </exception>
        protected static async Task<JArray> GetResultResponse(string url)
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
                        Debug.WriteLine(ex + "\n" + jsonString);
                        throw;
                    }
                }
            }
        }

        protected static async Task<bool> PatchAsync(string path, string jsonBody)
        {
            using (var client = new HttpClient())
            {
                var method = "PATCH";
                var httpVerb = new HttpMethod(method);
                var httpRequestMessage =
                    new HttpRequestMessage(httpVerb, path)
                    {
                        Content = new StringContent(jsonBody, Encoding.UTF8, "application/json")
                    };
                try
                {
                    var response = await client.SendAsync(httpRequestMessage);
                    if (!response.IsSuccessStatusCode)
                    {
                        var responseCode = response.StatusCode;
                        var responseJson = await response.Content.ReadAsStringAsync();
                        return false;
                    }

                    return true;
                }
                catch (Exception exception)
                {
                    throw new CustomException($"Error patching {jsonBody} in {path}\n{exception.Message}");
                }
            }
        }
    }
}
