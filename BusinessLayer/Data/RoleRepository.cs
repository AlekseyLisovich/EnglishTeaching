using BusinessLayer.Interfaces;
using DataLayer.DbContexts;
using DataLayer.Entities.Account;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BusinessLayer.Implementations
{
    public class RoleRepository : EfRepository<Role>, IRoleRepository
    {
        public RoleRepository(ApplicationContext dbcontext) : base(dbcontext)
        {
        }

        public async Task<Role> GetByName(string name)
        {
            return await _dbContext.Roles.FirstOrDefaultAsync(r => r.Name == name);
        }
    }
}
