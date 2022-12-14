using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Expense_tracker.Models;
using System.Web.Security;
using static Expense_tracker.CommonFiles.CommonCodes;
using static Expense_tracker.CommonFiles.DatabaseUtilClass;
namespace Expense_tracker.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            Auth model = new Auth();
            return View("~/Views/Login/Login.cshtml",model);
        }

        [HttpPost]
        public ActionResult LoginConfirm()
        {

            Auth model = new Auth();

            model.UserLogAuth.UserName = RequestFormData(Request,"userName");
            model.UserLogAuth.Password = RequestFormData(Request, "password");

           if(AuthProcess(model))
           {
                FormsAuthentication.SetAuthCookie(model.UserLogAuth.UserName, false);
                ViewBag.Message = "Welcome To Page";
                return RedirectToAction("Index","Home");
           }
           else 
           {
                ViewBag.Message = "No User  Found";
                return RedirectToAction("Index");
           }
            
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
        private bool AuthProcess(Auth model)
        {
            model.WorkDataset.User = SelectUser(model);
          
            foreach (DataTable table in model.WorkDataset.User.Tables)
            { 
                if ((table.Rows.Count != 0 )&& (table.Rows.Count == 1))
                {
                    DataRow UserDetail = model.WorkDataset.User.Tables[0].Rows[0];
                    model.UserLogAuth.UserName = UserDetail["USERNAME"].ToString();
                    model.UserLogAuth.Authoritytype = UserDetail["AUTHORITY_TYPE"].ToString();
                    model.UserLogAuth.CompanyId = UserDetail["COMPANY_ID"].ToString();
                    Session[LoginSession] = model.UserLogAuth;
                    return true;
                }
            }  
            return false;
        }
        private DataSet SelectUser(Auth model)
        {
            StringBuilder SqlBuilder = new StringBuilder();   
            SqlConnection con = new SqlConnection(cs);
            try
            {
                SqlBuilder.AppendLine("      SELECT");
                SqlBuilder.AppendLine("      USER_ID");
                SqlBuilder.AppendLine("     ,USERNAME");
                SqlBuilder.AppendLine("     ,EMAIL");
                SqlBuilder.AppendLine("     ,AUTHORITY_TYPE");
                SqlBuilder.AppendLine("     ,COMPANY_ID");
                SqlBuilder.AppendLine("     ,MAIL_ADDRESS");
                SqlBuilder.AppendLine("     FROM");
                SqlBuilder.AppendLine("     T_USER");
                SqlBuilder.AppendLine("     WHERE");
                SqlBuilder.AppendLine("     USERNAME = @USER_NAME");
                SqlBuilder.AppendLine("     AND PASSWORD = @PASSWORD");

                using (SqlCommand cmd = new SqlCommand(SqlBuilder.ToString(), con))
                {

                    cmd.Parameters.AddWithValue("@USER_NAME", model.UserLogAuth.UserName);
                    cmd.Parameters.AddWithValue("@PASSWORD", model.UserLogAuth.Password);
                    var UserDataTable = new DataTable("UserList");
                    con.Open();

                    var dataReader = cmd.ExecuteReader();
                    UserDataTable.Load(dataReader);

                    model.WorkDataset.User.Tables.Add(UserDataTable);
                }
                return model.WorkDataset.User;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;

            }
            finally
            {
                //WorkDataset.Tables.Clear();
                con.Close();
            }
        }

   
    }
}