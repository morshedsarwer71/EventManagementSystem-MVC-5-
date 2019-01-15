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
        [Required(AllowEmptyStrings = false, ErrorMessage = "Concern Name Required")]
        public int ConcernId { get; set; }
        public string OrderCode { get; set; }
        [Required(ErrorMessage = "Vat code required")]
        public string VATCode { get; set; }
        [Required(ErrorMessage = "Client Name Required")]
        public int ClientId { get; set; }
        public int NoOfManpower { get; set; }
        public int NoOfSetup { get; set; }
        public int NoOfService { get; set; }
        public int NoOfPax { get; set; }
        public int PaymentStatus { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Notes Required")]
        public string Notes { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Per Hour Rate Required")]
        public decimal PerHourRate { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Starting Date Required")]
        [DataType(DataType.DateTime)]
        public DateTime StartingDate { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Ending Date Required")]
        [DataType(DataType.DateTime)]
        public DateTime EndingDate { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Status Required")]
        public int Status { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Description Required")]
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public int CreatorId { get; set; }
        public DateTime ModificationDate { get; set; }
        public int ModifierId { get; set; }
        public int IsDelete { get; set; }
    }
}