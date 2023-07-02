using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagicVilla_Dtos;
using MagicVilla_Entity;

namespace MagicVilla_DataRepository.DataRepository.Interface
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string? username);
        Task<LoginResponceDto> Login(LoginRequestDto loginRequest);
        Task<LocalUser> Register(RegisterationRequestDto registerationRequest);

    }
}
