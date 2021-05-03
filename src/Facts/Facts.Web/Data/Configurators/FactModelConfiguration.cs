using Facts.Web.Data.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facts.Web.Data.Configurators
{
    public class FactModelConfiguration : IEntityTypeConfiguration<Fact>
    {
        public void Configure(EntityTypeBuilder<Fact> builder)
        {
            builder.ToTable("Facts");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id);
            builder.Property(x => x.Content).HasMaxLength(3000).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.CreatedBy).IsRequired().HasMaxLength(50);
            builder.Property(x => x.UpdatedAt);
            builder.Property(x => x.UpdatedBy).HasMaxLength(50);
            builder.HasIndex(x => x.Content);

            builder.HasMany(x => x.Tags).WithMany(x => x.Facts);
        }
    }
}
