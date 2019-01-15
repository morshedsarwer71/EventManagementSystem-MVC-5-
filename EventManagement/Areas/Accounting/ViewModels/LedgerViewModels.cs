using EventManagement.Areas.Accounting.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.Accounting.ViewModels
{
    public class LedgerViewModels
    {
        public IEnumerable<ResponseLedger> ResponseLedgers { get; set; }
    }
}