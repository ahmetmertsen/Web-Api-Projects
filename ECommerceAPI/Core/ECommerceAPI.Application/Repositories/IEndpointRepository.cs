using ECommerceAPI.Application.Repositories.Common;
using ECommerceAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Repositories
{
    public interface IEndpointRepository : IRepository<Endpoint>
    {
        Task<Endpoint?> GetEndpointWithMenuByCodeAsync(string code, string menu);
        Task<Endpoint?> GetRolesToEndpointWithMenu(string code, string menu);
        Task<Endpoint?> GetRolesToEndpoint(string code);
    }
}
