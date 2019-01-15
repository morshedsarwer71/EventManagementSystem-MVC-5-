using EventManagement.Areas.Accounting.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.Accounting.ViewModels
{
    public class JournalIndexViewModels
    {
        public IEnumerable<ResponseJournal> ResponseJournals { get; set; }
    }
}