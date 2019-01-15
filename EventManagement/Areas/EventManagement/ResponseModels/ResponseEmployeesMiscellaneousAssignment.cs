using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.ResponseModels
{
    public class ResponseEmployeesMiscellaneousAssignment
    {
        public int MiscellaneousAssignmentSerialNumber { get; set; }
        public int MiscellaneousAssignmentEmployeeId { get; set; }
        public string MiscellaneousAssignmentEmployeeFirstName { get; set; }
        public string MiscellaneousAssignmentEmployeeLastName { get; set; }
    }
}