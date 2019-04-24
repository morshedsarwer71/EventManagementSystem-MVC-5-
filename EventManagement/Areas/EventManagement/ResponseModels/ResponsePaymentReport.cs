using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.ResponseModels
{
    public class ResponsePaymentReport
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal TotalWorkBill { get; set; }
        public decimal TotalPayment { get; set; }
        public decimal Total { get; set; }
        public decimal GrandTotal { get; set; }
    }
}