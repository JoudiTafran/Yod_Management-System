namespace YodMS.Models
{
    public class Votes
    {
        public int VoteId { get; set; }

        public int VoterUserId { get; set; }
        public int VoteSessionId { get; set; } 

        public string Choice { get; set; }
        public string Comment { get; set; }
        public DateTime VotedAt { get; set; }

        public VoteSessions VoteSession { get; set; }
        public Users VoterUser { get; set; }
    }
}
