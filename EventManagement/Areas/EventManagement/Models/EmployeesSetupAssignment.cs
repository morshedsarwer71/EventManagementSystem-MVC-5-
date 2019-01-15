﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.Models
{
    public class EmployeesSetupAssignment
    {
        [Key]
        public int SetupAssignmentID { get; set; }
        public int WorkOrderId { get; set; }
        public int EmployeeId { get; set; }
        [NotMapped]
        public List<WorkOrderChildManpower> WorkOrderChildManpower { get; internal set; }
    }
}