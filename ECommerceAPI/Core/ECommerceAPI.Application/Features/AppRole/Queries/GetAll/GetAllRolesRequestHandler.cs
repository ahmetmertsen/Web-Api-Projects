using AutoMapper;
using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Application.Dtos.Role;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.AppRole.Queries.GetAll
{
    public class GetAllRolesRequestHandler : IRequestHandler<GetAllRolesRequest, List<RoleDto>>
    {
        private readonly IRoleService _roleService;

        public GetAllRolesRequestHandler(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
        }

        public async Task<List<RoleDto>> Handle(GetAllRolesRequest request, CancellationToken cancellationToken)
        {
            var roles = await _roleService.GetAllRoles();
            return roles;
        }
    }
}
