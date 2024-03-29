﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Warehousing.Web.Models.DTOs
{
    [Serializable]
    public class WarehouseLotDTO
    {
        public int Id { get;  set; }
        public string Name { get;  set; }
        public string Description { get;  set; }
        public string Type { get;  set; }

        [DisplayName("Occupated Status")]
        public string Occupated { get;  set; }

        [DisplayName("Weight Capacity (kg)")]
        public decimal WeightCapacity { get;  set; }
        public DateTime? LastInventoryChange { get;  set; }
        
        [DisplayName("First Name")]
        public string Manager_FirstName { get;  set; }

        [DisplayName("Last Name")]
        public string Manager_LastName { get;  set; }

        [DisplayName("Phone")]
        public string Manager_Phone { get;  set; }

        [DisplayName("Email")]
        public string Manager_Email { get;  set; }

        [DisplayName("Parcel Count")]
        public int ParcelCount { get; set; }

        [DisplayName("Parcel Total Weight (kg)")]
        public decimal ParcelWeight { get; set; }
    }
}
