using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Warehousing.Web.Models.DTOs
{
    public class WarehouseLotDTO
    {
        public int Id { get;  set; }
        public string Name { get;  set; }
        public string Description { get;  set; }
        public string Type { get;  set; }
        public string Occupated { get;  set; }
        public decimal WeightCapacity { get;  set; }
        public DateTime? LastInventoryChange { get;  set; }
        public string Manager_FirstName { get;  set; }
        public string Manager_LastName { get;  set; }
        public string Manager_Phone { get;  set; }
        public string Manager_Email { get;  set; }
        public int ParcelCount { get; set; }
        public decimal ParcelWeight { get; set; }
    }
}
