using EventManagement.Areas.Accounting.Interfaces;
using EventManagement.Areas.Accounting.Models;
using EventManagement.Areas.Accounting.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventManagement.Areas.Accounting.Controllers
{
    public class AccountingDataController : Controller
    {
        private readonly ISettings _settings;
        private readonly IJournal _journal;
        private readonly IReport _report;
        public AccountingDataController(ISettings settings, IJournal journal, IReport report)
        {
            _settings = settings;
            _journal = journal;
            _report = report;
        }
        // GET: Accounting/AccountingData
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AccountHead()
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
        public ActionResult AccountHeads(int page = 1)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            if (concernId > 0 && userId > 0)
            {
                var culture = "";
                var heads = _settings.ResponseAccountHeads(culture, page, concernId);
                AccountHeadsViewModels viewModels = new AccountHeadsViewModels()
                {
                    ResponseAccountHeads = heads
                };
                return View(viewModels);
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpGet]
        public JsonResult JsonAccountHeads(int page=1)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var culture = "";
            var heads = _settings.ResponseAccountHeads(culture, page, concernId);
            AccountHeadsViewModels viewModels = new AccountHeadsViewModels()
            {
                ResponseAccountHeads = heads
            };
            return Json(viewModels,JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult AddAccountHead()
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            if (concernId > 0 && userId > 0)
            {
                var reportHead = _settings.ReportHeads(concernId);
                ReportHeadViewModels viewModels = new ReportHeadViewModels()
                {
                    ReportHeads = reportHead
                };
                return View(viewModels);
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpPost]
        public JsonResult AddAccountHead(AccountsHead accountsHead)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            _settings.AddAccountHead(accountsHead, userId, concernId);
            return Json("added succes", JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult ReportHead()
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
        public ActionResult ReportHeads()
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            if (concernId > 0 && userId > 0)
            {
                var reports = _settings.ReportHeads(concernId);
                ReportHeadViewModels viewModels = new ReportHeadViewModels()
                {
                    ReportHeads = reports
                };
                return View(viewModels);
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpGet]
        public ActionResult AddReportHead()
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            if (concernId > 0 && userId > 0)
            {
                return View();
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpPost]
        public JsonResult AddReportHead(ReportHead reportHead)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            _settings.AddReportHead(reportHead, userId, concernId);
            return Json("added succes", JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Journal()
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
        public ActionResult Journals(int page = 1)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            if (concernId > 0 && userId > 0)
            {
                var culture = "";
                var journals = _journal.ResponseJournals(culture, concernId, page);
                JournalIndexViewModels viewModels = new JournalIndexViewModels()
                {
                    ResponseJournals = journals
                };
                return View(viewModels);
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpGet]
        public JsonResult JsonJournals(int page = 1)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var culture = "";
            var journals = _journal.ResponseJournals(culture, concernId, page);
            JournalIndexViewModels viewModels = new JournalIndexViewModels()
            {
                ResponseJournals = journals
            };
            return Json(viewModels, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public ActionResult AddJournal()
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            if (concernId > 0 && userId > 0)
            {
                var acoountHeads = _settings.AccountsHeads(concernId);
                AccountHeadsViewModels viewModels = new AccountHeadsViewModels()
                {
                    AccountsHeads = acoountHeads
                };
                return View(viewModels);
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpPost]
        public JsonResult AddJournal(Journal journal)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            _journal.AddJournal(journal, userId, concernId);
            return Json("added succes", JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Ledger()
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var acoountHeads = _settings.AccountsHeads(concernId);
            if (concernId > 0 && userId > 0)
            {
                AccountHeadsViewModels viewModels = new AccountHeadsViewModels()
                {
                    AccountsHeads = acoountHeads
                };

                return View(viewModels);
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });
        }
        [HttpPost]
        public JsonResult Ledger(int id,string fromDate, string toDate)
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);            
            var culture = "";
            var ledger = _report.ResponseLedgers(id, culture, concernId,fromDate, toDate);
            LedgerViewModels viewModels = new LedgerViewModels()
            {
                ResponseLedgers = ledger
            };
            return Json(viewModels, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult TrialBalance()
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var culture = "en-US";
            if (concernId > 0 && userId > 0)
            {
                var trial = _report.TrialBalances(concernId, culture);
                TrailaBalanceViewModels viewModels = new TrailaBalanceViewModels()
                {
                    TrialBalances = trial
                };
                return View(viewModels);
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });

        }
        [HttpGet]
        public ActionResult IncomeStatement()
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var culture = "en-US";
            if (concernId > 0 && userId > 0)
            {
                var income = _report.IncomeStatements(concernId, culture);
                IncomeStatementViewModels viewModels = new IncomeStatementViewModels()
                {
                    IncomeStatements = income
                };
                return View(viewModels);
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });

        }
        [HttpGet]
        public ActionResult BalanceSheet()
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var culture = "en-US";
            if (concernId > 0 && userId > 0)
            {
                var balance = _report.BalanceSheets(concernId, culture);
                BalanceSheetViewModels viewModels = new BalanceSheetViewModels()
                {
                    BalanceSheets = balance
                };
                return View(viewModels);
            }
            return RedirectToAction("LogIn", "GlobalData", new { Area = "Global" });

        }
        [HttpGet]
        public JsonResult VoucherCode()
        {
            var concernId = Convert.ToInt32(Session["ConcernId"]);
            var userId = Convert.ToInt32(Session["UserId"]);
            var date = DateTime.Now.ToString("mmssff");
            var code = concernId + "" + date + "" + userId;
            return Json(code,JsonRequestBehavior.AllowGet);
        }


    }
}