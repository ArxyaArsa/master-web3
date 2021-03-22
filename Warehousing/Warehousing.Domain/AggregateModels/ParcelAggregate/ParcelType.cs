using System;
using System.Collections.Generic;
using System.Text;
using Warehousing.Domain.SeedWork;

namespace Warehousing.Domain.AggregateModels.ParcelAggregate
{
    public class ParcelType : Entity
    {
        //public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal MinWeightOccupied { get; private set; }
        public decimal MaxWeight { get; private set; }
        public bool IsPerishable { get; private set; }
        public int? FreezerLifetime { get; private set; }
        public int? DryLifetime { get; private set; }

        public virtual IReadOnlyCollection<Parcel> Parcels { get; set; }
    }
}
