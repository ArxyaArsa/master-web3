using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Warehousing.Domain.SeedWork;

namespace Warehousing.Domain.AggregateModels.WarehouseLotAggregate
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

        private readonly List<Parcel> _parcels;
        public virtual IReadOnlyCollection<Parcel> Parcels => _parcels;

        public ParcelType()
        {
            _parcels = new List<Parcel>();
        }

        public ParcelType(string name, string desc, decimal minWeightOccupied, decimal maxWeight, bool isPerishable, int? freezerLifetime, int? dryLifetime) : this()
        {
            Update(name, desc, minWeightOccupied, maxWeight, isPerishable, freezerLifetime, dryLifetime);
        }

        public void Update(string name, string desc, decimal minWeightOccupied, decimal maxWeight, bool isPerishable, int? freezerLifetime, int? dryLifetime)
        {
            Name = name ?? throw new ArgumentNullException("Parcel Type Name cannot be empty!");
            Description = desc ?? throw new ArgumentNullException("Parcel Type Description cannot be empty!");

            if ((Id != 0 && GetValidParcels().Count == 0) || Id == 0)
            {
                if (minWeightOccupied <= 0)
                    throw new ArgumentOutOfRangeException("Parcel Type Min Weight Occupied must be greater than 0 (zero) !");

                MinWeightOccupied = minWeightOccupied;

                if (minWeightOccupied >= maxWeight)
                    throw new ArgumentOutOfRangeException("Parcel Type Max Weight must be greater than Min Weight Occupied!");

                MaxWeight = maxWeight;
                IsPerishable = isPerishable;

                if (isPerishable)
                {
                    if (freezerLifetime == null)
                        throw new ArgumentNullException("Freezer Lifetime cannot be empty if the Parcel Type is marked as Perishable!");

                    if (freezerLifetime < 0)
                        throw new ArgumentOutOfRangeException("Freezer Lifetime cannot be a negative number!");

                    if (dryLifetime == null)
                        throw new ArgumentNullException("Dry Lifetime cannot be empty if the Parcel Type is marked as Perishable!");

                    if (dryLifetime < 0)
                        throw new ArgumentOutOfRangeException("Dry Lifetime cannot be a negative number!");

                    FreezerLifetime = freezerLifetime;
                    DryLifetime = dryLifetime;
                }
            }
        }

        public IReadOnlyCollection<Parcel> GetValidParcels()
        {
            return Parcels.Where(x => x.IsRemoved == false).ToList();
        }
    }
}
