using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SmartFit.API.Extensions;
using SmartFit.Application;
using SmartFit.Application.Common.Behaviors;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Application.Features.Authentication.Commands.register;
using SmartFit.Domain.Entities;
using SmartFit.Infrastructure;
using SmartFit.Infrastructure.Identity;
using SmartFit.Infrastructure.Persistence;
using SmartFit.Infrastructure.Services;

using System.Text;
using System.Text.Json.Serialization;

namespace SmartFit.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // =========================================
            // Application Layer
            // =========================================

            builder.Services.AddApplication();

            // =========================================
            // Infrastructure Layer
            // =========================================

            builder.Services.AddInfrastructure(
                builder.Configuration);

            // =========================================
            // Database
            // =========================================

            builder.Services.AddDbContext<ApplicationDbContext>(
                options =>
                {
                    options.UseSqlServer(
                        builder.Configuration
                            .GetConnectionString(
                                "DefaultConnection"),
                        b => b.MigrationsAssembly(
                            "SmartFit.Infrastructure"));
                });

            // =========================================
            // Http Context Accessor
            // =========================================

            builder.Services.AddHttpContextAccessor();

            // =========================================
            // FluentValidation
            // =========================================

            builder.Services
                .AddFluentValidationAutoValidation();

            builder.Services
                .AddValidatorsFromAssembly(
                    typeof(RegisterCommandValidator)
                        .Assembly);

            // =========================================
            // MediatR Pipeline Behaviors
            // =========================================

            builder.Services.AddTransient(
                typeof(IPipelineBehavior<,>),
                typeof(ValidationBehavior<,>)
            );

            // =========================================
            // AI Services
            // =========================================

            builder.Services.AddHttpClient<
                IBMIPredictionService,
                BMIPredictionService>(client =>
                {
                    client.BaseAddress = new Uri(
                        builder.Configuration
                        ["AIModels:BMIPredictionUrl"]!);
                });

            builder.Services.AddHttpClient<
                ICaloriesPredictionService,
                CaloriesPredictionService>(client =>
                {
                    client.BaseAddress = new Uri(
                        builder.Configuration
                        ["AIModels:CaloriesPredictionUrl"]!);
                });

            builder.Services.AddHttpClient<
                IExerciseRecommendationAiService,
                ExerciseRecommendationAiService>(client =>
                {
                    client.BaseAddress = new Uri(
                        builder.Configuration
                        ["AIModels:ExerciseRecommendationUrl"]!);
                });

            // =========================================
            // JWT Authentication
            // =========================================

            builder.Services
                .AddAuthentication(options =>
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
                                        builder.Configuration["JWT:Secret"]!
                                    ))
                        };
                });

            // =========================================
            // Identity Options
            // =========================================

            builder.Services.Configure<IdentityOptions>(
                options =>
                {
                    options.Password.RequireDigit = true;

                    options.Password.RequireUppercase = false;

                    options.Password.RequireLowercase = false;

                    options.Password.RequireNonAlphanumeric = false;

                    options.Password.RequiredLength = 6;

                    options.Lockout.MaxFailedAccessAttempts = 5;

                    options.Lockout.DefaultLockoutTimeSpan =
                        TimeSpan.FromMinutes(5);

                    options.Lockout.AllowedForNewUsers = true;
                });

            // =========================================
            // Controllers
            // =========================================

            builder.Services
                .AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions
                        .Converters
                        .Add(new JsonStringEnumConverter());
                });

            // =========================================
            // Swagger
            // =========================================

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(
                    "v1",
                    new OpenApiInfo
                    {
                        Title = "SmartFit API",
                        Version = "v1"
                    });

                options.AddSecurityDefinition(
                    "Bearer",
                    new OpenApiSecurityScheme
                    {
                        Name = "Authorization",

                        Type = SecuritySchemeType.Http,

                        Scheme = "bearer",

                        BearerFormat = "JWT",

                        In = ParameterLocation.Header,

                        Description =
                            "Enter Bearer Token"
                    });

                options.AddSecurityRequirement(
                    new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference =
                                    new OpenApiReference
                                    {
                                        Type =
                                            ReferenceType
                                                .SecurityScheme,

                                        Id = "Bearer"
                                    }
                            },

                            Array.Empty<string>()
                        }
                    });
            });

            var app = builder.Build();

            // =========================================
            // Seed Roles & Admin
            // =========================================

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var roleManager =
                    services.GetRequiredService<
                        RoleManager<IdentityRole>>();

                var userManager =
                    services.GetRequiredService<
                        UserManager<ApplicationUser>>();

                RoleSeeder.SeedRolesAsync(roleManager)
                    .Wait();
            }

            // =========================================
            // Global Exception Middleware
            // =========================================

            app.UseCustomExceptionHandler();

            // =========================================
            // Swagger
            // =========================================

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();

                app.UseSwaggerUI();
            }

            // =========================================
            // HTTPS
            // =========================================

            app.UseHttpsRedirection();

            // =========================================
            // Authentication & Authorization
            // =========================================

            app.UseAuthentication();

            app.UseAuthorization();

            // =========================================
            // Controllers
            // =========================================

            app.MapControllers();

            app.Run();
        }
    }
}