using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.Accounting.ResponseModels
{
    public class ResponseJournal
    {
        public int Row { get; set; }
        public int JournalId { get; set; }
        public string DebitAccountHead { get; set; }
        public decimal DebitAmount { get; set; }
        public string CreditAccountHead { get; set; }
        public decimal CreditAmount { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public string Creator { get; set; }
        public string VoucherCode { get; set; }
    }
}