using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartFit.Domain.Entities;

namespace SmartFit.Infrastructure.Persistence.Configurations
{
    public class BMIRecordConfiguration
        : IEntityTypeConfiguration<BMIRecord>
    {
        public void Configure(EntityTypeBuilder<BMIRecord> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Sex)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(x => x.HeightCm)
                .IsRequired();

            builder.Property(x => x.WeightKg)
                .IsRequired();

            builder.Property(x => x.Age)
                .IsRequired();

            builder.Property(x => x.PredictedBMI)
                .IsRequired();

            builder.Property(x => x.BodyType)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.HealthStatus)
                .IsRequired()
                .HasMaxLength(50);

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