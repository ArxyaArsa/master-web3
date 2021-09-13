using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Warehousing.Domain.AggregateModels.WarehouseLotAggregate;

namespace Warehousing.Infrastructure.EntityConfigurations
{
    class ParcelEntityTypeConfiguration : IEntityTypeConfiguration<Parcel>
    {
        public void Configure(EntityTypeBuilder<Parcel> builder)
        {
            builder.ToTable("Parcel", WarehousingContext.DEFAULT_SCHEMA);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasAnnotation(EntityConfigurationConstants.SqlServer_ValueGenerationStrategy, SqlServerValueGenerationStrategy.IdentityColumn);

            builder.Property(x => x.ContractId).IsRequired();
            builder.Property(x => x.ParcelTypeId).IsRequired();
            builder.Property(x => x.WarehouseLotId).IsRequired();
            builder.Property(x => x.Weight).HasColumnType(EntityConfigurationConstants.DECIMAL_19_2);
            builder.Property(x => x.AddDate).IsRequired();
            builder.Property(x => x.ValidUntil);
            builder.Property(x => x.IsRemoved).IsRequired();
            builder.Property(x => x.RemovedDate);

            builder.HasKey(x => x.Id);

            //builder.HasOne("Contract")
            //    .WithMany()
            //    .HasForeignKey("ContractId")
            //    .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.WarehouseLot)
                .WithMany(x => x.Parcels)
                .HasForeignKey(x => x.WarehouseLotId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ParcelType)
                .WithMany(x => x.Parcels)
                .HasForeignKey(x => x.ParcelTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
