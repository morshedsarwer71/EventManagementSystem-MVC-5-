using EventManagement.Areas.EventManagement.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.ViewModels
{
    public class ReportViewModels
    {
        public IEnumerable<ResponseTimeSheet> ResponseTimeSheets { get; set; }
    }
}