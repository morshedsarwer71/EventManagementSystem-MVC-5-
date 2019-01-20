using EventManagement.Areas.EventManagement.Models;
using EventManagement.Areas.EventManagement.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Areas.EventManagement.Interfaces
{
    public interface IWorkOrderMiscellaneous
    {
        void AddWorkOrderMiscellaneous(int workOrederId, WorkOrderChildMiscellaneous workOrderChildMiscellaneous, int userId, string userName);
        IEnumerable<WorkOrderAssigned> WorkOrderAssigneds(int workOrederId, int userId, string userName, int concernId);
        void Delete(int WOCID, int userId, string userName, int concernId);
    }
}
