using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Warehousing.Web.Models.DTOs
{
    [Serializable]
    public class ParcelTypeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [DisplayName("Min Weight Occupied (kg)")]
        public decimal MinWeightOccupied { get; set; }

        [DisplayName("Max Weight (kg)")]
        public decimal MaxWeight { get; set; }

        [DisplayName("Is Perishable")]
        public bool IsPerishable { get; set; }

        [DisplayName("Freezer Lifetime (days)")]
        public int? FreezerLifetime { get; set; }

        [DisplayName("Dry Lifetime (days)")]
        public int? DryLifetime { get; set; }

        [DisplayName("Parcel Count")]
        public int ParcelCount { get; set; }        // calculated
    }
}
