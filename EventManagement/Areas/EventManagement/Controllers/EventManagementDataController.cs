using CrystalDecisions.CrystalReports.Engine;
using EventManagement.Areas.EventManagement.Interfaces;
using EventManagement.Areas.EventManagement.Models;
using EventManagement.Areas.EventManagement.ReportObject;
using EventManagement.Areas.EventManagement.ViewModels;
using EventManagement.Context;
using Microsoft.Reporting.WebForms;
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
        private readonly IEventManagementClient _client;
        private readonly IEventReport _report;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(EventManagementDataController));
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
            ITransaction transaction,
            IEventManagementClient client,
            IEventReport report
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
            _client = client;
            _report = report;
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
        public ActionResult EditEmployees(int page = 1)
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
                try
                {
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
                catch (Exception ex)
                {

                    log.Error(ex.ToString());
                }


            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpGet]
        public ActionResult EditEmployee(int id)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            if (concernId > 0 && userId > 0)
            {
                var entitle = _context.EmployeeEntitlements.ToList();
                var emp = _context.Employees.FirstOrDefault(x => x.EmployeeId == id);
                var active = _employee.IsActive();
                EmployeeViewModels viewModels = new EmployeeViewModels()
                {
                    Entitlements = entitle,
                    Employee = emp,
                    IsActives = active
                };
                return View(viewModels);
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpPost]
        public ActionResult EditEmployee(int id, Employee employee)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                var emp = _context.Employees.FirstOrDefault(x => x.EmployeeId == id);
                var image = "";
                var passport = "";
                if (employee.ImageUrl != null)
                {
                    //deleting image 
                    if (System.IO.File.Exists(emp.EmployeeImagePath))
                    {
                        System.IO.File.Delete(emp.EmployeeImagePath);
                    }
                    string empExtension = Path.GetExtension(employee.ImageUrl.FileName);
                    string empFileName = employee.EmployeeCode + empExtension;
                    image = "~/Areas/EventManagement/Image/" + empFileName;
                    empFileName = Path.Combine(Server.MapPath("~/Areas/EventManagement/Image/"), empFileName);
                    employee.ImageUrl.SaveAs(empFileName);
                    _employee.Update(employee, id, userId, userName, image, emp.PassportImagePath);
                }
                else
                {
                    _employee.Update(employee, id, userId, userName, emp.EmployeeImagePath, emp.PassportImagePath);
                }
                if (employee.PassportUrl != null)
                {
                    //deleting passport 
                    if (System.IO.File.Exists(emp.PassportImagePath))
                    {
                        System.IO.File.Delete(emp.PassportImagePath);
                    }
                    string passExtension = Path.GetExtension(employee.PassportUrl.FileName);
                    string passFileName = employee.EmployeeCode + passExtension;
                    passport = "~/Areas/EventManagement/Passport/" + passFileName;
                    passFileName = Path.Combine(Server.MapPath("~/Areas/EventManagement/Passport/"), passFileName);
                    employee.PassportUrl.SaveAs(passFileName);
                    _employee.Update(employee, id, userId, userName, emp.EmployeeImagePath, passport);
                }
                else
                {
                    _employee.Update(employee, id, userId, userName, emp.EmployeeImagePath, emp.PassportImagePath);
                }

                return RedirectToAction(nameof(EditEmployees));
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
        public ActionResult EventClient()
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
        public ActionResult EventClients()
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                var clients = _client.EventClients(concernId);
                return View(clients);
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpGet]
        public ActionResult AddEventClient()
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
        public ActionResult AddEventClient(EventManagementClient eventManagementClient)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                _client.Add(eventManagementClient, userName, userId, concernId);
                return RedirectToAction(nameof(EventClient));
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpGet]
        public ActionResult EditEventClient(int id)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                var client = _client.EventClientById(id);
                return View(client);
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpPost]
        public ActionResult EditEventClient(int id, EventManagementClient eventManagementClient)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                _client.Update(id, eventManagementClient, userName, userId);
                return RedirectToAction(nameof(EventClient));
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
            var orders = _work.GetWorkOrdersByStatusId("en-US", userName, userId, concernId, id);
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
                    Attendances = att
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
                _attendance.AddAttendance(attendance, userName, userId, concernId);
                return View(nameof(Index));
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpPost]
        public JsonResult DeleteAttendance(int id)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                _attendance.DeleteAttendance(id, userName, userId);
                return Json(new
                {
                    redirectUrl = Url.Action("Attendance", "EventManagementData", new { Area = "EventManagement" }),
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
                var payments = _transaction.ResponseSalaries(concernId, userName, userId);
                TransactionViewModels viewModels = new TransactionViewModels()
                {
                    SalaryPayments = payments
                };
                return View(viewModels);
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
                var employee = _context.Employees.Where(x => x.IsDelete == 0 && x.ConcernId == concernId).ToList();
                var saltype = _transaction.SalaryTypes();
                var trans = _transaction.TransactionTypes();
                TransactionViewModels viewModels = new TransactionViewModels()
                {
                    Employees = employee,
                    SalaryTypes = saltype,
                    TransactionTypes = trans
                };
                return View(viewModels);
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpPost]
        public JsonResult AddSalary(SalaryPayment salaryPayment)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                _transaction.AddSalaryPayment(salaryPayment, concernId, userName, userId);
                return Json(new
                {
                    redirectUrl = Url.Action("Salary", "EventManagementData", new { Area = "EventManagement" }),
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
        public ActionResult Expenditure()
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
        public ActionResult Expenditures()
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                var expense = _transaction.ResponseExpenditures(concernId, userName, userId);
                TransactionViewModels viewModels = new TransactionViewModels()
                {
                    ResponseExpenditures = expense
                };
                return View(viewModels);
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpGet]
        public ActionResult AddExpenditure()
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                var heads = _transaction.ExpenditureHeads(concernId, userName, userId);
                var type = _transaction.TransactionTypes();
                TransactionViewModels viewModels = new TransactionViewModels()
                {
                    ExpenditureHeads = heads,
                    TransactionTypes = type
                };
                return View(viewModels);
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpPost]
        public JsonResult AddExpenditure(Expenditure expenditure)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                _transaction.AddExpenditure(expenditure, concernId, userName, userId);
                return Json(new
                {
                    redirectUrl = Url.Action("Expenditure", "EventManagementData", new { Area = "EventManagement" }),
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
        public ActionResult ExpendituresReport()
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                var heads = _transaction.ExpenditureHeads(concernId, userName, userId);
                var type = _transaction.TransactionTypes();
                TransactionViewModels viewModels = new TransactionViewModels()
                {
                    ExpenditureHeads = heads,
                    TransactionTypes = type
                };
                return View(viewModels);
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpPost]
        public JsonResult ExpendituresReport(string fdate, string tDate, int transType, int head)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                ExpenditureReport expenditure = new ExpenditureReport();
                expenditure.FromDate = fdate;
                expenditure.ToDate = tDate;
                expenditure.TransType = transType;
                expenditure.Expenditure = head;
                var report = _transaction.ReportExpenditures(concernId, expenditure, userName, userId);
                TransactionViewModels viewModels = new TransactionViewModels()
                {
                    ResponseExpenditures = report
                };
                return Json(viewModels, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                redirectUrl = Url.Action("LogIn", "GlobalData", new { Area = "Global" }),
                isRedirect = true
            });

        }
        [HttpGet]
        public ActionResult ClientPayment()
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
        public ActionResult ClientPayments()
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                var payment = _transaction.ClientPayments(concernId, userName, userId,"en-US");
                TransactionViewModels viewModels = new TransactionViewModels()
                {
                    ClientPayments = payment
                };
                return View(viewModels);
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpGet]
        public ActionResult AddClientPayment()
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                var type = _transaction.TransactionTypes();
                var client = _transaction.Clients(concernId, userName, userId);
                var bank = _transaction.Banks(concernId, userName, userId);
                TransactionViewModels viewModels = new TransactionViewModels()
                {
                    TransactionTypes = type,
                    Banks= bank,
                    Clients= client
                };
                return View(viewModels);
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpPost]
        public JsonResult AddClientPayment(ClientPayment clientPayment)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                _transaction.AddClientPayment(clientPayment, concernId, userName, userId);
                return Json(new
                {
                    redirectUrl = Url.Action("ClientPayment", "EventManagementData", new { Area = "EventManagement" }),
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
        public ActionResult EditPayment(int id)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                var type = _transaction.TransactionTypes();
                var client = _transaction.Clients(concernId, userName, userId);
                var bank = _transaction.Banks(concernId, userName, userId);
                var clieint = _transaction.ClientPaymentById(id, concernId, userName, userId);
                TransactionViewModels viewModels = new TransactionViewModels()
                {
                    TransactionTypes = type,
                    Banks = bank,
                    Clients = client,
                    ClientPayment= clieint
                };
                return View(viewModels);
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpPost]
        public ActionResult EditPayment(int id,ClientPayment clientPayment)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                _transaction.UpdateClientPayment(clientPayment, id, concernId, userName, userId);
                return RedirectToAction(nameof(ClientPayment));
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpGet]
        public ActionResult SalariesReport()
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                var employee = _context.Employees.Where(x => x.IsDelete == 0 && x.ConcernId == concernId).ToList();
                var type = _transaction.TransactionTypes();
                TransactionViewModels viewModels = new TransactionViewModels()
                {
                    Employees = employee,
                    TransactionTypes = type
                };
                return View(viewModels);
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpPost]
        public JsonResult SalariesReport(string fdate, string tDate, int transType, int employeeId)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {
                var payment = _transaction.ResponseSalariesRepor(concernId, userName, userId, fdate, tDate, employeeId, transType);
                TransactionViewModels viewModels = new TransactionViewModels()
                {
                    SalaryPayments = payment
                };
                return Json(viewModels, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                redirectUrl = Url.Action("LogIn", "GlobalData", new { Area = "Global" }),
                isRedirect = true
            });

        }

        //---SDLC Reporting Testing Part
        [HttpGet]
        public ActionResult SalariesReportPDF(string fdate, string tDate, int transType = 0, int employeeId = 0)
        {
            var dae = "2019-01-01";
            var dad = "2019-01-30";
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (concernId > 0 && userId > 0)
            {

                try
                {
                    var payment = _transaction.ResponseSalariesRepor(concernId, userName, userId, dae, dad, employeeId, transType);
                    ReportDocument rd = new ReportDocument();
                    //rd.Load(Path.Combine(Server.MapPath("~/Areas/EventManagement/Reports/Transaction"), "SalaryReport.rpt"));
                    rd.Load(Path.Combine(Server.MapPath("~/CrystalReports"), "ReportCustomer.rpt"));
                    rd.SetDataSource(payment);
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream, "application/pdf", "CustomerList.pdf");
                }
                catch (Exception ex)
                {
                    log.Error(ex.ToString());
                    return View("Error", new HandleErrorInfo(ex, "EventManagementData", "SalariesReport"));

                }
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });

        }
        [HttpGet]
        public ActionResult SalariesReportExcel(string ReportType)
        {

            LocalReport local = new LocalReport();
            local.ReportPath = Server.MapPath("~/Areas/Reports/Employee.rdlc");

            ReportDataSource source = new ReportDataSource();
            source.Name = "DataSet";
            source.Value = _context.Employees.ToList();
            local.DataSources.Add(source);
            string reportType = ReportType;
            string mimeType;
            string encoding;
            string fileNameExtension = "xlsx";

            string[] streams;
            Warning[] warnings;
            byte[] renderByte;
            renderByte = local.Render("Excel", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            Response.AddHeader("content-disposition", "attachment:filename=employee_Report." + fileNameExtension);
            return File(renderByte, fileNameExtension);

        }
        [HttpGet]
        public ActionResult SalariesReportPDFTest(string ReportType)
        {

            Warning[] warnings;
            string mimeType;
            string[] streamids;
            string encoding;
            string filenameExtension;

            var viewer = new ReportViewer();
            viewer.LocalReport.ReportPath = Server.MapPath("~/Areas/Reports/Employee.rdlc");
            var cus = _context.Employees.ToList();
            ReportDataSource source = new ReportDataSource()
            {
                Name = "DataSet",
                Value = _context.Employees.ToList()
            };
            viewer.LocalReport.DataSources.Add(source);
            viewer.LocalReport.Refresh();

            var bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);

            //return new FileContentResult(bytes, mimeType);

            return File(bytes, mimeType, "Employee.pdf");

        }
        [HttpGet]
        public ActionResult WOManpowerPDF(int id)
        {

            Warning[] warnings;
            string mimeType;
            string[] streamids;
            string encoding;
            string filenameExtension;

            var viewer = new ReportViewer();
            viewer.LocalReport.ReportPath = Server.MapPath("~/Areas/EventManagement/Reports/WorkOrderManpower.rdlc");
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            var assigned = _manpower.WorkOrderAssigneds(id, userId, userName, concernId);
            ReportDataSource source = new ReportDataSource()
            {
                Name = "DataSet",
                Value = assigned
            };
            viewer.LocalReport.DataSources.Add(source);
            viewer.LocalReport.Refresh();

            var bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);

            //return new FileContentResult(bytes, mimeType);

            return File(bytes, mimeType, "Employee.pdf");

        }

        //----SDLC Reporting Testing Part
        [HttpGet]
        public ActionResult TimeSheet()
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
        public ActionResult DailyTimeSheet(int id)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            
            if (userId > 0 && concernId > 0)
            {
                ViewBag.id = id;
                var today = DateTime.Now.ToString("yyyy-MM-dd");
                var sheet = _report.DailyTimeSheet(userName, userId, id, today, "en-US");
                ReportViewModels viewModels = new ReportViewModels
                {
                    ResponseTimeSheets = sheet
                };
                return View(viewModels);
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpPost]
        public JsonResult DailyTimeSheetReport(int id,string date)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (userId > 0 && concernId > 0)
            {
                //var today = DateTime.Now.ToString("yyyy-MM-dd");
                var sheet = _report.DailyTimeSheet(userName, userId, id, date, "en-US");
                ReportViewModels viewModels = new ReportViewModels
                {
                    ResponseTimeSheets = sheet
                };
                return Json(viewModels,JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                redirectUrl = Url.Action("LogIn", "GlobalData", new { Area = "Global" }),
                isRedirect = true
            });
        }
        [HttpPost]
        public JsonResult DailyTimeSheetReportPDF(int id, string date)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (userId > 0 && concernId > 0)
            {
                //var today = DateTime.Now.ToString("yyyy-MM-dd");
                var sheet = _report.DailyTimeSheet(userName, userId, id, date, "en-US");
                ReportViewModels viewModels = new ReportViewModels
                {
                    ResponseTimeSheets = sheet
                };
                return Json(new
                {
                    redirectUrl = Url.Action("DailyTimeSheet", "EventManagementReport", new { Area = "EventManagement",id= id, date= date }),
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
        public ActionResult TimeSheetSummary(int id)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var userName = Convert.ToString(Session["UserName"]);
            if (userId > 0 && concernId > 0 && id>0)
            {
                ViewBag.id = id;
                var today = DateTime.Now.ToString("yyyy-MM-dd");
                var sheet = _report.TimeSheetSummary(userName, userId, id, "en-US");
                ReportViewModels viewModels = new ReportViewModels
                {
                    ResponseTimeSheets = sheet
                };
                return View(viewModels);
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }

    }
}