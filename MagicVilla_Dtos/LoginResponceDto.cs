using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagicVilla_Entity;

namespace MagicVilla_Dtos
{
    public class LoginResponceDto
    {
        public LocalUser? User { get; set; }
        public string? Token { get; set; }
    }
}
