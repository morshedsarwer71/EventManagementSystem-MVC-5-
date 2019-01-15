using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventManagement.Areas.EventManagement.Controllers
{
    public class EventManagementDataController : Controller
    {
        // GET: EventManagement/EventManagementData
        public ActionResult Index()
        {
            return View();
        }
    }
}