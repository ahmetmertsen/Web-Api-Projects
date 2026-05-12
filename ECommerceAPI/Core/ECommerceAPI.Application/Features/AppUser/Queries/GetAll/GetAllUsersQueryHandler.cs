using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Dtos.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.AppUser.Queries.GetAll
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQueryRequest, List<UserDto>>
    {
        private readonly IUserService _userService;

        public GetAllUsersQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<List<UserDto>> Handle(GetAllUsersQueryRequest request, CancellationToken cancellationToken)
        {
            var users = await _userService.GetAllUsersAsync();
            return users;
        }
    }
}
