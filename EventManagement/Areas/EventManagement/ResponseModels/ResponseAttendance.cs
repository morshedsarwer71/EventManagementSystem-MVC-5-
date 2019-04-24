using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.ResponseModels
{
    public class ResponseAttendance
    {
        public int AttendanceId { get; set; }
        public int SerialNumber { get; set; }
        public DateTime AttendanceDate { get; set; }
        public DateTime InTime { get; set; }
        public DateTime InTimeDate { get; set; }
        public DateTime OutTime { get; set; }
        public DateTime OutTimeDate { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
        public int TotalHour { get; set; }
        public int NumberOfRows { get; set; }
    }
}