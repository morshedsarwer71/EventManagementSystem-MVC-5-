using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.ResponseModels
{
    public class ResponseClientPayment
    {
        public int Serial { get; set; }
        public int PaymentId { get; set; }
        public string ClientName { get; set; }
        public string Date { get; set; }
        public string PaymentStatus { get; set; }
        public decimal Amount { get; set; }
        public string PayeesName { get; set; }
        public string Notes { get; set; }
        public string Description { get; set; }
    }
}