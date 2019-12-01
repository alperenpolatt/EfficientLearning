using EfLearning.Core.Announcements;
using EfLearning.Core.Classrooms;
using EfLearning.Core.Practices;
using EfLearning.Core.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EfLearning.Data
{
    public class EfContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public EfContext()
        {

        }
        //For startup identity
        public EfContext(DbContextOptions<EfContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(local)\SQLEXPRESS;Database=EfLearning;integrated security=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Material>()
                 .HasOne(pt => pt.GivenClassroom)
                 .WithMany(p => p.Materials)
                 .HasForeignKey(pt => pt.GivenClassroomId);


            modelBuilder.Entity<Material>()
                    .HasOne(a => a.Announcement)
                    .WithOne(a => a.Material)
                    .HasForeignKey<Announcement>(c => c.MaterialId);
            #region aspIdentity
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AppUser>().Ignore(x => x.PhoneNumber);
            modelBuilder.Entity<AppUser>().Ignore(x => x.PhoneNumberConfirmed);
            modelBuilder.Entity<AppUser>().Ignore(x => x.TwoFactorEnabled);
            #endregion
        }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public DbSet<Course> Courses { get; set; }
        public DbSet<GivenClassroom> GivenClassrooms { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<MaterialAnswer> MaterialAnswers { get; set; }
        public DbSet<TakenClassroom> TakenClassrooms { get; set; }

        public DbSet<DonePractice> DonePractices { get; set; }
        public DbSet<GivenPractice> GivenPractices { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

    }
}
