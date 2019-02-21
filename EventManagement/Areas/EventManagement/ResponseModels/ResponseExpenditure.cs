using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.ResponseModels
{
    public class ResponseExpenditure
    {
        public int Serial { get; set; }
        public int ExpenditureId { get; set; }
        public string ExpenditureHead { get; set; }
        public string ExpenditureDate { get; set; }
        public string TransactionType { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public int Rows { get; set; }
    }
}