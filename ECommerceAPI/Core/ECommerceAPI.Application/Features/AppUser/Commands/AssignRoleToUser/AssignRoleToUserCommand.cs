using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.AppUser.Commands.AssignRoleToUser
{
    public class AssignRoleToUserCommand : IRequest<AssignRoleToUserCommandResponse>
    {
        public int UserId { get; set; }
        public string[] Roles { get; set; } = null!;
    }
}
