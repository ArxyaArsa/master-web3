using System;
using System.Collections.Generic;
using System.Linq;
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

        public WarehouseLot() 
        {
            _parcels = new List<Parcel>(); 
        }

        public WarehouseLot(string name, string desc, string type, string occupated, decimal weightCapacity, string manFirstName, string manLastName, string manEmail, string manPhone) : this()
        {
            Name = name;
            Description = desc;
            Type = type;
            Occupated = occupated ?? "empty";
            WeightCapacity = weightCapacity;
            Manager = new WarehouseLotManager(manFirstName, manLastName, manEmail, manPhone);
        }

        public void UpdateName(string name)
        {
            Name = name;
        }

        public void UpdateDescription(string desc)
        {
            Description = desc;
        }

        public void UpdateType(string type)
        {
            Type = type;
        }

        public void UpdateWeightCapacity(decimal weightCapacity)
        {
            if (_parcels.Count > 0)
            {
                if (_parcels.Sum(x => x.Weight ?? 0) > weightCapacity)
                    throw new ArgumentOutOfRangeException("Error! The new Warehouse Weight Capacity value is less than the total Weight of all parcels in it! Either remove parcels or increase the Weight Capacity!");
                else
                    WeightCapacity = weightCapacity;
            }
            else
                WeightCapacity = weightCapacity;
        }

        public void UpdateManagerInfo(string fName, string lName, string email, string phone)
        {
            Manager = new WarehouseLotManager(fName, lName, email, phone);
        }
    }
}
