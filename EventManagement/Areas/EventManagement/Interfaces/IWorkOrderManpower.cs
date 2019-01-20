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
        IEnumerable<WorkOrderAssigned> WorkOrderAssigneds(int workOrderId, int userId, string userName,int concernId);
        void Delete(int WOCMID, int userId, string userName, int concernId);
    }
}
