namespace YodMS.Models
{
    public class DocumentTypes
    {
        public int DocTypeId { get; set; }
        public string TypeName { get; set; }

        public ICollection<Documents> Documents { get; set; }
        public ICollection<RoleDocPerms> RoleDocPerms { get; set; }
    }
}
