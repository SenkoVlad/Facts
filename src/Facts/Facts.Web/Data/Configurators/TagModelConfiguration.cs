using Facts.Web.Data.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facts.Web.Data.Configurators
{
    public class TagModelConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable("Tags");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(50);

            builder.HasIndex(x => x.Name);
        }
    }
}
