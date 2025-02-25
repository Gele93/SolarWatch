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
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using SolarWatch.Data.Seeder;
using SolarWatch.Services.CityServices;
using SolarWatch.Services.SunMovementServices;
using SolarWatch.Data.DataImport;
using SolarWatch.Services.CityNameServices;

namespace SolarWatch
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            AddServices(builder);
            ConfigureSwagger(builder);
            AddDbContext(builder, configuration);
            AddAuthentication(builder, configuration);
            AddAuthorizationPolicies(builder, configuration);
            AddIdentity(builder);

            var app = builder.Build();

            using var scope = app.Services.CreateScope();
            var authenticationSeeder = scope.ServiceProvider.GetRequiredService<AuthenticationSeeder>();
            authenticationSeeder.AddRoles();
            authenticationSeeder.AddAdmin();

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
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<ICityService, CityServices>();
            builder.Services.AddScoped<ISunMovementService, SunMovementService>();
            builder.Services.AddScoped<ICityNameRepository, CityNameRepository>();
            builder.Services.AddScoped<ICityNameService, CityNameService>(); 
            builder.Services.AddScoped<AuthenticationSeeder>();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        }
        private static void ConfigureSwagger(WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Demo API",
                    Version = "v1"
                });

                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
            });

        }
        private static void AddDbContext(WebApplicationBuilder builder, ConfigurationManager configuration)
        {
            builder.Services.AddDbContext<SolarWatchContext>(options =>
            {
                var connectionString = configuration["SolarWatch_connection"];
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

            if (string.IsNullOrEmpty(secretKey))
            {
                throw new InvalidOperationException("JWT SecretKey is missing.");
            }

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

        private static void AddAuthorizationPolicies(WebApplicationBuilder builder, ConfigurationManager configuration)
        {
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireUserOrAdmin", policy =>
                    policy.RequireRole(configuration["Roles:User"], configuration["Roles:Admin"]));
                options.AddPolicy("RequireAdmin", policy =>
                    policy.RequireRole(configuration["Roles:Admin"]));
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
    .AddRoles<IdentityRole>() //Enable Identity roles 
    .AddEntityFrameworkStores<SolarWatchContext>();


        }

    }
}
