using DataLayer.Entities.Account;

namespace BusinessLayer.Specifications
{
    public sealed class RoleWithEmailSpecification : BaseSpecification<Role>
    {
        public RoleWithEmailSpecification(string name)
            : base(b => b.Name == name)
        {
        }
    }
}
