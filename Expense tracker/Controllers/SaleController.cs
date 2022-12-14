using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Expense_tracker.CommonFiles.DatabaseUtilClass;
using static Expense_tracker.CommonFiles.CommonCodes;
namespace Expense_tracker.Controllers
{
    public class SaleController : Controller
    {
        // GET: Sale
        public ActionResult Index()
        {
            if (Session[LoginSession] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }
        public ActionResult SalesComplete()
        {
            return null;
        }
    }
}