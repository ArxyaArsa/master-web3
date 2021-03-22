using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Warehousing.Domain.AggregateModels.ParcelAggregate;

namespace Warehousing.Infrastructure.EntityConfigurations
{
    class ParcelTypeEntityTypeConfiguration : IEntityTypeConfiguration<ParcelType>
    {
        public void Configure(EntityTypeBuilder<ParcelType> builder)
        {
            builder.ToTable("ParcelType", WarehousingContext.DEFAULT_SCHEMA);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasAnnotation(EntityConfigurationConstants.SqlServer_ValueGenerationStrategy, SqlServerValueGenerationStrategy.IdentityColumn);

            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(500);
            builder.Property(x => x.MinWeightOccupied).HasColumnType(EntityConfigurationConstants.DECIMAL_19_2).IsRequired();
            builder.Property(x => x.MaxWeight).HasColumnType(EntityConfigurationConstants.DECIMAL_19_2).IsRequired();
            builder.Property(x => x.IsPerishable).IsRequired();
            builder.Property(x => x.FreezerLifetime);
            builder.Property(x => x.DryLifetime);

            builder.HasKey(x => x.Id);
        }
    }
}
