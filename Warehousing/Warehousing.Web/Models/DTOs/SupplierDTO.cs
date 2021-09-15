using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Warehousing.Web.Models.DTOs
{
    [Serializable]
    public class SupplierDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [DisplayName("First Name")]
        public string Contact_FirstName { get; set; }

        [DisplayName("Last Name")]
        public string Contact_LastName { get; set; }

        public string Contact_Phone { get; set; }

        [DisplayName("Email")]
        public string Contact_Email { get; set; }

        [DisplayName("Fax")]
        public string Contact_Fax { get; set; }


        [DisplayName("State")]
        public string Address_State { get; set; }

        [DisplayName("Country")]
        public string Address_Country { get; set; }

        [DisplayName("Postal Code")]
        public string Address_PostalCode { get; set; }

        [DisplayName("Address Line 1")]
        public string Address_AddressLine1 { get; set; }

        [DisplayName("Address Line 2")]
        public string Address_AddressLine2 { get; set; }


        [DisplayName("Date Added")]
        public DateTime AddDate { get; set; }

        [DisplayName("First Contract Date")]
        public DateTime? FirstContractDate { get; set; }

        [DisplayName("Last Contract Date")]
        public DateTime? LastContractDate { get; set; }
    }
}
