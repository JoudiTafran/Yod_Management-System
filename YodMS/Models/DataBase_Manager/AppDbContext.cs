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
            base.OnModelCreating(modelBuilder);

            // Define Keys:

            modelBuilder.Entity<RoleDocPerms>()
                .HasKey(rp => new { rp.RoleId, rp.DocTypeId }); // مفتاح مركب 

            modelBuilder.Entity<DocumentTypes>().HasKey(dt => dt.DocTypeId);
            modelBuilder.Entity<Users>().HasKey(u => u.UserId);
            modelBuilder.Entity<Roles>().HasKey(r => r.RoleId);
            modelBuilder.Entity<Documents>().HasKey(d => d.DocId);
            modelBuilder.Entity<DocumentTypes>().HasKey(dt => dt.DocTypeId);
            modelBuilder.Entity<Reviews>().HasKey(r => r.ReviewId);
            modelBuilder.Entity<Votes>().HasKey(v => v.VoteId);
            modelBuilder.Entity<VoteSessions>().HasKey(vs => vs.VoteSessionId);

            // Define relationships:

            modelBuilder.Entity<RoleDocPerms>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RoleDocPerms)
                .HasForeignKey(rp => rp.RoleId);

            modelBuilder.Entity<RoleDocPerms>()
                .HasOne(rp => rp.DocumentType)
                .WithMany(d => d.RoleDocPerms)
                .HasForeignKey(rp => rp.DocTypeId);

            modelBuilder.Entity<Documents>()
                .HasOne(d => d.OwnerUser)
                .WithMany(u => u.Documents)
                .HasForeignKey(d => d.OwnerUserId);

            modelBuilder.Entity<Documents>()
                .HasOne(d => d.DocType)
                .WithMany(dt => dt.Documents)
                .HasForeignKey(d => d.DocTypeId);

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

            // Filling out the models:

            modelBuilder.Entity<Roles>().HasData(
                new Roles { RoleId = 1, RoleName = "رئيس الهيئة التنفيذية" },
                new Roles { RoleId = 2, RoleName = "نائب الرئيس" },
                new Roles { RoleId = 3, RoleName = "أمين عام" },
                new Roles { RoleId = 4, RoleName = "مسؤول مالي" },
                new Roles { RoleId = 5, RoleName = "مسؤول علاقات" },
                new Roles { RoleId = 6, RoleName = "مسؤول أكاديمي" },
                new Roles { RoleId = 7, RoleName = "مسؤول أنشطة" },
                new Roles { RoleId = 8, RoleName = "مسؤولة طالبات" },
                new Roles { RoleId = 9, RoleName = "مسؤول إعلامي" },
                new Roles { RoleId = 10, RoleName = "مٌراقب" }
             );

            modelBuilder.Entity<DocumentTypes>().HasData(
                new DocumentTypes { DocTypeId = 1, TypeName = "تصور" },
                new DocumentTypes { DocTypeId = 2, TypeName = "تقرير" },
                new DocumentTypes { DocTypeId = 3, TypeName = "بيان" },
                new DocumentTypes { DocTypeId = 4, TypeName = "محضر" },
                new DocumentTypes { DocTypeId = 5, TypeName = "خطة" }
             );

            modelBuilder.Entity<RoleDocPerms>().HasData(
                new RoleDocPerms { RoleId = 1, DocTypeId = 1 },
                new RoleDocPerms { RoleId = 1, DocTypeId = 2 },
                new RoleDocPerms { RoleId = 1, DocTypeId = 3 },
                new RoleDocPerms { RoleId = 2, DocTypeId = 1 },
                new RoleDocPerms { RoleId = 2, DocTypeId = 2 },
                new RoleDocPerms { RoleId = 3, DocTypeId = 1 },
                new RoleDocPerms { RoleId = 3, DocTypeId = 2 },
                new RoleDocPerms { RoleId = 3, DocTypeId = 3 },
                new RoleDocPerms { RoleId = 3, DocTypeId = 4 },
                new RoleDocPerms { RoleId = 4, DocTypeId = 1 },
                new RoleDocPerms { RoleId = 4, DocTypeId = 2 },
                new RoleDocPerms { RoleId = 5, DocTypeId = 1 },
                new RoleDocPerms { RoleId = 5, DocTypeId = 2 },
                new RoleDocPerms { RoleId = 5, DocTypeId = 5 },
                new RoleDocPerms { RoleId = 6, DocTypeId = 1 },
                new RoleDocPerms { RoleId = 6, DocTypeId = 2 },
                new RoleDocPerms { RoleId = 6, DocTypeId = 5 },
                new RoleDocPerms { RoleId = 7, DocTypeId = 1 },
                new RoleDocPerms { RoleId = 7, DocTypeId = 2 },
                new RoleDocPerms { RoleId = 7, DocTypeId = 5 },
                new RoleDocPerms { RoleId = 8, DocTypeId = 1 },
                new RoleDocPerms { RoleId = 8, DocTypeId = 2 },
                new RoleDocPerms { RoleId = 8, DocTypeId = 5 },
                new RoleDocPerms { RoleId = 9, DocTypeId = 1 },
                new RoleDocPerms { RoleId = 9, DocTypeId = 2 },
                new RoleDocPerms { RoleId = 9, DocTypeId = 5 }
             );
        }
    }
}
