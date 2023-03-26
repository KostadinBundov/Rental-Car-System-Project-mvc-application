using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rental_Car_System_Project.Models;

namespace Rental_Car_System_Project.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .HasIndex(x => x.UserName)
                .IsUnique();

            builder.Entity<User>()
                .HasIndex(x => x.Email)
                .IsUnique();

            builder.Entity<User>()
                .HasIndex(x => x.PIN)
                .IsUnique();
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
    }
}