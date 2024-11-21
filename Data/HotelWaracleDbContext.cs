using HotelWaracleBookingApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace HotelWaracleBookingApi.Data
{
    public class HotelWaracleDbContext : DbContext
    {
        private readonly ILogger<HotelWaracleDbContext> _logger;

        public HotelWaracleDbContext(DbContextOptions<HotelWaracleDbContext> options, ILogger<HotelWaracleDbContext> logger) : base(options)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        } 

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<HotelRoom> HotelRooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Hotel>().ToTable("Hotels", "dbo");
            modelBuilder.Entity<HotelRoom>().ToTable("HotelRooms", "dbo");
            modelBuilder.Entity<Booking>().ToTable("Bookings", "dbo");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }
    }
}
