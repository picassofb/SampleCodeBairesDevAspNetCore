using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApiCore.Models
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<ProjectTask>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<Project>().Property(x => x.Status).HasDefaultValue(true);

            base.OnModelCreating(modelBuilder);
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {

        }


    }
}
