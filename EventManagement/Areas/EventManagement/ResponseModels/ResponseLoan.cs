using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.ResponseModels
{
    public class ResponseLoan
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string InstallmentDate { get; set; }
        public DateTime Date { get; set; }
        public DateTime LoanPaidDate { get; set; }
        public int NumOfInstallment { get; set; }
        public decimal Amont { get; set; }
        public decimal DueAmount { get; set; }
        public decimal PaidAmount { get; set; }
    }
}