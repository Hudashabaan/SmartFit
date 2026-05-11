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
    public class UserExerciseRecommendationConfiguration
        : IEntityTypeConfiguration<UserExerciseRecommendation>
    {
        public void Configure(
            EntityTypeBuilder<UserExerciseRecommendation> builder)
        {
            builder.HasKey(r => r.Id);

            // =========================
            // Properties
            // =========================

            builder.Property(r => r.RecommendedExercises)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(r => r.RecommendedEquipment)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(r => r.RecommendationReason)
                .HasMaxLength(500);

            // =========================
            // Index
            // =========================

            builder.HasIndex(r => r.RecommendedAt);

            // =========================
            // Relationships
            // =========================

            builder.HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
