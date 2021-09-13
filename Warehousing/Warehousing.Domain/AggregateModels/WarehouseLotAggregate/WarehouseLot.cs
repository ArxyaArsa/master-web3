using System;
using System.Collections.Generic;
using System.Text;
using Warehousing.Domain.SeedWork;

namespace Warehousing.Domain.AggregateModels.WarehouseLotAggregate
{
    public class WarehouseLot : Entity, IAggregateRoot
    {
        //public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Type { get; private set; }            // 'freezer', 'dry'
        public string Occupated { get; private set; }       // 'empty', 'has-items', 'full' 
        public decimal WeightCapacity { get; private set; }
        public DateTime? LastInventoryChange { get; private set; }
        public WarehouseLotManager Manager { get; private set; }

        public readonly List<Parcel> _parcels;
        public virtual IReadOnlyCollection<Parcel> Parcels { get { return _parcels; } }

        public WarehouseLot() { }

        public WarehouseLot(string name, string desc, string type, string occupated, decimal weightCapacity, string manFirstName, string manLastName, string manEmail, string manPhone) : this()
        {
            Name = name;
            Description = desc;
            Type = type;
            Occupated = occupated;
            WeightCapacity = weightCapacity;
            Manager = new WarehouseLotManager(manFirstName, manLastName, manEmail, manPhone);
            _parcels = new List<Parcel>();
        }
    }
}
