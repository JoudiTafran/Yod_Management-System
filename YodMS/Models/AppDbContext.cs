using Microsoft.EntityFrameworkCore;

namespace YodMS.Models
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

            // علاقة Review ↔ Document
            modelBuilder.Entity<Reviews>()
                .HasOne(r => r.Document)
                .WithMany(d => d.Reviews)
                .HasForeignKey(r => r.DocId);

            modelBuilder.Entity<Reviews>()
                .HasOne(r => r.ReviewerUser)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.ReviewerUserId);

            modelBuilder.Entity<Votes>()
                .HasOne(v => v.VoteSession)
                .WithMany(s => s.Votes)
                .HasForeignKey(v => v.VoteId);

            modelBuilder.Entity<Votes>()
                .HasOne(v => v.VoterUser)
                .WithMany(u => u.Votes)
                .HasForeignKey(v => v.VoterUserId);

            modelBuilder.Entity<VoteSessions>()
                .HasOne(v => v.CreatedByUser)
                .WithMany(u => u.VoteSessionsCreated)
                .HasForeignKey(v => v.CreatedByUserId);
        }
    }
}
