namespace YodMS.Models
{
    public class Roles
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public ICollection<Users> Users { get; set; }
        public ICollection<RoleDocPerms> RoleDocPerms { get; set; }
    }
}
