using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.ResponseModels
{
    public class ResponseEmployeesSetupAssignment
    {
        public int SetupAssignmentSerialNumber { get; set; }
        public int SetupAssignmentEmployeeId { get; set; }
        public string SetupAssignmentEmployeeFirstName { get; set; }
        public string SetupAssignmentEmployeeLastName { get; set; }
    }
}