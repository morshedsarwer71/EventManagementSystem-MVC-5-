using EventManagement.Areas.Accounting.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.Accounting.ViewModels
{
    public class BalanceSheetViewModels
    {
        public IEnumerable<BalanceSheet> BalanceSheets { get; set; }
    }
}