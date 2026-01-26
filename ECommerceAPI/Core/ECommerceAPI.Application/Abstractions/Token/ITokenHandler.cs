using ECommerceAPI.Application.Dtos;
using ECommerceAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Abstractions.Token
{
    public interface ITokenHandler
    {
        public Dtos.Token CreateAccessToken(User user, IList<string> roles);
        public string CreateRefreshToken();
    }
}
