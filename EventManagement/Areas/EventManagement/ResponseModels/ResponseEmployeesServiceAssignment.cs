using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.ResponseModels
{
    public class ResponseEmployeesServiceAssignment
    {
        public int ServiceAssignmentSerialNumber { get; set; }
        public int ServiceAssignmentEmployeeId { get; set; }
        public string ServiceAssignmentEmployeeFirstName { get; set; }
        public string ServiceAssignmentEmployeeLastName { get; set; }
    }
}