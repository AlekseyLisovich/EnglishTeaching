namespace DataLayer.Entities.Account
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int? RoleId { get; set; }
        public Role Role { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string CellPhone { get; set; }
        public int Age { get; set; }
    }
}
