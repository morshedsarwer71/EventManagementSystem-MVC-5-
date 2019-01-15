using EventManagement.Areas.Accounting.Models;
using EventManagement.Areas.Accounting.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Areas.Accounting.Interfaces
{
    public interface IJournal
    {
        void AddJournal(Journal journal,int userId,int concernId);
        IEnumerable<ResponseJournal> ResponseJournals(string culture, int concenId, int page);
    }
}
