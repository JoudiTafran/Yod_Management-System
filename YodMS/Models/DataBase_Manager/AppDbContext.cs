using Microsoft.EntityFrameworkCore;

namespace YodMS.Models.DataBase_Manager
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Documents> Documents { get; set; }
        public DbSet<DocumentTypes> DocumentTypes { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<VoteSessions> VoteSessions { get; set; }
        public DbSet<Votes> Votes { get; set; }
        public DbSet<RoleDocPerms> RoleDocPerms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // مفتاح مركب لـ RoleDocPerm
            modelBuilder.Entity<RoleDocPerms>()
                .HasKey(rp => new { rp.RoleId, rp.DocTypeId });

            modelBuilder.Entity<DocumentTypes>().HasKey(dt => dt.DocTypeId);
            modelBuilder.Entity<Users>().HasKey(u => u.UserId);
            modelBuilder.Entity<Roles>().HasKey(r => r.RoleId);
            modelBuilder.Entity<Documents>().HasKey(d => d.DocId);
            modelBuilder.Entity<DocumentTypes>().HasKey(dt => dt.DocTypeId);
            modelBuilder.Entity<Reviews>().HasKey(r => r.ReviewId);
            modelBuilder.Entity<Votes>().HasKey(v => v.VoteId);
            modelBuilder.Entity<VoteSessions>().HasKey(vs => vs.VoteSessionId);


            modelBuilder.Entity<RoleDocPerms>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RoleDocPerms)
                .HasForeignKey(rp => rp.RoleId);

            modelBuilder.Entity<RoleDocPerms>()
                .HasOne(rp => rp.DocumentType)
                .WithMany(d => d.RoleDocPerms)
                .HasForeignKey(rp => rp.DocTypeId);

            // علاقة Document ↔ OwnerUser
            modelBuilder.Entity<Documents>()
                .HasOne(d => d.OwnerUser)
                .WithMany(u => u.Documents)
                .HasForeignKey(d => d.OwnerUserId);

            modelBuilder.Entity<Documents>()
                .HasOne(d => d.DocType)
                .WithMany(dt => dt.Documents)
                .HasForeignKey(d => d.DocTypeId);

            // علاقة Review ↔ Document
            modelBuilder.Entity<Reviews>()
                .HasOne(r => r.Document)
                .WithMany(d => d.Reviews)
                .HasForeignKey(r => r.DocId);

            modelBuilder.Entity<Reviews>()
                .HasOne(r => r.ReviewerUser)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.ReviewerUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Votes>()
                .HasOne(v => v.VoteSession)
                .WithMany(s => s.Votes)
                .HasForeignKey(v => v.VoteSessionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Votes>()
                .HasOne(v => v.VoterUser)
                .WithMany(u => u.Votes)
                .HasForeignKey(v => v.VoterUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<VoteSessions>()
                .HasOne(v => v.CreatedByUser)
                .WithMany(u => u.VoteSessionsCreated)
                .HasForeignKey(v => v.CreatedByUserId);
        }
    }
}
