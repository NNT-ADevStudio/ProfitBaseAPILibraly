using System;
using System.Threading.Tasks;

namespace ProfitBaseAPILibraly.Controllers.AuthControllers.Interfeses
{
    public interface IAuth
    {
        /// <summary>
        /// Ключ API.
        /// </summary>
        string ApiKey { get; }

        /// <summary>
        /// Базовый URL для API ProfitBase.
        /// </summary>
        Uri BaseUrl { get; }

        /// <summary>
        /// Поддомен ProfitBase.
        /// </summary>
        string Subdomain { get; set; }

        /// <summary>
        /// Токен доступа.
        /// </summary>
        string AccessToken { get; }

        /// <summary>
        /// Обновляет токен доступа.
        /// </summary>
        /// <returns>Возвращает true, если обновление токена прошло успешно, иначе false.</returns>
        Task<bool> RefreshAccessToken();
    }
}
