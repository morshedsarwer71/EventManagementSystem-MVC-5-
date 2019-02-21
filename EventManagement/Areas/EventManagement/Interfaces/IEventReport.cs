using EventManagement.Areas.EventManagement.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Areas.EventManagement.Interfaces
{
    public interface IEventReport
    {
        IEnumerable<ResponseTimeSheet> DailyTimeSheet (string userName,int userId, int orderId,string Date, string culture);
        IEnumerable<ResponseTimeSheet> TimeSheetSummary (string userName,int userId, int orderId,string culture);
    }
}
