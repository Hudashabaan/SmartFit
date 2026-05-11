using Microsoft.EntityFrameworkCore;
using SmartFit.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SmartFit.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<ApplicationUser> Users { get; }
        DbSet<UserProfile> UserProfiles { get; }
        DbSet<BMIRecord> BMIRecords { get; set; }
        DbSet<CaloriesPrediction> CaloriesPredictions { get; set; }

        DbSet<Exercise> Exercises { get; set; }

        DbSet<ExerciseCategory> ExerciseCategories { get; set; }

         DbSet<WorkoutPlan> WorkoutPlans { get; set; }

        DbSet<WorkoutPlanExercise> WorkoutPlanExercises { get; set; }

        DbSet<UserExerciseRecommendation> UserExerciseRecommendations { get; set; }



        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
