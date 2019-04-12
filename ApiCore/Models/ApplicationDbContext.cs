using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApiCore.Models
{
    //Inherid from IdentityDbContext so we can use EF core and the default authentication system
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {
        //DbSets to transform models to tables
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }

        // EF Core's Fluent API provides methods for configuring various aspects of model.
        // We Override this method to add our own configurations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<ProjectTask>().HasQueryFilter(x => x.IsDeleted == false);
            //modelBuilder.Entity<Project>().Property(x => x.Done).HasDefaultValue(true);

            base.OnModelCreating(modelBuilder);
        }

        // Constructor to receive the options we've added in AddDbContext service and pass them to IdentityDbContext
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {

        }


    }
}
