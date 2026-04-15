using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Domain.Entities;
using SmartFit.Domain.Entities.SmartFit.Domain.Entities;

namespace SmartFit.Infrastructure.Identity
{
    public class ApplicationDbContext
        : IdentityDbContext<ApplicationUser>,
          IApplicationDbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<BodyAnalysis> BodyAnalyses { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<FoodAnalysis> FoodAnalyses { get; set; }
        public DbSet<Goal> Goals { get; set; }

        public DbSet<HealthSchedule> HealthSchedules { get; set; }
        public DbSet<NotificationHistory> NotificationHistories { get; set; }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
            => await base.SaveChangesAsync(cancellationToken);

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}