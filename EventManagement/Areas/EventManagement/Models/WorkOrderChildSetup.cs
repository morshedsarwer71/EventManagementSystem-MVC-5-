using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.Models
{
    public class WorkOrderChildSetup
    {
        [Key]
        public int WOCSetupId { get; set; }
        [Required]
        public int WorkOrderId { get; set; }
        [Required]
        public int EventSetupId { get; set; }
        [NotMapped]
        public virtual List<EventSetup> EventSetup { get; set; }
    }
}