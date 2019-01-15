using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.ResponseModels
{
    public class ResponseEventClient
    {
        public int EventClientId { get; set; }
        public int SerialNumber { get; set; }
        public string ConcernName { get; set; }
        public string ClientNameEN { get; set; }
        public string ClientNameAR { get; set; }
        public string ClientContactInfo { get; set; }
        public string ClientAddress { get; set; }
    }
}