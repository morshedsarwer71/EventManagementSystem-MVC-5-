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

namespace EventManagement.Areas.EventManagement.Controllers
{
    public class EventManagementDataController : Controller
    {
        private readonly DataContext _context;
        private readonly IEmployee _employee;
        private readonly IWorkOrderParent _work;
        private readonly IEventSetup _setup;
        private readonly IEventService _service;
        public EventManagementDataController
            (
            DataContext context,
            IEmployee employee,
            IWorkOrderParent work,
            IEventSetup setup,
            IEventService service
            )
        {
            _context = context;
            _employee = employee;
            _work = work;
            _setup = setup;
            _service = service;
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
                return Json(new {
                    redirectUrl=Url.Action("Setup","EventManagementData",new {Area= "EventManagement" }),
                    isRedirect=true
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
                var setup = _setup.EventSetupById(userId,userName,id);
                return View(setup);
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpPost]
        public ActionResult EditSetup(EventSetup eventSetup,int id)
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
                var service = _service.EventServiceById(userId, userName,id);
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
        public ActionResult Pax()
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
        public ActionResult Paxes()
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
        public ActionResult AddPax()
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
        public JsonResult AddPax(EventService eventService)
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
        public ActionResult EditPax(int id)
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
        public ActionResult EditPax(EventService eventService, int id)
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
                WorkOrderViewModels viewModels = new WorkOrderViewModels()
                {
                    EventClients = clients,
                    WorkOrderStatus = status
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
                return Json(new {
                    redirectUrl = Url.Action("WorkOrder", "EventManagementData",new {Area= "EventManagement" }),
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

    }
}