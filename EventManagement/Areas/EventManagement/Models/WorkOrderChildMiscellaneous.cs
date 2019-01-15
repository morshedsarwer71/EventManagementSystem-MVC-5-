using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.Models
{
    public class WorkOrderChildMiscellaneous
    {
        [Key]
        public int WOCMiscellaneousId { get; set; }
        public int WorkOrderId { get; set; }
        public int EventMiscellaneousId { get; set; }
        [NotMapped]
        public virtual List<EventMiscellaneous> EventMiscellaneous { get; set; }
    }
}