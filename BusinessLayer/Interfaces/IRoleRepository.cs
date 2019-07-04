using DataLayer.Entities.Account;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IRoleRepository : IAsyncRepository<Role>
    {
        Task<Role> GetByName(string name);
    }
}
