using DataLayer.Entities.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Specifications
{
    public sealed class UserWithItemsSpecification : BaseSpecification<User>
    {
        public UserWithItemsSpecification(string email)
            : base(b => b.Email == email)
        {
        }
    }
}
