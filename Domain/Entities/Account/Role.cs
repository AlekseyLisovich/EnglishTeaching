using System.Collections.Generic;

namespace DataLayer.Entities.Account
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }
        public List<User> Users { get; set; }
        public Role()
        {
            Users = new List<User>();
        }
    }
}
