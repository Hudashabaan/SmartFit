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
    public class ExerciseConfiguration : IEntityTypeConfiguration<Exercise>
    {
        public void Configure(EntityTypeBuilder<Exercise> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(e => e.Level)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.FitnessGoal)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.FitnessType)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Equipment)
                .HasMaxLength(200);

            builder.HasIndex(e => e.Level);

            builder.HasIndex(e => e.FitnessGoal);

            builder.HasIndex(e => e.FitnessType);

            builder.HasOne(e => e.Category)
                .WithMany(c => c.Exercises)
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
