using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartFit.Domain.Entities;

namespace SmartFit.Infrastructure.Configurations
{
    public class WorkoutPlanExerciseConfiguration
        : IEntityTypeConfiguration<WorkoutPlanExercise>
    {
        public void Configure(EntityTypeBuilder<WorkoutPlanExercise> builder)
        {
            builder.HasKey(wp => new
            {
                wp.WorkoutPlanId,
                wp.ExerciseId
            });

            builder.HasOne(wp => wp.WorkoutPlan)
                .WithMany(w => w.WorkoutPlanExercises)
                .HasForeignKey(wp => wp.WorkoutPlanId);

            builder.HasOne(wp => wp.Exercise)
                .WithMany(e => e.WorkoutPlanExercises)
                .HasForeignKey(wp => wp.ExerciseId);
        }
    }
}
