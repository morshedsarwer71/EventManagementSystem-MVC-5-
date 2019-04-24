using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.Models
{
    public class WorkOrderChildManpower
    {
        [Key]
        public int WOCManpowerId { get; set; }
        [Required]
        public int WorkOrderId { get; set; }
        [Required(ErrorMessage = "Employee name required")]
        public int EmployeeId { get; set; }
        [Required]
        public DateTime FromDate { get; set; }
        [Required]
        public DateTime ToDate { get; set; }
        [NotMapped]
        public bool IsChecked { get; set; }
        [NotMapped]
        public virtual List<Employee> Employee { get; set; }
        [NotMapped]
        public virtual List<EventManagementClient> Clients { get; set; }
    }
}