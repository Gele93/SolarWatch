using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SolarWatch.Data.Entities;
using System.Diagnostics.Metrics;

namespace SolarWatch.Data.Context
{
    public class SolarWatchContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<SunMovement> SunMovements { get; set; }

        public SolarWatchContext(DbContextOptions<SolarWatchContext> options) : base(options)
        {
        }

    }
}
