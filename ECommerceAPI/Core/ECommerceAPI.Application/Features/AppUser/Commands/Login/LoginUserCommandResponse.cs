using ECommerceAPI.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.AppUser.Commands.Login
{
    public class LoginUserCommandResponse
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public Token Token { get; set; }

    }
}
