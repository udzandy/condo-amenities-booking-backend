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
        public DbSet<AmenityUnit> AmenityUnits => Set<AmenityUnit>();
        public DbSet<AmenityTimeSlot> AmenityTimeSlots => Set<AmenityTimeSlot>();
        public DbSet<AmenityPolicy> AmenityPolicies => Set<AmenityPolicy>();
        public DbSet<Booking> Bookings => Set<Booking>();
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

            // USER
            modelBuilder.Entity<User>()
                .HasKey(u => u.UserId);

            // AMENITY
            modelBuilder.Entity<Amenity>()
                .HasKey(x => x.AmenityId);

            // UNIT
            modelBuilder.Entity<AmenityUnit>()
                .HasKey(x => x.UnitId);

            modelBuilder.Entity<AmenityUnit>()
                .HasOne(x => x.Amenity)
                .WithMany(x => x.Units)
                .HasForeignKey(x => x.AmenityId);

            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasPrecision(18, 2);

            // SLOT
            modelBuilder.Entity<AmenityTimeSlot>()
                .HasKey(x => x.SlotId);

            modelBuilder.Entity<AmenityTimeSlot>()
                .HasOne(x => x.Unit)
                .WithMany(x => x.TimeSlots)
                .HasForeignKey(x => x.UnitId);

            modelBuilder.Entity<AmenityPolicy>()
                .HasKey(x => x.PolicyId);

            modelBuilder.Entity<AmenityPolicy>()
                .HasOne(x => x.Amenity)
                .WithOne(x => x.Policy)
                .HasForeignKey<AmenityPolicy>(x => x.AmenityId);

            // BOOKING
            // Concurrency token
            modelBuilder.Entity<Booking>()
                .Property(b => b.RowVersion)
                .IsRowVersion();

            modelBuilder.Entity<Booking>()
                .HasKey(x => x.BookingId);

            // Relationship with User (Keep Cascade if you want Bookings gone when User is deleted)
            modelBuilder.Entity<Booking>()
                .HasOne(x => x.User)
                .WithMany(x => x.Bookings)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationship with Amenity (Changed to Restrict)
            modelBuilder.Entity<Booking>()
                .HasOne(x => x.Amenity)
                .WithMany()
                .HasForeignKey(x => x.AmenityId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relationship with Unit (Keep Cascade)
            modelBuilder.Entity<Booking>()
                .HasOne(x => x.Unit)
                .WithMany(x => x.Bookings)
                .HasForeignKey(x => x.UnitId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationship with Slot (Changed to Restrict)
            modelBuilder.Entity<Booking>()
                .HasOne(x => x.Slot)
                .WithMany(x => x.Bookings)
                .HasForeignKey(x => x.SlotId)
                .OnDelete(DeleteBehavior.Restrict);

            // IMPORTANT UNIQUE RULE
            modelBuilder.Entity<Booking>()
                .HasIndex(x => new
                {
                    x.BookingDate,
                    x.UnitId,
                    x.SlotId
                })
                .IsUnique();


            //modelBuilder.Entity<Booking>()
            //    .HasOne(b => b.Amenity)
            //    .WithMany(a => a.Bookings)
            //    .HasForeignKey(b => b.AmenityId);

            //modelBuilder.Entity<Payment>()
            //    .HasOne(p => p.Booking)
            //    .WithOne(b => b.Payment)
            //    .HasForeignKey<Payment>(p => p.BookingId);

            modelBuilder.Entity<AuditLog>()
                .HasKey(a => a.LogId);

            modelBuilder.Entity<AuditLog>()
                .HasOne(a => a.User)
                .WithMany(u => u.AuditLogs)
                .HasForeignKey(a => a.UserId);
        }
    }
}
