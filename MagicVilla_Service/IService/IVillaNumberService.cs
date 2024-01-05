using MagicVilla_VillaAPI.Models.Dtos;

namespace MagicVilla_Service.IService
{
    public interface IVillaNumberService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(VillaNumberCreateDto dto, string token);
        Task<T> UpdateAsync<T>(VillaNumberUpdateDto dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);
    }
}
