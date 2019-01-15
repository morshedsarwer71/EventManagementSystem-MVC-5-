using EventManagement.Areas.Accounting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.Accounting.ViewModels
{
    public class ReportHeadViewModels
    {
        public IEnumerable<ReportHead> ReportHeads { get; set; }
    }
}