using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Warehousing.Domain.AggregateModels.WarehouseLotAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Warehousing.Infrastructure.EntityConfigurations
{
    class WarehouseLotEntityTypeConfiguration : IEntityTypeConfiguration<WarehouseLot>
    {
        public void Configure(EntityTypeBuilder<WarehouseLot> builder)
        {
            builder.ToTable("WarehouseLot", WarehousingContext.DEFAULT_SCHEMA);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasAnnotation(EntityConfigurationConstants.SqlServer_ValueGenerationStrategy, SqlServerValueGenerationStrategy.IdentityColumn);

            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(500);
            builder.Property(x => x.Type).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Occupated).HasMaxLength(50).IsRequired();
            builder.Property(x => x.WeightCapacity).HasColumnType(EntityConfigurationConstants.DECIMAL_19_2).IsRequired();
            builder.Property(x => x.LastInventoryChange);

            builder.OwnsOne(x => x.Manager, y =>
            {
                y.Property(z => z.FirstName).HasMaxLength(100).IsRequired().HasColumnName("Manager_FirstName");
                y.Property(z => z.LastName).HasMaxLength(100).IsRequired().HasColumnName("Manager_LastName");
                y.Property(z => z.Phone).HasMaxLength(100).HasColumnName("Manager_Phone");
                y.Property(z => z.Email).HasMaxLength(100).HasColumnName("Manager_Email");
            });

            builder.HasKey(x => x.Id);
        }
    }
}
