using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using SolarWatch.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarIntegrationTest.Factories
{
    public class SolarWebApplicationFactory : WebApplicationFactory<Program>
    {
        private readonly string _dbName = Guid.NewGuid().ToString();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");

            builder.ConfigureServices(services =>
            {
                var solarWatchDbContextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<SolarWatchContext>));

                services.Remove(solarWatchDbContextDescriptor);

                services.AddDbContext<SolarWatchContext>(options =>
                {
                    options.UseInMemoryDatabase(_dbName);
                });


                using var scope = services.BuildServiceProvider().CreateScope();
                var solarContext = scope.ServiceProvider.GetRequiredService<SolarWatchContext>();
                solarContext.Database.EnsureDeleted();
                solarContext.Database.EnsureCreated();


            });
        }
    }
}
