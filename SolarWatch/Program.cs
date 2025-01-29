using Microsoft.EntityFrameworkCore;
using SolarWatch.Services.ApiServices;
using SolarWatch.Services.ParseServices;
using SolarWatch.Data.Context;
using SolarWatch.Services.Repositories;
using SolarWatch.Services.SolarServices;

namespace SolarWatch
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddScoped<ICityDataProvider, OpenWeatherMapApi>();
            builder.Services.AddScoped<ICityJsonParser, CityJsonParseService>();
            builder.Services.AddScoped<ISunMoveProvider, SunRiseSetApi>();
            builder.Services.AddScoped<ISunJsonParser, SunJsonParseService>();
            builder.Services.AddScoped<ICityRepository, CityRepository>();
            builder.Services.AddScoped<ISunMovementRepository, SunMovementRepository>();
            builder.Services.AddScoped<ISolar, SolarService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<SolarWatchContext>(options =>
            {
                options.UseSqlServer(
                    "Server=localhost,1433;Database=SolarWatch;User Id=sa;Password=yourStrong(!)Password;Encrypt=false;");
            });



            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
