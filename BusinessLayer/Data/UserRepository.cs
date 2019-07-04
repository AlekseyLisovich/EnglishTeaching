using BusinessLayer.Interfaces;
using DataLayer.DbContexts;
using DataLayer.Entities.Account;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BusinessLayer.Implementations
{
    public class UserRepository : EfRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationContext dbcontext) : base(dbcontext)
        {
        }
    
        public async Task<User> GetByEmail(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<User> GetUserWithCredentials(string email, string password)
        {
            User user = await _dbContext.Users
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

            return user;
        }
    }
}
