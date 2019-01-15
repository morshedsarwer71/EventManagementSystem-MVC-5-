using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.ResponseModels
{
    public class ResponseWorkOrderParent
    {
        public int OrderId { get; set; }
        public int SerialNumber { get; set; }
        public string WorkOrderCode { get; set; }
        public string ConcernName { get; set; }
        public string ClientName { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
        public string TotalDays { get; set; }
        public int NumberOfManpower { get; set; }
        public int NoOfManpower { get; set; }
        public int NoOfSetup { get; set; }
        public int NoOfService { get; set; }
        public int NoOfPax { get; set; }
        public string Notes { get; set; }
        public decimal PerHourRate { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string CreatorName { get; set; }
        public DateTime CreationDate { get; set; }
        public string ModifierName { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}