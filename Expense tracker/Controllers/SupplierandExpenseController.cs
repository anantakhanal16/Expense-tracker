using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Expense_tracker.CommonFiles.DatabaseUtilClass;

namespace Expense_tracker.Controllers
{
    [Authorize]
    public class SupplierandExpenseController : Controller
    {
        [Authorize(Roles = "Admin,Customer")]
        public ActionResult SupplierList()
        {
          return View("~/Views/SupplierandExpense/SupplierList.cshtml");
        }
        [Authorize(Roles = "Admin,Customer")]
        public ActionResult SupplierListAddForm()
        {
            return View();
            
        }
        public ActionResult SupplierListAddFormComplete()
        {

            return View();

        }
    }
}