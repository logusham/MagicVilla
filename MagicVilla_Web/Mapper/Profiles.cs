using AutoMapper;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dtos;

namespace MagicVilla_Web.Mapper
{
    public class Profiles :Profile
    {
        public Profiles()
        {
            CreateMap<VillaCreateDto, VillaDto>().ReverseMap();
            CreateMap<VillaUpdateDto, VillaDto>().ReverseMap();
            CreateMap<VillaNumberCreateDto, VillaNumberDto>().ReverseMap();
            CreateMap<VillaNumberUpdateDto, VillaNumberDto>().ReverseMap();
        }
    }
}
