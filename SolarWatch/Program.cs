using Microsoft.EntityFrameworkCore;
using SolarWatch.Services.ApiServices;
using SolarWatch.Services.ParseServices;
using SolarWatch.Data.Context;
using SolarWatch.Services.Repositories;
using SolarWatch.Services.SolarServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SolarWatch.Services.AuthServices;
using Microsoft.AspNetCore.Identity;

namespace SolarWatch
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            AddServices(builder);
            AddDbContext(builder);
            AddAuthentication(builder, configuration);
            AddIdentity(builder);


            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        private static void AddServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ICityDataProvider, OpenWeatherMapApi>();
            builder.Services.AddScoped<ICityJsonParser, CityJsonParseService>();
            builder.Services.AddScoped<ISunMoveProvider, SunRiseSetApi>();
            builder.Services.AddScoped<ISunJsonParser, SunJsonParseService>();
            builder.Services.AddScoped<ICityRepository, CityRepository>();
            builder.Services.AddScoped<ISunMovementRepository, SunMovementRepository>();
            builder.Services.AddScoped<ISolar, SolarService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        }

        private static void AddDbContext(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<SolarWatchContext>(options =>
            {
                var connectionString = Environment.GetEnvironmentVariable("SolarWatch_connection");
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new InvalidOperationException("Connection string not found.");
                }

                options.UseSqlServer(connectionString);
            });
        }
        private static void AddAuthentication(WebApplicationBuilder builder, ConfigurationManager configuration)
        {
            // Read from JwtSettings
            var issuer = configuration["JwtSettings:Issuer"];
            var audience = configuration["JwtSettings:Audience"];
            var secretKey = configuration["JwtSettings:SecretKey"];

            builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ClockSkew = TimeSpan.Zero,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(secretKey)
            ),
        };
    });
        }
        private static void AddIdentity(WebApplicationBuilder builder)
        {
            builder.Services
    .AddIdentityCore<IdentityUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.User.RequireUniqueEmail = true;
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
    })
    .AddEntityFrameworkStores<SolarWatchContext>();


        }

    }
}
