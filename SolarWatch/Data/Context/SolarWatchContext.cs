using Microsoft.EntityFrameworkCore;
using SolarWatch.Data.Entities;
using System.Diagnostics.Metrics;

namespace SolarWatch.Data.Context
{
    public class SolarWatchContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<SunMovement> SunMovements { get; set; }


        public SolarWatchContext(DbContextOptions<SolarWatchContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<City>()
                .HasIndex(u => u.Name)
                .IsUnique();
        }
    }
}
