using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.Models
{
    public class WorkOrderStatus
    {
        [Key]
        public int OrderStatusId { get; set; }
        [Required]
        public string OrderStates { get; set; }
    }
}