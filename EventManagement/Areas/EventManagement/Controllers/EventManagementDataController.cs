using EventManagement.Areas.EventManagement.Interfaces;
using EventManagement.Areas.EventManagement.Models;
using EventManagement.Areas.EventManagement.ViewModels;
using EventManagement.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EventManagement.Areas.EventManagement.Controllers
{
    public class EventManagementDataController : Controller
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
        public EventManagementDataController
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
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Employee()
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            if (concernId > 0 && userId > 0)
            {
                return View();
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpGet]
        public ActionResult Employees(int page = 1)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                var emp = _employee.Employees(userId, userName, concernId, page);
                EmployeeViewModels viewModels = new EmployeeViewModels()
                {
                    Employees = emp
                };
                return View(viewModels);
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpGet]
        public ActionResult AddEmployee()
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            if (concernId > 0 && userId > 0)
            {
                var entitle = _context.EmployeeEntitlements.ToList();
                return View(entitle);
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpPost]
        public ActionResult AddEmployee(Employee employee)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                //string empFileName = Path.GetFileNameWithoutExtension(employee.ImageUrl.FileName);
                string empExtension = Path.GetExtension(employee.ImageUrl.FileName);
                string empFileName = employee.EmployeeCode + empExtension;
                employee.EmployeeImagePath = "~/Areas/EventManagement/Image/" + empFileName;
                empFileName = Path.Combine(Server.MapPath("~/Areas/EventManagement/Image/"), empFileName);
                employee.ImageUrl.SaveAs(empFileName);

                string passExtension = Path.GetExtension(employee.PassportUrl.FileName);
                string passFileName = employee.EmployeeCode + passExtension;
                employee.PassportImagePath = "~/Areas/EventManagement/Passport/" + passFileName;
                passFileName = Path.Combine(Server.MapPath("~/Areas/EventManagement/Passport/"), passFileName);
                employee.PassportUrl.SaveAs(passFileName);
                _employee.AddEmployee(employee, userId, userName, concernId);
                ModelState.Clear();
                return RedirectToAction(nameof(Employee));
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpGet]
        public ActionResult Setup()
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                return View();
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpGet]
        public ActionResult Setups()
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                var setups = _setup.EventSetups(userId, userName, concernId);
                return View(setups);
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpGet]
        public ActionResult AddSetup()
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                return View();
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpPost]
        public JsonResult AddSetup(EventSetup eventSetup)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                _setup.AddEventSetup(eventSetup, userId, userName, concernId);
                return Json(new
                {
                    redirectUrl = Url.Action("Setup", "EventManagementData", new { Area = "EventManagement" }),
                    isRedirect = true
                });
            }
            return Json(new
            {
                redirectUrl = Url.Action("LogIn", "GlobalData", new { Area = "Global" }),
                isRedirect = true
            });
        }
        [HttpGet]
        public ActionResult EditSetup(int id)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                var setup = _setup.EventSetupById(userId, userName, id);
                return View(setup);
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpPost]
        public ActionResult EditSetup(EventSetup eventSetup, int id)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                _setup.Update(eventSetup, userId, userName, id);
                return RedirectToAction(nameof(Setup));
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpGet]
        public ActionResult Service()
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                return View();
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpGet]
        public ActionResult Services()
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                var services = _service.EventServices(userId, userName, concernId);
                return View(services);
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpGet]
        public ActionResult AddService()
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                return View();
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpPost]
        public JsonResult AddService(EventService eventService)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                _service.AddEventService(eventService, userId, userName, concernId);
                return Json(new
                {
                    redirectUrl = Url.Action("Service", "EventManagementData", new { Area = "EventManagement" }),
                    isRedirect = true
                });
            }
            return Json(new
            {
                redirectUrl = Url.Action("LogIn", "GlobalData", new { Area = "Global" }),
                isRedirect = true
            });
        }
        [HttpGet]
        public ActionResult EditService(int id)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                var service = _service.EventServiceById(userId, userName, id);
                return View(service);
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpPost]
        public ActionResult EditService(EventService eventService, int id)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                _service.Update(eventService, userId, userName, id);
                return RedirectToAction(nameof(Service));
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpGet]
        public ActionResult Miscellaneous()
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                return View();
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpGet]
        public ActionResult Miscellaneouses()
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                var miscellaneous = _miscellaneous.EventMiscellaneouses(userId, userName, concernId);
                return View(miscellaneous);
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpGet]
        public ActionResult AddMiscellaneous()
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                return View();
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpPost]
        public JsonResult AddMiscellaneous(EventMiscellaneous eventmiscellaneous)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                _miscellaneous.AddEventMiscellaneous(eventmiscellaneous, userId, userName, concernId);
                return Json(new
                {
                    redirectUrl = Url.Action("Miscellaneous", "EventManagementData", new { Area = "EventManagement" }),
                    isRedirect = true
                });
            }
            return Json(new
            {
                redirectUrl = Url.Action("LogIn", "GlobalData", new { Area = "Global" }),
                isRedirect = true
            });
        }
        [HttpGet]
        public ActionResult EditMiscellaneous(int id)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                var miscellaneous = _miscellaneous.EventMiscellaneousById(userId, userName, id);
                return View(miscellaneous);
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpPost]
        public ActionResult EditMiscellaneous(EventMiscellaneous eventmiscellaneous, int id)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                _miscellaneous.Update(eventmiscellaneous, userId, userName, id);
                return RedirectToAction(nameof(Miscellaneous));
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpGet]
        public ActionResult WorkOrder()
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                return View();
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpGet]
        public ActionResult WorkOrderByStatus()
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                var status = _context.WorkOrderStatuses.ToList();
                WorkOrderViewModels viewModels = new WorkOrderViewModels()
                {
                    WorkOrderStatus = status
                };
                return View(viewModels);
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpPost]
        public JsonResult WorkOrderByStatus(int id)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            var orders = _work.GetWorkOrdersByStatusId("en-US", userName, userId, concernId,id);
            WorkOrderViewModels viewModels = new WorkOrderViewModels()
            {
                WorkOrderParents = orders
            };
            return Json(viewModels, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult WorkOrders()
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                var orders = _work.GetWorkOrders("en-US", userName, userId, concernId);
                WorkOrderViewModels viewModels = new WorkOrderViewModels()
                {
                    WorkOrderParents = orders
                };
                return View(viewModels);
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpGet]
        public ActionResult AddWorkOrder()
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                var clients = _context.EventManagementClients.ToList();
                var status = _context.WorkOrderStatuses.ToList();
                var vat = _setting.GetDefaultSetting(concernId, userId, userName);
                var pay = _setting.PaymentStatuses();
                WorkOrderViewModels viewModels = new WorkOrderViewModels()
                {
                    EventClients = clients,
                    WorkOrderStatus = status,
                    VAT = vat,
                    PaymentStatus = pay
                };
                return View(viewModels);
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpPost]
        public JsonResult AddWorkOrder(WorkOrderParent orderParent)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                _work.AddWorkOrder(orderParent, userName, userId, concernId);
                return Json(new
                {
                    redirectUrl = Url.Action("WorkOrder", "EventManagementData", new { Area = "EventManagement" }),
                    isRedirect = true
                });
            }
            return Json(new
            {
                redirectUrl = Url.Action("LogIn", "GlobalData", new { Area = "Global" }),
                isRedirect = true
            });
        }
        [HttpGet]
        public ActionResult EditWorkOrder(int id)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                var clients = _context.EventManagementClients.ToList();
                var status = _context.WorkOrderStatuses.ToList();
                var vat = _setting.GetDefaultSetting(concernId, userId, userName);
                var pay = _setting.PaymentStatuses();
                var work = _work.workOrderById(id, userName, userId);
                WorkOrderViewModels viewModels = new WorkOrderViewModels()
                {
                    EventClients = clients,
                    WorkOrderStatus = status,
                    VAT = vat,
                    PaymentStatus = pay,
                    WorkOrderParent = work
                };
                return View(viewModels);
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpGet]
        public ActionResult DetailsWorkOrder(int id)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                var clients = _context.EventManagementClients.ToList();
                var status = _context.WorkOrderStatuses.ToList();
                var vat = _setting.GetDefaultSetting(concernId, userId, userName);
                var pay = _setting.PaymentStatuses();
                var work = _work.workOrderById(id, userName, userId);
                WorkOrderViewModels viewModels = new WorkOrderViewModels()
                {
                    EventClients = clients,
                    WorkOrderStatus = status,
                    VAT = vat,
                    PaymentStatus = pay,
                    WorkOrderParent = work
                };
                return View(viewModels);
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpPost]
        public ActionResult EditWorkOrder(WorkOrderParent orderParent, int id)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                _work.Update(userName, userId, orderParent, id);
                return RedirectToAction(nameof(WorkOrder));
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpGet]
        public ActionResult ConfirmedWorkOrders()
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                var orders = _work.GetUpcomingWorkOrders("en-US", userName, userId, concernId);
                WorkOrderViewModels viewModels = new WorkOrderViewModels()
                {
                    WorkOrderParents = orders
                };
                return View(viewModels);
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpGet]
        public ActionResult AssignManpower(int id)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                //var employees = _employee.Employees(userId, userName, concernId,1);
                var employee = _context.Employees.ToList();
                WorkOrderChildManpower workOrder = new WorkOrderChildManpower()
                {
                    Employee = employee
                };
                return View(workOrder);
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpPost]
        public ActionResult AssignManpower(int id, WorkOrderChildManpower childManpower)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                _manpower.AddWorkOrderManpower(childManpower, id, userId, userName);
                return RedirectToAction(nameof(ConfirmedWorkOrders));
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpGet]
        public ActionResult AssignSetup(int id)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                var setups = _context.EventSetups.ToList();
                WorkOrderChildSetup childSetup = new WorkOrderChildSetup()
                {
                    EventSetup = setups
                };
                return View(childSetup);
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpPost]
        public ActionResult AssignSetup(int id, WorkOrderChildSetup orderChildSetup)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                _orderSetup.AddWorkOrderSetup(orderChildSetup, id, userId, userName);
                return RedirectToAction(nameof(ConfirmedWorkOrders));
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpGet]
        public ActionResult AssignService(int id)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                var service = _context.EventServices.ToList();
                WorkOrderChildService orderChildService = new WorkOrderChildService()
                {
                    EventService = service
                };
                return View(orderChildService);
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpPost]
        public ActionResult AssignService(int id, WorkOrderChildService orderChildService)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                _orderService.AddWorkOrderService(orderChildService, id, userId, userName);
                return RedirectToAction(nameof(ConfirmedWorkOrders));
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpGet]
        public ActionResult AssignMiscellaneous(int id)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                var micell = _context.EventMiscellaneouses.ToList();
                WorkOrderChildMiscellaneous miscellaneous = new WorkOrderChildMiscellaneous()
                {
                    EventMiscellaneous = micell
                };
                return View(miscellaneous);
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpPost]
        public ActionResult AssignMiscellaneous(int id, WorkOrderChildMiscellaneous childMiscellaneous)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                _orderMiscellaneous.AddWorkOrderMiscellaneous(id, childMiscellaneous, userId, userName);
                return RedirectToAction(nameof(ConfirmedWorkOrders));
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpGet]
        public ActionResult AssignedWorkOrders()
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                var orders = _work.GetUpcomingWorkOrders("en-US", userName, userId, concernId);
                WorkOrderViewModels viewModels = new WorkOrderViewModels()
                {
                    WorkOrderParents = orders
                };
                return View(viewModels);
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpGet]
        public ActionResult AssignedManpower(int id)
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
                return View(viewModels);
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpPost]
        public JsonResult DeleteAssignedManpower(int id)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                _manpower.Delete(id, userId, userName, concernId);
                return Json(new
                {
                    redirectUrl = Url.Action("AssignedWorkOrders", "EventManagementData", new { Area = "EventManagement" }),
                    isRedirect = true
                });
            }
            return Json(new
            {
                redirectUrl = Url.Action("LogIn", "GlobalData", new { Area = "Global" }),
                isRedirect = true
            });
        }
        [HttpGet]
        public ActionResult AssignedSetup(int id)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                var assigned = _orderSetup.WorkOrderAssigneds(id, userId, userName, concernId);
                WorkOrderAssignedViewModels viewModels = new WorkOrderAssignedViewModels()
                {
                    WorkOrderAssigneds = assigned
                };
                return View(viewModels);
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpPost]
        public JsonResult DeleteAssignedSetup(int id)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                _orderSetup.Delete(id, userId, userName, concernId);
                return Json(new
                {
                    redirectUrl = Url.Action("AssignedWorkOrders", "EventManagementData", new { Area = "EventManagement" }),
                    isRedirect = true
                });
            }
            return Json(new
            {
                redirectUrl = Url.Action("LogIn", "GlobalData", new { Area = "Global" }),
                isRedirect = true
            });
        }
        [HttpGet]
        public ActionResult AssignedService(int id)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                var assigned = _orderService.WorkOrderAssigneds(id, userId, userName, concernId);
                WorkOrderAssignedViewModels viewModels = new WorkOrderAssignedViewModels()
                {
                    WorkOrderAssigneds = assigned
                };
                return View(viewModels);
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpPost]
        public JsonResult DeleteAssignedService(int id)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                _orderService.Delete(id, userId, userName, concernId);
                return Json(new
                {
                    redirectUrl = Url.Action("AssignedWorkOrders", "EventManagementData", new { Area = "EventManagement" }),
                    isRedirect = true
                });
            }
            return Json(new
            {
                redirectUrl = Url.Action("LogIn", "GlobalData", new { Area = "Global" }),
                isRedirect = true
            });
        }
        [HttpGet]
        public ActionResult AssignedMiscellaneous(int id)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                var assigned = _orderMiscellaneous.WorkOrderAssigneds(id, userId, userName, concernId);
                WorkOrderAssignedViewModels viewModels = new WorkOrderAssignedViewModels()
                {
                    WorkOrderAssigneds = assigned
                };
                return View(viewModels);
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpPost]
        public JsonResult DeleteAssignedMiscellaneous(int id)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                _orderMiscellaneous.Delete(id, userId, userName, concernId);
                return Json(new
                {
                    redirectUrl = Url.Action("AssignedWorkOrders", "EventManagementData", new { Area = "EventManagement" }),
                    isRedirect = true
                });
            }
            return Json(new
            {
                redirectUrl = Url.Action("LogIn", "GlobalData", new { Area = "Global" }),
                isRedirect = true
            });
        }
        [HttpGet]
        public ActionResult Attendance()
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                return View();
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpGet]
        public ActionResult Attendances()
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                var att = _attendance.Attendances(concernId, userName, userId);
                ResponseAttendanceViewModels viewModels = new ResponseAttendanceViewModels()
                {
                    Attendances= att
                };
                return View(viewModels);
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpGet]
        public ActionResult AddAttendance()
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                //var employees = _employee.Employees(userId, userName, concernId,1);
                var employee = _context.Employees.ToList();
                WorkOrderChildManpower workOrder = new WorkOrderChildManpower()
                {
                    Employee = employee
                };
                return View(workOrder);
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpPost]
        public ActionResult AddAttendance(Attendance attendance)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                _attendance.AddAttendance(attendance, userName, userId,concernId);
                return View(nameof(Index));
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpGet]
        public ActionResult ExpenditureHead()
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                return View();
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpGet]
        public ActionResult ExpenditureHeads()
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                var heads = _transaction.ExpenditureHeads(concernId, userName, userId);
                return View(heads);
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpGet]
        public ActionResult AddExpenditureHead()
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                return View();
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpPost]
        public ActionResult AddExpenditureHead(ExpenditureHead expenditureHead)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                _transaction.AddExpenditureHead(expenditureHead, concernId, userName, userId);
                return RedirectToAction(nameof(ExpenditureHead));
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpGet]
        public ActionResult Salary()
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                return View();
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpGet]
        public ActionResult Salaries()
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                return View();
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpGet]
        public ActionResult AddSalary()
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                return View();
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpPost]
        public ActionResult AddSalary(SalaryPayment salaryPayment)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                return View();
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }


    }
}