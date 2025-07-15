namespace YodMS.Models
{
    public class VoteSessions
    {
        public int VoteSessionId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public int CreatedByUserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ClosedAt { get; set; }
        public string Status { get; set; }

        public Users CreatedByUser { get; set; }
        public ICollection<Votes> Votes { get; set; }
    }

}
