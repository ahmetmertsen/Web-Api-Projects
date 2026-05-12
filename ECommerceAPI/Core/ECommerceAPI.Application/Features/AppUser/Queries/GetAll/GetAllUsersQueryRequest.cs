using ECommerceAPI.Application.Dtos.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.AppUser.Queries.GetAll
{
    public class GetAllUsersQueryRequest : IRequest<List<UserDto>>
    {
    }
}
