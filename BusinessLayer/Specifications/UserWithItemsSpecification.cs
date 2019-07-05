using DataLayer.Entities.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Specifications
{
    public sealed class UserWithItemsSpecification : BaseSpecification<User>
    {
        public UserWithItemsSpecification(int Id)
            : base(b => b.Id == Id)
        {
            AddInclude(b => b.Role);
        }

        public UserWithItemsSpecification(string email)
            : base(b => b.Email == email)
        {
        }

        public UserWithItemsSpecification(string email, string password)
            : base(b => b.Email == email && b.Password == password)
        {
            AddInclude(b => b.Role);
        }
    }
}
