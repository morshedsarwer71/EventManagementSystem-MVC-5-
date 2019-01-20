using EventManagement.Areas.EventManagement.Models;
using EventManagement.Areas.EventManagement.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.ViewModels
{
    public class WorkOrderViewModels
    {
        public IEnumerable<ResponseWorkOrderParent> WorkOrderParents { get; set; }
        public IEnumerable<WorkOrderStatus> WorkOrderStatus { get; set; }
        public IEnumerable<EventManagementClient> EventClients { get; set; }
        public WorkOrderParent WorkOrderParent { get; set; }
    }
}