
namespace YodMS.Models
{
    public class Documents
    {
        public int DocId { get; set; }
        public int OwnerUserId { get; set; }
        public int DocTypeId { get; set; }

        public string Title { get; set; }
        public DateTime EventDate { get; set; }
        public string Status { get; set; }
        public string FilePath { get; set; }
        public DateTime CreatedAt { get; set; }

        public Users OwnerUser { get; set; }
        public DocumentTypes DocType { get; set; }
        public ICollection<Reviews> Reviews { get; set; }
    }
}
