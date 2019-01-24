using EventManagement.Areas.EventManagement.Models;
using EventManagement.Areas.EventManagement.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Areas.EventManagement.Interfaces
{
    public interface IWorkOrderParent
    {
        IEnumerable<ResponseWorkOrderParent> GetWorkOrders(string culture, string UserName, int UserId, int? ConcernId);
        IEnumerable<ResponseWorkOrderParent> GetWorkOrdersByStatusId(string culture, string UserName, int UserId, int ConcernId,int statusId);
        IEnumerable<ResponseWorkOrderParent> GetCompletedWorkOrders(string culture, string UserName, int UserId, int? ConcernId);
        IEnumerable<ResponseWorkOrderParent> GetUpcomingWorkOrders(string culture, string UserName, int UserId, int? ConcernId);
        void AddWorkOrder(WorkOrderParent workOrder, string UserName, int UserId, int concernId);
        WorkOrderParent workOrderById(int id, string UserName, int UserId);
        void Delete(int id, string UserName, int UserId);
        void Update(string UserName, int UserId, WorkOrderParent workOrder, int id);
        IEnumerable<ResponseWorkOrderParent> WorkOrderDetailsById(string culture, string userName, int UserId, int OrderId);
    }
}
