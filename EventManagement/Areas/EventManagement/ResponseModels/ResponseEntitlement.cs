using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.ResponseModels
{
    public class ResponseEntitlement
    {
        public int EntitlementId { get; set; }
        public int SerialNumber { get; set; }
        public string EntitlementCode { get; set; }
        public string EmployeeTypeEN { get; set; }
        public string EmployeeTypeAR { get; set; }
    }
}