using System;
using System.Collections.Generic;
using System.Text;
using Warehousing.Domain.SeedWork;

namespace Warehousing.Domain.AggregateModels.SupplierAggregate
{
    public class Contract : Entity
    {
        //public int Id { get; private set; }
        public int SupplierId { get; private set; }
        public string Description { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime PaymentDueUntil { get; private set; }
        public bool IsPayed { get; private set; }
        public DateTime AddDate { get; private set; }

        public Supplier Supplier { get; private set; }

        public readonly List<ParcelAggregate.Parcel> _parcels;
        public IReadOnlyCollection<ParcelAggregate.Parcel> Parcels => _parcels;
    }
}
