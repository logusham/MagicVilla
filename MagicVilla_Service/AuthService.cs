using MagicVilla.Utility;
using MagicVilla_Dtos;
using MagicVilla_Model;
using MagicVilla_Service.IService;
using MagicVilla_Web.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MagicVilla_Service
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private string villaUrl;

        public AuthService(IHttpClientFactory httpClientFactory,IConfiguration configuration) : base(httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
            villaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
        }
        public Task<T> LoginAsync<T>(LoginRequestDto dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Url = villaUrl + "/api/UsersAuth/login",
                Data = dto
            });
        }

        public Task<T> RegisterAsync<T>(RegisterationRequestDto dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Url = villaUrl + "/api/UsersAuth/Register",
                Data = dto
            });
        }
    }
}
