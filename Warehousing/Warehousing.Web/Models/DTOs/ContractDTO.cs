using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Warehousing.Web.Models.DTOs
{
    [Serializable]
    public class ContractDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime PaymentDueUntil { get; set; }
        public bool IsPayed { get; set; }
        public DateTime AddDate { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }        // not in entity
    }
}
