using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SmartFit.API.Middleware;
using SmartFit.Application;
using SmartFit.Application.Common.Behaviors;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Application.Common.Services;
using SmartFit.Application.Features.Authentication.Commands.register;
using SmartFit.Domain.Entities;
using SmartFit.Infrastructure;
using SmartFit.Infrastructure.Identity;
using SmartFit.Infrastructure.Services;
using System.Text;
using System.Text.Json.Serialization;
using Hangfire;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using System.Threading.Tasks;

namespace SmartFit.API
{
    public class Program
    {
        public static async System.Threading.Tasks.Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // -----------------------------
            // Add services to the container
            // -----------------------------

            // Application Layer
            builder.Services.AddApplication();

            builder.Services.AddScoped<IJwtService, JwtService>();

            // Infrastructure Layer
            builder.Services.AddInfrastructure(builder.Configuration);

            builder.Services.AddHttpContextAccessor();

            // Database
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")));

            // Hangfire
            builder.Services.AddHangfire(config =>
                config.UseSqlServerStorage(
                    builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddHangfireServer();

            // Identity Service
            builder.Services.AddScoped<IIdentityService, IdentityService>();

            // FluentValidation
            builder.Services.AddFluentValidationAutoValidation();

            builder.Services.AddValidatorsFromAssembly(
                typeof(RegisterCommandValidator).Assembly);

            // MediatR Validation Pipeline
            builder.Services.AddTransient(
                typeof(IPipelineBehavior<,>),
                typeof(ValidationBehavior<,>)
            );

            // JWT Authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme =
                    JwtBearerDefaults.AuthenticationScheme;

                options.DefaultChallengeScheme =
                    JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters =
                    new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer =
                            builder.Configuration["JWT:Issuer"],

                        ValidAudience =
                            builder.Configuration["JWT:Audience"],

                        IssuerSigningKey =
                            new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(
                                    builder.Configuration["JWT:Secret"]))
                    };
            });

            // Identity Options
            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Lockout.MaxFailedAccessAttempts = 5;

                options.Lockout.DefaultLockoutTimeSpan =
                    TimeSpan.FromMinutes(5);

                options.Lockout.AllowedForNewUsers = true;
            });

            // Controllers
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters
                        .Add(new JsonStringEnumConverter());
                });

            // Firebase
            try
            {
                var path = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "Configurations",
                    "firebase-adminsdk.json");

                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile(path)
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Firebase Error: " + ex.Message);
            }

            // Swagger
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition(
                    "Bearer",
                    new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                        Scheme = "bearer",
                        BearerFormat = "JWT",
                        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                        Description =
                            "Enter 'Bearer' [space] and then your valid token"
                    });

                options.AddSecurityRequirement(
                    new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                    {
                        {
                            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                            {
                                Reference =
                                    new Microsoft.OpenApi.Models.OpenApiReference
                                    {
                                        Type =
                                            Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                        Id = "Bearer"
                                    }
                            },
                            new string[] { }
                        }
                    });
            });

            var app = builder.Build();

            // -----------------------------
            // Seed Roles & Admin Users
            // -----------------------------
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var roleManager =
                    services.GetRequiredService<RoleManager<IdentityRole>>();

                var userManager =
                    services.GetRequiredService<UserManager<ApplicationUser>>();

                // 🔥 Seed Roles
                await RoleSeeder.SeedRolesAsync(roleManager);

                // 🔥 Seed Admin + Support
                await DataSeeder.SeedAdminAsync(userManager);
            }

            // Exception Middleware
            app.UseMiddleware<ExceptionMiddleware>();

            // -----------------------------
            // HTTP Request Pipeline
            // -----------------------------

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();

                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Hangfire Dashboard
            if (!app.Environment.IsEnvironment("Migration"))
            {
                app.UseHangfireDashboard();

                var recurringJobManager =
                    app.Services.GetRequiredService<IRecurringJobManager>();

                recurringJobManager.AddOrUpdate<IBackgroundJobService>(
                    "process-schedules",
                    x => x.ProcessSchedulesAsync(),
                    Cron.Minutely);
            }

            // Authentication
            app.UseAuthentication();

            // Authorization
            app.UseAuthorization();

            // Controllers
            app.MapControllers();

            app.Run();
        }
    }
}