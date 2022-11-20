using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Expense_tracker.Controllers
{
    public class EmployeeSalaryController : Controller
    {
        
        public ActionResult Index()
        {
            return View("~/Views/EmployeeSalary/EmpSalaryList.cshtml");
        }
    }
}