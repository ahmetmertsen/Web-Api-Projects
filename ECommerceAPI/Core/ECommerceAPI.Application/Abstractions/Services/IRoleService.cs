using ECommerceAPI.Application.Dtos.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Abstractions.Services
{
    public interface IRoleService
    {
        Task<List<RoleDto>> GetAllRoles();
        Task<RoleDto> GetRoleById(int id);
        Task<bool> CreateRole(string name);
        Task<bool> DeleteRole(int id);
        Task<bool> UpdateRole(int id, string name);

    }
}
