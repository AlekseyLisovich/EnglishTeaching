using DataLayer.Entities.Account;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IUserRepository : IAsyncRepository<User>
    {
        Task<User> GetByEmail(string email);
        Task<User> GetUserWithCredentials(string email, string password);
    }
}
