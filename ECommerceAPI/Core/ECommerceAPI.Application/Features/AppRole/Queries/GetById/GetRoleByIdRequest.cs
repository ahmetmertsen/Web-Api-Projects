using ECommerceAPI.Application.Dtos.Role;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.AppRole.Queries.GetById
{
    public class GetRoleByIdRequest : IRequest<RoleDto>
    {
        public int Id { get; set; }
    }
}
