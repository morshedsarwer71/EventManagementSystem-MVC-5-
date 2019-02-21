using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.ResponseModels
{
    public class ResponseTimeSheet
    {
        public int Serial { get; set; }
        public string OrderCode { get; set; }
        public int OrderId { get; set; }
        public string ClientName { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Date { get; set; }
        public string SheetCode { get; set; }
        public string InTime { get; set; }
        public string OutTime { get; set; }
        public decimal TotalHour { get; set; }
        public decimal PerHourRate { get; set; }
        public decimal TotalWithoutVat { get; set; }
        public decimal VatValue { get; set; }
        public decimal VatAmount { get; set; }
        public decimal TotalAfterVat { get; set; }
    }
}