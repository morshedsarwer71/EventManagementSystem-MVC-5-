using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.ResponseModels
{
    public class ResponseSalaryPayments
    {
        public int Serial { get; set; }
        public int SalaryPaymentId { get; set; }
        public string SalaryType { get; set; }
        public int EmployeeId { get; set; }
        public string Employee { get; set; }
        public string PaymentDate { get; set; }
        public string TransactionType { get; set; }
        public decimal Amount { get; set; }
        public string SalaryMonth { get; set; }
        public string Description { get; set; }
        public int Rows { get; set; }
    }
}