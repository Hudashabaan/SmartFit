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

        public class TrainerClientRelationConfiguration
            : IEntityTypeConfiguration<TrainerClientRelation>
        {
            public void Configure(EntityTypeBuilder<TrainerClientRelation> builder)
            {
                builder.HasKey(x => x.Id);

                // 🔥 Relationship مع Trainer
                builder.HasOne(x => x.Trainer)
                    .WithMany()
                    .HasForeignKey(x => x.TrainerId)
                    .OnDelete(DeleteBehavior.Restrict);

                // 🔥 Relationship مع Client
                builder.HasOne(x => x.Client)
                    .WithMany()
                    .HasForeignKey(x => x.ClientId)
                    .OnDelete(DeleteBehavior.Restrict);

                // 🔥 Unique Constraint
                builder.HasIndex(x => new { x.TrainerId, x.ClientId })
                    .IsUnique();
            }
        }
    
}
