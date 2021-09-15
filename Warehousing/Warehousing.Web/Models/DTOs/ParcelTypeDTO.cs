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
        public decimal MinWeightOccupied { get; set; }
        public decimal MaxWeight { get; set; }
        public bool IsPerishable { get; set; }
        public int? FreezerLifetime { get; set; }
        public int? DryLifetime { get; set; }

        public int ParcelCount { get; set; }        // calculated
    }
}
