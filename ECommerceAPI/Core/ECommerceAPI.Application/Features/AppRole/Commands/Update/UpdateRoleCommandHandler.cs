using ECommerceAPI.Application.Abstractions.Services;
using ECommerceAPI.Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.AppRole.Commands.Update
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, UpdateRoleCommandResponse>
    {
        private readonly IRoleService _roleService;

        public UpdateRoleCommandHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<UpdateRoleCommandResponse> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleService.GetRoleById(request.Id);
            if (role == null) 
            {
                throw new NotFoundException("Rol bulanamadı.");
            }
            await _roleService.UpdateRole(request.Id, request.Name);

            return new UpdateRoleCommandResponse
            {
                Succeeded = true,
                Message = "Rol başarıyla güncellendi"
            };
        }
    }
}
