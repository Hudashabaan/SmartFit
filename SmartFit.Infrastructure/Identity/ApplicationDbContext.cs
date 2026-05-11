using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Domain.Entities;


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

        public DbSet<ApplicationUser> Users { get; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<BMIRecord> BMIRecords { get; set; }
        public DbSet<CaloriesPrediction> CaloriesPredictions { get; set; }

        // =========================
        // Exercise Recommendation
        // =========================

        public DbSet<Exercise> Exercises { get; set; }

        public DbSet<ExerciseCategory> ExerciseCategories { get; set; }

        public DbSet<WorkoutPlan> WorkoutPlans { get; set; }

        public DbSet<WorkoutPlanExercise> WorkoutPlanExercises { get; set; }

        public DbSet<UserExerciseRecommendation> UserExerciseRecommendations { get; set; }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
            => await base.SaveChangesAsync(cancellationToken);

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}