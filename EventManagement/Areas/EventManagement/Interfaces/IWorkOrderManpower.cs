using EventManagement.Areas.EventManagement.Models;
using EventManagement.Areas.EventManagement.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Areas.EventManagement.Interfaces
{
    public interface IWorkOrderManpower
    {
        void AddWorkOrderManpower(WorkOrderChildManpower workOrderManpower, int workOrderId, int userId, string userName);
        void AddWorkOrderDailyManpower(WorkOrderDailyManpower workOrderManpower,int userId, string userName, int workOrderId);
        IEnumerable<ResponseDailyManPower> DailyManpowers(int wodId);
        IEnumerable<WorkOrderAssigned> WorkOrderAssigneds(int workOrderId, int userId, string userName,int concernId);
        void Delete(int WOCMID, int userId, string userName, int concernId);
        void DeleteManpower(int id, int userId, string userName);
    }
}
