using AutoMapper;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dtos;

namespace MagicVilla_VillaAPI.Mapper
{
    public class Profiles :Profile
    {
        public Profiles()
        {
            CreateMap<Villa, VillaDto>().ReverseMap();
            CreateMap<VillaCreateDto, Villa>().ReverseMap();
            CreateMap<VillaUpdateDto, Villa>().ReverseMap();
            CreateMap<VillaNumber, VillaNumberDto>().ReverseMap();
            CreateMap<VillaNumberCreateDto, VillaNumber>().ReverseMap();
            CreateMap<VillaNumberUpdateDto, VillaNumber>().ReverseMap();
        }
    }
}
