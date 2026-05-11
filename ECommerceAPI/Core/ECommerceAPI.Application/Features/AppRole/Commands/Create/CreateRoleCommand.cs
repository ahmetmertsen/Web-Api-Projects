using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.AppRole.Commands.Create
{
    public class CreateRoleCommand : IRequest<CreateRoleCommandResponse>
    {
        public string Name { get; set; } = null!;
    }
}
