using EventManagement.Areas.Global.Interfaces;
using EventManagement.Areas.Global.Models;
using EventManagement.Areas.Global.Password;
using EventManagement.Areas.Global.ViewModels;
using EventManagement.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventManagement.Areas.Global.Controllers
{
    public class GlobalDataController : Controller
    {
        private readonly DataContext _context;
        private readonly ISystemUser _system;
        public GlobalDataController(DataContext context, ISystemUser system)
        {
            _context = context;
            _system = system;
        }
        // GET: Global/GlobalData
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LogIn(SystemUser systemUser)
        {
            var getuser = _context.SystemUsers.FirstOrDefault(m => m.EmailAddress == systemUser.EmailAddress);
            if (getuser != null)
            {
                Helper helper = new Helper();
                var hashSalt = getuser.HashSalt;
                var password = helper.HashPasswordUsingSHA2(systemUser.Password, hashSalt);
                var query = _context.SystemUsers.FirstOrDefault(m => m.EmailAddress == systemUser.EmailAddress && m.Password == password);
                if (query != null)
                {
                    Session["ConcernId"] = Convert.ToInt32(query.ConcernID);
                    Session["UserId"] = Convert.ToInt32(query.UserID);
                    Session["UserName"] = Convert.ToString(query.Name);
                    Session["CreationDate"] = Convert.ToDateTime(query.CreationDate);
                    return RedirectToAction("Index", "AccountingData", new { Area = "Accounting" });
                }
                ViewBag.error = "email or password wrong !";
            }

            return View();
        }
        [HttpGet]
        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction(nameof(LogIn));
        }
        //[HttpPost]
        //public JsonResult LogIn(SystemUser systemUser)
        //{
        //    return Json(new { redirectUrl = Url.Action("Index", "Home"), isRedirect = true });
        //}
        [HttpGet]
        public ActionResult Registration()
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            if (concernId > 0 && userId > 0)
            {
                return View();
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        public ActionResult Users()
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var user = _system.Users(concernId);
            SystemUserViewModels viewModels = new SystemUserViewModels()
            {
                SystemUsers = user
            };
            return View(viewModels);
        }
        [HttpGet]
        public ActionResult AddUser()
        {
            return View();
        }
        [HttpPost]
        public JsonResult AddUser(SystemUser systemUser)
        {
            var chkUser = _context.SystemUsers.FirstOrDefault(m => m.EmailAddress == systemUser.EmailAddress);
            if (chkUser == null)
            {
                var concernId = Convert.ToInt32(Session["ConcernId"]);
                var userId = Convert.ToInt32(Session["UserId"]);
                Helper helper = new Helper();
                var hashSalt = helper.GenerateRandomSalt();
                var password = helper.HashPasswordUsingSHA2(systemUser.Password, hashSalt);
                systemUser.Password = password;
                systemUser.CreatorId = userId;
                systemUser.ConcernID = concernId;
                systemUser.CreationDate = DateTime.Now;
                systemUser.LoginDate = DateTime.Now;
                systemUser.HashSalt = hashSalt;
                systemUser.ActivationDate = DateTime.Now;
                systemUser.ModificationDate = DateTime.Now;
                _context.SystemUsers.Add(systemUser);
                _context.SaveChanges();
                return Json("added successfully", JsonRequestBehavior.AllowGet);
            }
            return Json("User name or email already exists", JsonRequestBehavior.AllowGet);
        }
    }
}