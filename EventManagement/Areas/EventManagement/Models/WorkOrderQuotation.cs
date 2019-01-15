using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.Models
{
    public class WorkOrderQuotation
    {
        [Key]
        public int QuotationWorkOrderId { get; set; }
        public string QuotationCode { get; set; }
        public int QuotationStatus { get; set; }
        public int ClientId { get; set; }
        public int VATCode { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
        public decimal PerHourRate { get; set; }
        public decimal VATValue { get; set; }
        public int NoOfManpower { get; set; }
        public int NoOfPax { get; set; }
        public int NoOfService { get; set; }
        public int NoOfSetup { get; set; }
        public string Notes { get; set; }
        public DateTime CreationDate { get; set; }
        public int CreatorId { get; set; }
        public DateTime ModificationDate { get; set; }
        public int ModifierId { get; set; }
        public int IsDelete { get; set; }
        public string Description { get; set; }
    }
}