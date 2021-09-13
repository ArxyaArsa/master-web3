using System;

namespace Warehousing.Web.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public string ErrorDetails { get; set; }

        public ErrorViewModel() { }

        public ErrorViewModel(string errorDetails)
        {
            ErrorDetails = errorDetails;
        }
    }
}