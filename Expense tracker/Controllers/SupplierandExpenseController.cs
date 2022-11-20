using Expense_tracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Expense_tracker.CommonFiles.DatabaseUtilClass;
using static Expense_tracker.CommonFiles.CommonCodes;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Expense_tracker.Controllers
{
    //[Authorize]
    public class SupplierandExpenseController : Controller
    {
        //[Authorize(Roles = "Admin,Customer")]
        public ActionResult SupplierList()
        {
            if(Session[LoginSession] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            Supplier model = new Supplier();
            SupplierList(model);
            return View("~/Views/SupplierandExpense/SupplierList.cshtml", model);
        }
        //[Authorize(Roles = "Admin,Customer")]
        public ActionResult SupplierListAddForm()
        {
            return View();
        }
        public ActionResult SupplierListAddFormComplete()
        {
            Supplier model = new Supplier();

            model.SupDetail.SupplierName = RequestFormData(Request, "venusername");
            model.SupDetail.SupplierEmail = RequestFormData(Request, "venaddress");
            model.SupDetail.Description = RequestFormData(Request, "desc");
            model.SupDetail.ContactNo = RequestFormData(Request, "phone");
            model.SupDetail.MailAddress = RequestFormData(Request, "mailaddress");

            InsertSupplier(model);

            return RedirectToAction("SupplierList");
        }

        private void InsertSupplier(Supplier model)
        {
            StringBuilder sb = new StringBuilder();
            SqlConnection con = new SqlConnection(cs);
            try
            {
                sb.Append("INSERT ");
				sb.Append("INTO T_SUPPLIER( ");
				sb.Append("     COMPANY_ID ");
				sb.Append("    , SHOP_ID ");
				sb.Append("    , SUPPLIER_NAME ");
				sb.Append("    , CONTACT_NUMBER ");
				sb.Append("    , MAIL ");
				sb.Append("    , REG_USER ");
				sb.Append("    , REG_DTM ");
				sb.Append(") ");
				sb.Append("VALUES ( ");
                sb.Append("    @COMPANY_ID");
                sb.Append("   ,@SHOP_ID");
                sb.Append("   , @SUPPLIER_NAME");
                sb.Append("   , @CONTACT_NUMBER");
				sb.Append("   , @MAIL");
				sb.Append("   , @REG_USER ");
				sb.Append("   , @REG_DTM ");
				sb.Append(") ");

				using (SqlCommand cmd = new SqlCommand(sb.ToString(), con))
                {
                  
                    cmd.Parameters.AddWithValue("@SUPPLIER_NAME", model.SupDetail.SupplierName);
                    cmd.Parameters.AddWithValue("@CONTACT_NUMBER", model.SupDetail.ContactNo);
                    cmd.Parameters.AddWithValue("@MAIL", model.SupDetail.MailAddress);
                    cmd.Parameters.AddWithValue("@REG_USER", Auth.Authoritytype);
                    cmd.Parameters.AddWithValue("@REG_DTM", (DateTime.Now.TimeOfDay).ToString());
                    cmd.Parameters.AddWithValue("@COMPANY_ID", Auth.CompanyId);
                    cmd.Parameters.AddWithValue("@SHOP_ID", Auth.ShopId);
                    
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
              
            }
            finally
            {
                //WorkDataset.Tables.Clear();
                con.Close();
            }
        }

        private void SupplierList(Supplier model)
        {
            StringBuilder sb = new StringBuilder();
            SqlConnection con = new SqlConnection(cs);

            try
            {
                sb.Append("SELECT ");
                sb.Append("    SUPPLIER_KEY_ID ");
                sb.Append("    , COMPANY_ID ");
                sb.Append("    , SHOP_ID ");
                sb.Append("    , SUPPLIER_NAME ");
                sb.Append("    , CONTACT_NUMBER ");
                sb.Append("    , MAIL ");
                sb.Append("    , REG_USER ");
                sb.Append("    , REG_DTM ");
                sb.Append("    , UPD_USER ");
                sb.Append("    , UPD_DTM ");
                sb.Append("FROM ");
                sb.Append("   T_SUPPLIER ");
                sb.Append("WHERE ");
                sb.Append("    COMPANY_ID = @COMPANY_ID ");
                sb.Append("    AND SHOP_ID = @SHOP_ID ");
                sb.Append("ORDER BY ");
                sb.Append("    SUPPLIER_KEY_ID ");
                using (SqlCommand cmd = new SqlCommand(sb.ToString(), con))
                {
                    cmd.Parameters.AddWithValue("@COMPANY_ID", Auth.CompanyId);
                    cmd.Parameters.AddWithValue("@SHOP_ID", Auth.ShopId);
                    con.Open();
                    var dataReader = cmd.ExecuteReader();
                    var UserDataTable = new DataTable("UserList");
                    UserDataTable.Load(dataReader);

                    model.WorkDataset.TempDataSet.Tables.Add(UserDataTable);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            finally
            {
               con.Close();
            }
        }
    }
}