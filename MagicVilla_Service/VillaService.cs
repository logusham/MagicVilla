using MagicVilla.Utility;
using MagicVilla_Entity.Helper;
using MagicVilla_Model;
using MagicVilla_Service.IService;
using MagicVilla_VillaAPI.Models.Dtos;
using MagicVilla_Web.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Net.Http;

namespace MagicVilla_Service
{
    public class VillaService : BaseService, IVillaService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IOptions<ServiceUrls> _configServiceUrls;
        private string villaUrl;
        public VillaService(IHttpClientFactory httpClientFactory,IOptions<ServiceUrls> configServiceUrls) : base(httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
            _configServiceUrls = configServiceUrls;
            villaUrl = _configServiceUrls.Value.VillaAPI;
        }
        public Task<T> CreateAsync<T>(VillaCreateDto dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Url = villaUrl+"/api/VillaAPI",
                Data = dto,
                Token = token
            });
        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = villaUrl + "/api/VillaAPI/"+id,
                Token = token
            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = villaUrl + "/api/VillaAPI",
                Token = token
            });
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = villaUrl + "/api/VillaAPI/"+id,
                Token = token
            });
        }

        public Task<T> UpdateAsync<T>(VillaUpdateDto dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Url = villaUrl + "/api/VillaAPI/"+dto.Id,
                Data = dto,
                Token = token
            });
        }
    }
}
