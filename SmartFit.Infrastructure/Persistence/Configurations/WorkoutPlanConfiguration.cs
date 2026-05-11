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
    public class WorkoutPlanConfiguration
        : IEntityTypeConfiguration<WorkoutPlan>
    {
        public void Configure(EntityTypeBuilder<WorkoutPlan> builder)
        {
            builder.HasKey(w => w.Id);

            builder.Property(w => w.Title)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(w => w.Goal)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(w => w.Level)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
