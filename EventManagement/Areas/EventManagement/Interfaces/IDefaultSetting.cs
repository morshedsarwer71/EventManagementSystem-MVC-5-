using EventManagement.Areas.EventManagement.Models;
using EventManagement.Areas.EventManagement.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Areas.EventManagement.Interfaces
{
    public interface IDefaultSetting
    {
        IEnumerable<DefaultSetting> GetDefaultSetting(int concernId,int userId,string userName);
        IEnumerable<PaymentStatus> PaymentStatuses();
    }
}
