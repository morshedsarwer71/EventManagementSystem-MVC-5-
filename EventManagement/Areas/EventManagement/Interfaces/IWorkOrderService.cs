using EventManagement.Areas.EventManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Areas.EventManagement.Interfaces
{
    public interface IWorkOrderService
    {
        void AddWorkOrderService(WorkOrderChildService workOrderChildService, int workOrderId, int userId, string userName);
    }
}
