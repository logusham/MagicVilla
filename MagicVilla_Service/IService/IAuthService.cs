using MagicVilla_Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicVilla_Service.IService
{
    public interface IAuthService
    {
        Task<T> LoginAsync<T>(LoginRequestDto dto);
        Task<T> RegisterAsync<T>(RegisterationRequestDto dto);
    }
}
