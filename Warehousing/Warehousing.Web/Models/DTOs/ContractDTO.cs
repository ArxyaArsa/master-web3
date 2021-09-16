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

        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; }

        [DisplayName("Payment Due Until")]
        public DateTime PaymentDueUntil { get; set; }

        [DisplayName("Is Payed")]
        public bool IsPayed { get; set; }

        [DisplayName("Date Added")]
        public DateTime AddDate { get; set; }

        public int SupplierId { get; set; }
        
        [DisplayName("Supplier")]
        public string SupplierName { get; set; }        // not in entity
    }
}
