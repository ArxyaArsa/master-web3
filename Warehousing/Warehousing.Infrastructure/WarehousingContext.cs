using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Text;
using Warehousing.Domain.AggregateModels.SupplierAggregate;
using Warehousing.Domain.AggregateModels.WarehouseLotAggregate;
using Warehousing.Infrastructure.EntityConfigurations;

namespace Warehousing.Infrastructure
{
    public class WarehousingContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "dbo";

        public DbSet<Parcel> Parcels { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<ParcelType> ParcelTypes { get; set; }
        public DbSet<WarehouseLot> WarehouseLots { get; set; }

        public WarehousingContext(DbContextOptions<WarehousingContext> options) : base(options)
        {
            System.Diagnostics.Debug.WriteLine("WarehousingContext::ctor ->" + this.GetHashCode());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0")
                .HasAnnotation(EntityConfigurationConstants.Relational_MaxIdentifierLength, 128)
                .HasAnnotation(EntityConfigurationConstants.SqlServer_ValueGenerationStrategy, SqlServerValueGenerationStrategy.IdentityColumn)
                //.ForSqlServerUseSequenceHiLo()
                ;

            modelBuilder.ApplyConfiguration(new ParcelEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ContractEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SupplierEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ParcelTypeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new WarehouseLotEntityTypeConfiguration());

            System.Diagnostics.Debug.WriteLine("WarehousingContext::OnModelCreating ->" + this.GetHashCode());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.EnableSensitiveDataLogging();

            System.Diagnostics.Debug.WriteLine("WarehousingContext::OnConfiguring ->" + this.GetHashCode());
        }
    }
}
