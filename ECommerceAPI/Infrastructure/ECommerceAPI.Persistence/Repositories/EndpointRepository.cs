using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Persistence.Contexts;
using ECommerceAPI.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Repositories
{
    public class EndpointRepository : Repository<Endpoint>, IEndpointRepository
    {
        private readonly ECommerceDbContext _context;

        public EndpointRepository(ECommerceDbContext context) : base(context) { _context = context; }

        public async Task<Endpoint?> GetEndpointWithMenuByCodeAsync(string code, string menu) => await _context.Endpoints
            .Include(e => e.Menu)
            .Include(e => e.Roles)
            .FirstOrDefaultAsync(c => c.Code == code && c.Menu.Name == menu);

        public async Task<Endpoint?> GetRolesToEndpointWithMenu(string code, string menu) => await _context.Endpoints
            .Include(e => e.Roles)
            .Include(e => e.Menu)
            .FirstOrDefaultAsync(e => e.Code == code && e.Menu.Name == menu);

        public async Task<Endpoint?> GetRolesToEndpoint(string code) => await _context.Endpoints
            .Include(e => e.Roles)
            .FirstOrDefaultAsync(e => e.Code == code);
    }
}
