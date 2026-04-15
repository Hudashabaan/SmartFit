using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SmartFit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFit.Infrastructure.Persistence.Configurations
{
    public class FoodAnalysisConfiguration : IEntityTypeConfiguration<FoodAnalysis>
    {
        public void Configure(EntityTypeBuilder<FoodAnalysis> builder)
        {
            builder.ToTable("FoodAnalyses");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ImageUrl)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.FoodName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Calories).IsRequired();
            builder.Property(x => x.Protein).IsRequired();
            builder.Property(x => x.Carbs).IsRequired();
            builder.Property(x => x.Fat).IsRequired();

            builder.Property(x => x.Confidence)
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.Property(x => x.UserId)
                .IsRequired();
        }
    }
}
