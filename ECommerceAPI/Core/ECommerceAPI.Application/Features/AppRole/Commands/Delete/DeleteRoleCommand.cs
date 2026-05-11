using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.AppRole.Commands.Delete
{
    public class DeleteRoleCommand : IRequest<DeleteRoleCommandResponse>
    {
        public int Id { get; set; }
    }
}
