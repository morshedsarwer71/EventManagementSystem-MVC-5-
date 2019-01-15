using EventManagement.Areas.Accounting.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Areas.Accounting.Interfaces
{
    public interface IReport
    {
        IEnumerable<ResponseLedger> ResponseLedgers(int headId,string culture,int concernId, string fromDate, string toDate);
        IEnumerable<TrialBalance> TrialBalances(int concernId,string culture);
        IEnumerable<IncomeStatement> IncomeStatements(int concernId,string culture);
        IEnumerable<BalanceSheet> BalanceSheets(int concernId,string culture);
    }
}
