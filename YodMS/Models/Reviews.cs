namespace YodMS.Models
{
    public class Reviews
    {
        public int ReviewId { get; set; }
        public int DocId { get; set; }
        public int ReviewerUserId { get; set; }

        public string Decision { get; set; }
        public string Comment { get; set; }
        public DateTime ReviewedAt { get; set; }

        public Documents Document { get; set; }
        public Users ReviewerUser { get; set; }
    }
}
