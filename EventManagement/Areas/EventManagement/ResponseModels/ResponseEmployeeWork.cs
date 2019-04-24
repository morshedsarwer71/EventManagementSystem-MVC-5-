using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.ResponseModels
{
    public class ResponseEmployeeWork
    {
        public string InTime { get; set; }
        public string OutTime { get; set; }
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public decimal TotalHour { get; set; }
        public string AttDate { get; set; }
        public decimal PerHourRate { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public decimal Total { get; set; }
        public string EventName { get; set; }
    }
}