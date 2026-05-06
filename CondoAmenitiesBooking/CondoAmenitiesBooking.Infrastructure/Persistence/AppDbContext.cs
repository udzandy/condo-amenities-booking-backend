using CondoAmenitiesBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CondoAmenitiesBooking.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Amenity> Amenities => Set<Amenity>();
        public DbSet<Booking> Bookings => Set<Booking>();
        public DbSet<AmenityRule> AmenityRules => Set<AmenityRule>();
        public DbSet<Payment> Payments => Set<Payment>();
        public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<User>()
            //    .Property(u => u.Role)
            //    .HasConversion<string>();

            //modelBuilder.Entity<User>()
            //    .Property(u => u.OccupancyType)
            //    .HasConversion<string>();

            // Primary Key for User (string)
            modelBuilder.Entity<User>()
                .HasKey(u => u.UserId);

            // Explicitly defines precision (18 total digits, 2 after decimal)
            modelBuilder.Entity<Amenity>()
                .Property(a => a.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasPrecision(18, 2);

            // Concurrency token
            modelBuilder.Entity<Booking>()
                .Property(b => b.RowVersion)
                .IsRowVersion();

            // Relationships
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Amenity)
                .WithMany(a => a.Bookings)
                .HasForeignKey(b => b.AmenityId);

            modelBuilder.Entity<AmenityRule>()
                .HasKey(a => a.RuleId);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Booking)
                .WithOne(b => b.Payment)
                .HasForeignKey<Payment>(p => p.BookingId);

            modelBuilder.Entity<AuditLog>()
                .HasKey(a => a.LogId);

            modelBuilder.Entity<AuditLog>()
                .HasOne(a => a.User)
                .WithMany(u => u.AuditLogs)
                .HasForeignKey(a => a.UserId);
        }

        //public DbSet<User> Users => Set<User>();
    }
}
