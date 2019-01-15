using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.Models
{
    public class EmployeesMiscellaneousAssignment
    {
        [Key]
        public int MiscellaneousAssignmentID { get; set; }
        public int WorkOrderId { get; set; }
        public int EmployeeId { get; set; }
        [NotMapped]
        public virtual List<WorkOrderChildManpower> WorkOrderChildManpower { get; set; }
    }
}