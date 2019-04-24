using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.ResponseModels
{
    public class ResponseDailyManPower
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal PerHourRate { get; set; }
        public string Name { get; set; }
        public string EventName { get; set; }
        public string ClientName { get; set; }
    }
}