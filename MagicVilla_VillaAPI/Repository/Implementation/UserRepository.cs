using MagicVilla_DataRepository.DataRepository.Interface;
using MagicVilla_Dtos;
using MagicVilla_Entity;
using MagicVilla_VillaAPI.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MagicVilla_DataRepository.DataRepository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private string secretkey;

        public UserRepository(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            secretkey = configuration.GetValue<string>("ApiSettings:Secret");
        }
        public bool IsUniqueUser(string? username)
        {
            var user = _db.LocalUsers.FirstOrDefault(u => u.UserName.ToLower() == username.ToLower());
            if(user != null)
            {
                return false;
            }
            return true;
        }

        public async Task<LoginResponceDto> Login(LoginRequestDto loginRequest)
        {
            var user = _db.LocalUsers.FirstOrDefault(u => 
            u.UserName.ToLower() == loginRequest.UserName.ToLower() && 
            u.Password == loginRequest.Password);
            if(user == null)
            {
                return new LoginResponceDto()
                {
                    Token = "",
                    User = null,
                };
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretkey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,user.Id.ToString()),
                    new Claim(ClaimTypes.Role,user.Role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponceDto loginResponce = new LoginResponceDto()
            {
                Token = tokenHandler.WriteToken(token),
                User = user,
            };
            return loginResponce;
        }

        public async Task<LocalUser> Register(RegisterationRequestDto registerationRequest)
        {
            LocalUser user = new()
            {
                UserName = registerationRequest.UserName,
                Name = registerationRequest.Name,
                Password = registerationRequest.Password,
                Role = registerationRequest.Role
            };
            await _db.LocalUsers.AddAsync(user);
            await _db.SaveChangesAsync();
            user.Password = "";
            return user;
        }
    }
}
