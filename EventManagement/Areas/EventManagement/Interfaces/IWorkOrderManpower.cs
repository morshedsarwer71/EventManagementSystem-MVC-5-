using EventManagement.Areas.EventManagement.Models;
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
    }
}
