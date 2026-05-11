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
    public class MenuRepository : Repository<Menu>, IMenuRepository
    {
        private readonly ECommerceDbContext _context;

        public MenuRepository(ECommerceDbContext context) : base(context) { _context = context; }

        public async Task<Menu?> GetMenuByNameAsync(string name) => await _context.Menus
            .FirstOrDefaultAsync(m => m.Name == name);
    }
}
