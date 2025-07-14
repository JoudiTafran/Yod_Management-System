namespace YodMS.Models
{
    public class RoleDocPerms
    {
        public int RoleId { get; set; }
        public int DocTypeId { get; set; }

        public Roles Role { get; set; }
        public DocumentTypes DocumentType { get; set; }
    }
}
