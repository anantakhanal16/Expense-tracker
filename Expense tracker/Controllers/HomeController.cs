using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Expense_tracker.CommonFiles.DatabaseUtilClass;

namespace Expense_tracker.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            if (Session[LoginSession] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
       
        }
    }
}