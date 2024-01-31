using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Dal.Models
{
    public  class JobsPortalDbContext: DbContext
    {
        public JobsPortalDbContext(DbContextOptions<JobsPortalDbContext> options)
           : base(options)
        {

        }
       

        public DbSet<Company>? Companies => Set<Company>();
        public DbSet<Employee>? Employees => Set<Employee>();
        public DbSet<JobCategory>? JobCategories => Set<JobCategory>();
        public DbSet<JobNature>? JobNatures => Set<JobNature>();
        public DbSet<JobRequirementDetails>? JobRequirementDetailsList => Set<JobRequirementDetails>();
        public DbSet<JobRequirements>? JobRequirementsList => Set<JobRequirements>();
        public DbSet<JobStatus>? JobStatuses => Set<JobStatus>();
        public DbSet<PostJob>? PostJobs => Set<PostJob>();
        public DbSet<User>? Users => Set<User>();
        public DbSet<UserType>? UserTypes => Set<UserType>();

        public DbSet<PrimarySkill>? PrimarySkill => Set<PrimarySkill>();

        public DbSet<SecondarySkill>? SecondarySkill => Set<SecondarySkill>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Replace the connection string with your actual SQL Server connection string
            optionsBuilder.UseSqlServer("server=ANANTH;database=JobPortal1;trusted_connection=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure entity relationships and other model configurations here
            // ...

            // Resolve multiple cascade paths issue for PostJob -> User relationship
            modelBuilder.Entity<PostJob>()
                .HasOne(p => p.User)
                .WithMany(u => u.PostJobs)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_PostJobs_Users_UserId");

            // Add similar configurations for other relationships if needed

            base.OnModelCreating(modelBuilder);
        }

    }
}
