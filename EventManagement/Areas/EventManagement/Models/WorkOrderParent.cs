using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.Models
{
    public class WorkOrderParent
    {
        [Key]
        public int WorkOrderId { get; set; }
        public int ConcernId { get; set; }
        public string OrderCode { get; set; }
        public int VATCode { get; set; }
        public int ClientId { get; set; }
        public int NoOfManpower { get; set; }
        public int NoOfSetup { get; set; }
        public int NoOfService { get; set; }
        public int NoOfPax { get; set; }
        public int PaymentStatus { get; set; }
        public string Notes { get; set; }
        public decimal PerHourRate { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
        public int Status { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public int CreatorId { get; set; }
        public DateTime ModificationDate { get; set; }
        public int ModifierId { get; set; }
        public int IsDelete { get; set; }
    }
}