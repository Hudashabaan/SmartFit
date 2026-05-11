using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartFit.Domain.Entities;

namespace SmartFit.Infrastructure.Persistence.Configurations
{
    public class CaloriesPredictionConfiguration
        : IEntityTypeConfiguration<CaloriesPrediction>
    {
        public void Configure(EntityTypeBuilder<CaloriesPrediction> builder)
        {
            // Primary Key
            builder.HasKey(x => x.Id);

            // Properties
            builder.Property(x => x.Gender)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.WorkoutType)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.WeightKg)
                .HasPrecision(5, 2);

            builder.Property(x => x.SessionDurationHours)
                .HasPrecision(4, 2);

            builder.Property(x => x.BMI)
                .HasPrecision(5, 2);

            builder.Property(x => x.PredictedBurnedCalories)
                .HasPrecision(8, 2);

            builder.Property(x => x.WorkoutAnalysis)
                .HasMaxLength(500);

            builder.Property(x => x.WorkoutSummary)
                .HasMaxLength(500);

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            // Relationship
            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
