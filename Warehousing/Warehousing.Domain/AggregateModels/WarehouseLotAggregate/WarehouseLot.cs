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

        private readonly List<Parcel> _parcels;
        public virtual IReadOnlyCollection<Parcel> Parcels => _parcels;

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
            if (GetValidParcels().Count > 0)
            {
                if (GetValidParcels().Sum(x => x.Weight ?? 0) > weightCapacity)
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

        public void AddParcel(int contractId, ParcelType parcelType, decimal? weight)
        {
            var actualWeight = (weight ?? 0) < parcelType.MinWeightOccupied ? parcelType.MinWeightOccupied : (weight ?? 0);

            if (actualWeight > parcelType.MaxWeight)
                throw new ArgumentOutOfRangeException("The Weight of the Parcel is too high for this Parcel Type!");

            var currentWeight = 0m;
            if (GetValidParcels().Count > 0)
                currentWeight = GetValidParcels().Sum(x => x.Weight ?? 0);

            if (currentWeight + actualWeight > WeightCapacity)
                throw new ArgumentOutOfRangeException("The Weight of the Parcel is too high and cannot fit in the Warehouse!");

            var p = new Parcel(contractId, parcelType, this, actualWeight);

            _parcels.Add(p);

            LastInventoryChange = DateTime.UtcNow;
            Occupated = currentWeight + actualWeight == WeightCapacity ? "full" : "has-items";
        }

        public void UpdateParcel(int pId, int contractId, ParcelType parcelType, decimal? weight)
        {
            var p = GetValidParcels().FirstOrDefault(x => x.Id == pId);

            var actualWeight = (weight ?? 0) < parcelType.MinWeightOccupied ? parcelType.MinWeightOccupied : (weight ?? 0);

            if (actualWeight > parcelType.MaxWeight)
                throw new ArgumentOutOfRangeException("The Weight of the Parcel is too high for this Parcel Type!");

            var currentWeight = GetValidParcels().Sum(x => x.Weight ?? 0) - (p.Weight ?? 0);

            if (currentWeight + actualWeight > WeightCapacity)
                throw new ArgumentOutOfRangeException("The Weight of the Parcel is too high and cannot fit in the Warehouse!");

            p.Update(contractId, parcelType, this, actualWeight);

            LastInventoryChange = DateTime.UtcNow;
            Occupated = currentWeight + actualWeight == WeightCapacity ? "full" : "has-items";
        }

        public void RemoveParcel(Parcel p)
        {
            p.MarkAsRemoved();

            var currentWeight = GetValidParcels().Sum(x => x.Weight ?? 0) - (p.Weight ?? 0);

            LastInventoryChange = DateTime.UtcNow;
            Occupated = currentWeight == 0 ? "empty" : "has-items";
        }

        public IReadOnlyCollection<Parcel> GetValidParcels()
        {
            return Parcels.Where(x => x.IsRemoved == false).ToList();
        }
    }
}
