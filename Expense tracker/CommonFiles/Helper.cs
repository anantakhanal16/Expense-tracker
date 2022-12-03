//using Expense_tracker.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using System.Web.Routing;
//using static Expense_tracker.CommonFiles.DatabaseUtilClass;


//namespace Expense_tracker.CommonFiles
//{
   
//        public class Helper:ActionFilterAttribute
//       {


//        public class FilterAllActions : ActionFilterAttribute, IActionFilter, IResultFilter
//        {
//            public  void OnActionExecuted(ActionExecutedContext filterContext)
//            {
//                if (string.IsNullOrEmpty(Auth.Authoritytype))
//                {
//                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
//                    {
//                        controller = "Login",
//                        action = "Index"

//                    }));

//                }
//                base.OnActionExecuted(filterContext);
//            }
      

//        //    public void OnActionExecuted(ActionExecutedContext filterContext)
//        //    {
//        //        throw new System.NotImplementedException();
//        //    }

//        //    public void OnResultExecuting(ResultExecutingContext filterContext)
//        //    {
//        //        throw new System.NotImplementedException();
//        //    }

//        //    public void OnResultExecuted(ResultExecutedContext filterContext)
//        //    {
//        //        throw new System.NotImplementedException();
//        //    }
//        }


    
//    }
//    }
    
