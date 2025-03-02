using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SolarWatch.Data.DataImport;
using SolarWatch.Data.Entities;
using System.Diagnostics.Metrics;

namespace SolarWatch.Data.Context
{
    public class SolarWatchContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        private readonly bool _isTesting;
        public DbSet<City> Cities { get; set; }
        public DbSet<SunMovement> SunMovements { get; set; }
        public DbSet<CityName> CityNames { get; set; }
        public SolarWatchContext(DbContextOptions<SolarWatchContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CityName>().HasData(
                CityNameReader.Read()
            );
        }
    }
}
