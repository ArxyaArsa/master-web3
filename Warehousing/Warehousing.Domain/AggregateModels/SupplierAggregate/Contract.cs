using System;
using System.Collections.Generic;
using System.Text;
using Warehousing.Domain.SeedWork;

namespace Warehousing.Domain.AggregateModels.SupplierAggregate
{
    public class Contract : Entity
    {
        //public int Id { get; private set; }   // in Entity

        // created values
        public string Description { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime PaymentDueUntil { get; private set; }
        public bool IsPayed { get; private set; }
        public DateTime AddDate { get; private set; }

        // aggregate root reference
        public int SupplierId { get; private set; }
        public Supplier Supplier { get; private set; }

        //// other aggregate
        //public readonly List<ParcelAggregate.Parcel> _parcels;
        //public IReadOnlyCollection<ParcelAggregate.Parcel> Parcels => _parcels;

        public Contract(Supplier s, string desc, DateTime startDate, DateTime paymentDueUntil, bool isPayed = false)
        {
            Description = desc;
            StartDate = startDate;
            PaymentDueUntil = paymentDueUntil;
            IsPayed = isPayed;

            SupplierId = s.Id;
            Supplier = s;

            AddDate = DateTime.UtcNow;
        }
    }
}
