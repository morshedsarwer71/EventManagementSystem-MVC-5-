using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.Models
{
    public class WorkOrderDailyManpower
    {
        [Key]
        public int WOCManpowerId { get; set; }
        [Required]
        public int WorkOrderId { get; set; }
        public decimal PerHourRate { get; set; }
        [Required(ErrorMessage = "Employee name required")]
        public int EmployeeId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [NotMapped]
        public bool IsChecked { get; set; }
        [NotMapped]
        public virtual List<Employee> Employee { get; set; }
    }
}