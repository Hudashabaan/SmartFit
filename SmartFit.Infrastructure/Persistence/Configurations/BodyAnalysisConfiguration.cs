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

    public class BodyAnalysisConfiguration : IEntityTypeConfiguration<BodyAnalysis>
    {
        public void Configure(EntityTypeBuilder<BodyAnalysis> builder)
        {
            builder.HasKey(x => x.Id);

            // ممكن نزود علاقات بعدين مع User لو محتاجين
        }
    }
}
