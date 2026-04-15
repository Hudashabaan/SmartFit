using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Infrastructure.Identity;
using SmartFit.Infrastructure.Persistence;
using SmartFit.Domain.Entities;
using SmartFit.Infrastructure.Services;
using SmartFit.Infrastructure.AI;
using SmartFit.Application.Common.Services;

namespace SmartFit.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IApplicationDbContext>(provider =>
    provider.GetRequiredService<ApplicationDbContext>());

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IIdentityService, IdentityService>();

            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IAIService, AIService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IFoodRecognitionService, FoodRecognitionService>();
            services.AddScoped<IImageService, CloudinaryService>();
            services.AddScoped<IBackgroundJobService, BackgroundJobService>();
            services.AddScoped<INotificationService, FirebaseNotificationService>();
            services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}
