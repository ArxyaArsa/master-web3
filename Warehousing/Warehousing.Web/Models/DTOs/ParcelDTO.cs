using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Warehousing.Web.Models.DTOs
{
    [Serializable]
    public class ParcelDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ContractId { get; set; }
        public string ContractName { get; set; }        // not in entity
        public int ParcelTypeId { get; set; }
        public string ParcelTypeName { get; set; }      // not in entity
        public int WarehouseLotId { get; set; }
        public decimal? Weight { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime? ValidUntil { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime? RemovedDate { get; set; }


        //public ParcelType ParcelType { get; private set; }
        //public WarehouseLotDTO WarehouseLot { get; private set; }

        //[DisplayName("Parcel Total Weight (kg)")]
    }
}
