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
    

        public class TrainerInviteConfiguration
            : IEntityTypeConfiguration<TrainerInvite>
        {
            public void Configure(EntityTypeBuilder<TrainerInvite> builder)
            {
                builder.HasKey(x => x.Id);

                builder.Property(x => x.Code)
                    .IsRequired()
                    .HasMaxLength(20);

                builder.HasIndex(x => x.Code)
                    .IsUnique();

                builder.Property(x => x.IsUsed)
                    .HasDefaultValue(false);
            }
        }
    
}
