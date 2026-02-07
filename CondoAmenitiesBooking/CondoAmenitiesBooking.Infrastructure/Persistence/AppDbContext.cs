using CondoAmenitiesBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CondoAmenitiesBooking.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion<string>();

            modelBuilder.Entity<User>()
                .Property(u => u.OccupancyType)
                .HasConversion<string>();
        }

        public DbSet<User> Users => Set<User>();
    }
}
