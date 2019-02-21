using EventManagement.Areas.EventManagement.Interfaces;
using EventManagement.Areas.EventManagement.ViewModels;
using EventManagement.Context;
using Microsoft.Reporting.WebForms;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventManagement.Areas.EventManagement.Controllers
{
    public class EventManagementReportController : Controller
    {
        private readonly DataContext _context;
        private readonly IEmployee _employee;
        private readonly IWorkOrderParent _work;
        private readonly IEventSetup _setup;
        private readonly IEventService _service;
        private readonly IEventMiscellaneous _miscellaneous;
        private readonly IWorkOrderManpower _manpower;
        private readonly IWorkOrderSetup _orderSetup;
        private readonly IWorkOrderService _orderService;
        private readonly IWorkOrderMiscellaneous _orderMiscellaneous;
        private readonly IDefaultSetting _setting;
        private readonly IAttendance _attendance;
        private readonly ITransaction _transaction;
        public EventManagementReportController
            (
             DataContext context,
            IEmployee employee,
            IWorkOrderParent work,
            IEventSetup setup,
            IEventService service,
            IEventMiscellaneous miscellaneous,
            IWorkOrderManpower manpower,
            IWorkOrderSetup orderSetup,
            IWorkOrderService orderService,
            IWorkOrderMiscellaneous orderMiscellaneous,
            IDefaultSetting setting,
            IAttendance attendance,
            ITransaction transaction
            )
        {
            _context = context;
            _employee = employee;
            _work = work;
            _setup = setup;
            _service = service;
            _miscellaneous = miscellaneous;
            _manpower = manpower;
            _orderSetup = orderSetup;
            _orderService = orderService;
            _orderMiscellaneous = orderMiscellaneous;
            _setting = setting;
            _attendance = attendance;
            _transaction = transaction;
        }
        public ActionResult WOManpowerPDF(int id)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                var assigned = _manpower.WorkOrderAssigneds(id, userId, userName, concernId);
                WorkOrderAssignedViewModels viewModels = new WorkOrderAssignedViewModels()
                {
                    WorkOrderAssigneds = assigned
                };
                var report = new PartialViewAsPdf("WOManpowerPDF", viewModels);
                return report;
            }
            else
            {
                return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
            }
        }
    }
}