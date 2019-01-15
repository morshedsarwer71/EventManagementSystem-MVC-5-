using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.Models
{
    public class WorkOrderChildService
    {
        [Key]
        public int WOCServiceId { get; set; }
        [Required]
        public int WorkOrderId { get; set; }
        [Required(ErrorMessage = "Choose event service name for this project")]
        public int EventServiceId { get; set; }
        [NotMapped]
        public virtual List<EventService> EventService { get; set; }
    }
}