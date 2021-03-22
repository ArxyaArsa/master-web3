using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Warehousing.Domain.AggregateModels.SupplierAggregate;

namespace Warehousing.Infrastructure.EntityConfigurations
{
    class SupplierEntityTypeConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.ToTable("Supplier", WarehousingContext.DEFAULT_SCHEMA);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasAnnotation(EntityConfigurationConstants.SqlServer_ValueGenerationStrategy, SqlServerValueGenerationStrategy.IdentityColumn);

            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(500);
            builder.Property(x => x.AddDate).IsRequired();
            builder.Property(x => x.FirstContractDate);
            builder.Property(x => x.LastContractDate);

            builder.OwnsOne(x => x.Contact, y =>
            {
                y.Property(z => z.FirstName).HasMaxLength(100).HasColumnName("Contact_FirstName");
                y.Property(z => z.LastName).HasMaxLength(100).HasColumnName("Contact_LastName");
                y.Property(z => z.Phone).HasMaxLength(100).HasColumnName("Contact_Phone");
                y.Property(z => z.Email).HasMaxLength(100).HasColumnName("Contact_Email");
                y.Property(z => z.Fax).HasMaxLength(100).HasColumnName("Contact_Fax");
            });

            builder.OwnsOne(x => x.Address, y =>
            {
                y.Property(z => z.State).HasMaxLength(100).HasColumnName("Address_State");
                y.Property(z => z.Country).HasMaxLength(100).HasColumnName("Address_Country");
                y.Property(z => z.PostalCode).HasMaxLength(100).HasColumnName("Address_PostalCode");
                y.Property(z => z.AddressLine1).HasMaxLength(100).HasColumnName("Address_AddressLine1");
                y.Property(z => z.AddressLine2).HasMaxLength(100).HasColumnName("Address_AddressLine2");
            });

            builder.HasKey(x => x.Id);
        }
    }
}
