using MagicVilla_VillaAPI.Models.Dtos;

namespace MagicVilla_Service.IService
{
    internal interface IVillaService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(VillaCreateDto dto);
        Task<T> UpdateAsync<T>(VillaUpdateDto dto);
        Task<T> DeleteAsync<T>(int id);
    }
}
