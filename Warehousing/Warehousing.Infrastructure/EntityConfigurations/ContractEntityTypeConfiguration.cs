using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Warehousing.Domain.AggregateModels.SupplierAggregate;

namespace Warehousing.Infrastructure.EntityConfigurations
{
    class ContractEntityTypeConfiguration : IEntityTypeConfiguration<Contract>
    {
        public void Configure(EntityTypeBuilder<Contract> builder)
        {
            builder.ToTable("Contract", WarehousingContext.DEFAULT_SCHEMA);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasAnnotation(EntityConfigurationConstants.SqlServer_ValueGenerationStrategy, SqlServerValueGenerationStrategy.IdentityColumn);

            builder.Property(x => x.Description).HasMaxLength(500);
            builder.Property(x => x.StartDate).IsRequired();
            builder.Property(x => x.PaymentDueUntil).IsRequired();
            builder.Property(x => x.IsPayed).IsRequired();
            builder.Property(x => x.AddDate).IsRequired();
            builder.Property(x => x.SupplierId).IsRequired();

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Supplier)
                .WithMany(s => s.Contracts)
                .HasForeignKey(x => x.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
