using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Warehousing.Web.Models
{
    public class ModalViewModel
    {
        public string UniqueClass { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string BodyURL { get; set; }         // url to get the body from
        public bool HasSaveButton { get; set; } = true;
        public string SaveButtonText { get; set; } = "Save changes";
        public bool HasCancelButton { get; set; } = true;
        public string CancelButtonText { get; set; } = "Cancel";
    }
}
