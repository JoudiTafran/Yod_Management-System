namespace YodMS.Models
{
    public class Users
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneWhatsapp { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }

        public Roles Role { get; set; }
        public ICollection<Documents> Documents { get; set; }
        public ICollection<Reviews> Reviews { get; set; }
        public ICollection<VoteSessions> VoteSessionsCreated { get; set; }
        public ICollection<Votes> Votes { get; set; }
    }
}
