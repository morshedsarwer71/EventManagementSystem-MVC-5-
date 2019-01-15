using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.Accounting.ResponseModels
{
    public class ResponseLedger
    {
        public int JournalId { get; set; }
        public string AcountHeadName { get; set; }
        public decimal DebitJournalAmount { get; set; }
        public decimal CreditJournalAmount { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public string Date { get; set; }
        public string VoucherCode { get; set; }
    }
}