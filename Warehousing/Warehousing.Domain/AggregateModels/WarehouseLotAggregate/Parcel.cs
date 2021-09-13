using System;
using System.Collections.Generic;
using System.Text;
using Warehousing.Domain.SeedWork;

namespace Warehousing.Domain.AggregateModels.WarehouseLotAggregate
{
    public class Parcel : Entity, IAggregateRoot
    {
        //public int Id { get; private set; }
        public int ContractId { get; private set; }         // only fk - no ref obj
        public int ParcelTypeId { get; private set; }
        public int WarehouseLotId { get; private set; }     // only fk - no ref obj
        public decimal? Weight { get; private set; }
        public DateTime AddDate { get; private set; }
        public DateTime? ValidUntil { get; private set; }
        public bool IsRemoved { get; private set; }
        public DateTime? RemovedDate { get; private set; }

        public virtual ParcelType ParcelType { get; private set; }
        public virtual WarehouseLot WarehouseLot { get; private set; }

        public Parcel() { }
    }
}
