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
        public bool IsRemoved { get; private set; }               // !!! NOT USED
        public DateTime? RemovedDate { get; private set; }        // !!! NOT USED

        public virtual ParcelType ParcelType { get; private set; }
        public virtual WarehouseLot WarehouseLot { get; private set; }

        public Parcel() { }

        public Parcel(int contractId, ParcelType parcelType, WarehouseLot warehouse, decimal? weight)
        {
            Update(contractId, parcelType, warehouse, weight);
        }

        public void Update(int contractId, ParcelType parcelType, WarehouseLot warehouse, decimal? weight)
        {
            ContractId = contractId;
            ParcelTypeId = parcelType.Id;
            ParcelType = parcelType;
            WarehouseLotId = warehouse.Id;
            WarehouseLot = warehouse;
            Weight = weight;

            AddDate = DateTime.UtcNow;

            if (parcelType.IsPerishable)
                ValidUntil = AddDate.AddDays(warehouse.Type == "dry" ? (parcelType.DryLifetime ?? 0) : (parcelType.FreezerLifetime ?? 0));
        }

        public void MarkAsRemoved()
        {
            IsRemoved = true;
            RemovedDate = DateTime.UtcNow;
        }
    }
}
