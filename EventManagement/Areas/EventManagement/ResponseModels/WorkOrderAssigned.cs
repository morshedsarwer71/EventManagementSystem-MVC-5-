using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.ResponseModels
{
    public class WorkOrderAssigned
    {
        public int Serial { get; set; }
        public string Name { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime Todate { get; set; }
        public int OrderId { get; set; }
        public string OrderCode { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string NIDNumber { get; set; }
        public string PassNumber { get; set; }
        public string ImagePath { get; set; }
        public string PassPath { get; set; }
        public int WOCMid { get; set; }
    }
}