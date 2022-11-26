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
using static Expense_tracker.Models.Supplier;

namespace Expense_tracker.Controllers
{
    //[Authorize]
    public class SupplierandExpenseController : Controller
    {
        //[Authorize(Roles = "Admin,Customer")]
        public ActionResult SupplierList()
        {
            if (Session[LoginSession] == null)
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

        public ActionResult CategoryList()
        {
            Supplier model = new Supplier();
            CategoryList(model);
            return View("~/Views/SupplierandExpense/CategoryList.cshtml",model);
        }

        public ActionResult CategoryListAddForm()
        {
            Supplier model = new Supplier();
            SupplierList(model);
            foreach (DataRow suppiler in model.WorkDataset.TempDataSet.Tables[0].Rows)
            {
                model.SupDetail.SuppliersLists.Add(suppiler["SUPPLIER_KEY_ID"].ToString(), suppiler["SUPPLIER_NAME"].ToString());
            }

            return View("~/Views/SupplierandExpense/CategoryListAddForm.cshtml", model);
        }
        public ActionResult CategoryAddComplete()
        {
            Supplier model = new Supplier();
            model.CatForm.CategoryName = RequestFormData(Request, "catgory_name");
            model.CatForm.SupplierId = RequestFormData(Request, "supplierkey");
            model.CatForm.AvilableFlg = RequestFormData(Request, "aviableflg");
            model.CatForm.Description = RequestFormData(Request, "desc");
            InsertCategory(model);
            return RedirectToAction("CategoryList");
            
        }

        public ActionResult ItemsList()
        {
            Supplier model = new Supplier();
            ItemList(model);
            return View(model);
        }
        public ActionResult ItemListAddForm()
        {
            Supplier model = new Supplier();
            SelectboxData(model);
            return View(model);
        }
        public ActionResult ItemListAddFormComplete()
        {
            Supplier model = new Supplier();
            model.ItemFormData.SupplierID = RequestFormData(Request, "supplierid");
            model.ItemFormData.CategoryName = RequestFormData(Request, "categoryid");
            model.ItemFormData.ItemName = RequestFormData(Request, "itemname");
            model.ItemFormData.ItemPrice = RequestFormData(Request, "itemprice");
            model.ItemFormData.AvaliableFlg = RequestFormData(Request, "aviableflg");
            model.ItemFormData.ItemType = RequestFormData(Request, "itemtype");
            model.ItemFormData.Remarks = RequestFormData(Request, "remarks");
            InsertItem(model);
            InsertItemDetails(model);
            return RedirectToAction("ItemsList");
        }
        private void SelectboxData(Supplier model)
        {
            SupplierList(model);
            foreach (DataRow suppiler in model.WorkDataset.TempDataSet.Tables[0].Rows)
            {
                model.ItemFormData.SuppliersLists.Add(suppiler["SUPPLIER_KEY_ID"].ToString(), suppiler["SUPPLIER_NAME"].ToString());

            }
            CategoryList(model);
            foreach (DataRow suppiler in model.WorkDataset.TempDataSet.Tables[1].Rows)
            {
                model.ItemFormData.CategoryLists.Add(suppiler["CAT_ID"].ToString(), suppiler["CATEGORY_NAME"].ToString());
                
            }

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

        private void InsertCategory(Supplier model)
        {
            StringBuilder sb = new StringBuilder();
            SqlConnection con = new SqlConnection(cs);
            try
            {
                
                sb.Append("INSERT ");
				sb.Append("INTO T_SUPPLIER_CATEGORY( ");
	        	sb.Append("     COM_ID ");
				sb.Append("    , SHOP_ID ");
				sb.Append("    , CATEGORY_NAME ");
				sb.Append("    , REG_USER ");
				sb.Append("    , REG_DTM ");
				sb.Append("    , SUPPLIER_KEY_ID ");
				sb.Append(") ");
				sb.Append("VALUES ( ");
				sb.Append("     @COMPANY_ID ");
				sb.Append("    , @SHOP_ID ");
				sb.Append("    , @CATEGORY_NAME");
				sb.Append("    , @REG_USER");
				sb.Append("    , @REG_DTM");
				sb.Append("    , @SUPPLIER_KEY_ID ");
				sb.Append(") ");

				using (SqlCommand cmd = new SqlCommand(sb.ToString(), con))
                {

                    cmd.Parameters.AddWithValue("@CATEGORY_NAME", model.CatForm.CategoryName);
                    cmd.Parameters.AddWithValue("@SUPPLIER_KEY_ID", model.CatForm.SupplierId);
                   
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

        private void CategoryList(Supplier model)
        {
            StringBuilder sb = new StringBuilder();
            SqlConnection con = new SqlConnection(cs);
            try 
            {

                
                sb.Append("SELECT ");
                sb.Append("   T_SUPPLIER_CATEGORY.CAT_ID ");
                sb.Append("  ,T_SHOP.SHOP_NAME ");
                sb.Append("  ,T_SUPPLIER.SUPPLIER_NAME ");
                sb.Append("  , T_SUPPLIER_CATEGORY.CATEGORY_NAME");
                sb.Append("   FROM");
                sb.Append("    T_SUPPLIER_CATEGORY ");
                sb.Append("    JOIN ");
                sb.Append("    T_SUPPLIER ");
                sb.Append("    ON ");
                sb.Append("    T_SUPPLIER.SUPPLIER_KEY_ID = T_SUPPLIER_CATEGORY.SUPPLIER_KEY_ID ");
                sb.Append("    AND T_SUPPLIER.SHOP_ID = T_SUPPLIER_CATEGORY.SHOP_ID ");
                sb.Append("    AND T_SUPPLIER.COMPANY_ID =T_SUPPLIER_CATEGORY.COMPANY_ID ");
                sb.Append("    JOIN ");
                sb.Append("     T_SHOP ");
                sb.Append("     ON ");
                sb.Append("     T_SUPPLIER_CATEGORY.SHOP_ID=T_SHOP.SHOP_ID");
                sb.Append("     WHERE");
                sb.Append("    T_SUPPLIER_CATEGORY.COMPANY_ID=@COMPANY_ID");
                sb.Append("    AND T_SUPPLIER_CATEGORY.SHOP_ID=@SHOP_ID");
                sb.Append("  ORDER BY");
                sb.Append("    CAT_ID ");

                using (SqlCommand cmd = new SqlCommand(sb.ToString(), con))
                {
                    cmd.Parameters.AddWithValue("@COMPANY_ID", Auth.CompanyId);
                    cmd.Parameters.AddWithValue("@SHOP_ID", Auth.ShopId);
                    con.Open();
                    var dataReader = cmd.ExecuteReader();
                    var CategoryListTable = new DataTable("CategoryList");
                    CategoryListTable.Load(dataReader);

                    model.WorkDataset.TempDataSet.Tables.Add(CategoryListTable);
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

        private void InsertItem(Supplier model)
        {
            StringBuilder sb = new StringBuilder();
            SqlConnection con = new SqlConnection(cs);

            try
            {
                sb.Append("INSERT ");
                sb.Append("INTO SUPPLIER_ITEM_DETAILS( ");
                sb.Append("    COMPANY_ID ");
                sb.Append("    , SHOP_ID ");
                sb.Append("    , SUPPLIER_ID ");
                sb.Append("    , ITEM_NAME ");
                sb.Append("    , REG_USER ");
                sb.Append(") ");
                sb.Append("VALUES ( ");
                sb.Append("    @COMPANY_ID ");
                sb.Append("    , @SHOP_ID ");
                sb.Append("    , @SUPPLIER_ID ");
                sb.Append("    , @ITEM_NAME ");
                sb.Append("    , @REG_USER ");
               
                sb.Append(");SELECT SCOPE_IDENTITY(); ");
                using (SqlCommand cmd = new SqlCommand(sb.ToString(), con))
                {

                    //cmd.Parameters.AddWithValue("@CATEGORY_NAME", model.ItemFormData.CategoryName);
                    cmd.Parameters.AddWithValue("@SUPPLIER_ID", model.ItemFormData.SupplierID);
                    cmd.Parameters.AddWithValue("@ITEM_NAME", model.ItemFormData.ItemName);

                    cmd.Parameters.AddWithValue("@REG_USER", Auth.Authoritytype);
                    cmd.Parameters.AddWithValue("@REG_DTM", (DateTime.Now.TimeOfDay).ToString());
                    cmd.Parameters.AddWithValue("@COMPANY_ID", Auth.CompanyId);
                    cmd.Parameters.AddWithValue("@SHOP_ID", Auth.ShopId);

                    con.Open();
                    //cmd.ExecuteNonQuery();
                    model.ItemFormData.ItemID = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                SystemException.ReferenceEquals(model, ex);
            }

        }

        private void InsertItemDetails(Supplier model)
        {
            StringBuilder sb = new StringBuilder();
            SqlConnection con = new SqlConnection(cs);
            try
            {
                sb.Append("INSERT ");
                sb.Append("INTO ITEM_SUPPLIER(ITEM_KEY_ID,COMP_KEY_ID, ITEM_PRICE_TYPE, AMOUNT) ");
                sb.Append("VALUES (@ITEM_KEY_ID, @COMPANY_ID, @ITEM_PRICE_TYPE, @AMOUNT) ");
             
                using (SqlCommand cmd = new SqlCommand(sb.ToString(), con))
                {

                    cmd.Parameters.AddWithValue("@CATEGORY_NAME", model.ItemFormData.CategoryName);
                    cmd.Parameters.AddWithValue("@ITEM_PRICE_TYPE", model.ItemFormData.ItemType);
                    cmd.Parameters.AddWithValue("@AMOUNT", model.ItemFormData.ItemPrice);
                    cmd.Parameters.AddWithValue("@COMPANY_ID", Auth.CompanyId);
                    cmd.Parameters.AddWithValue("@ITEM_KEY_ID", model.ItemFormData.ItemID);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                SystemException.ReferenceEquals(model, ex);
            }
           
        }

        private void ItemList(Supplier model)
        {
            StringBuilder sb = new StringBuilder();
            SqlConnection con = new SqlConnection(cs);
            try
            { 
            sb.Append("SELECT");
            sb.Append("     ITEM_SUPPLIER.ITEM_KEY_ID");
            sb.Append("    ,T_SUPPLIER.SUPPLIER_NAME ");
            sb.Append("    , T_SUPPLIER_CATEGORY.CATEGORY_NAME ");
            sb.Append("    , SUPPLIER_ITEM_DETAILS.ITEM_NAME ");
            sb.Append("    , REFRENCES.NAME ");
            sb.Append("    , ITEM_SUPPLIER.AMOUNT ");
            sb.Append("FROM ");
            sb.Append("    SUPPLIER_ITEM_DETAILS JOIN ITEM_SUPPLIER ");
            sb.Append("        ON ITEM_SUPPLIER.ITEM_KEY_ID = SUPPLIER_ITEM_DETAILS.ITEM_KEY_ID ");
            sb.Append("        AND ITEM_SUPPLIER.COMPANY_ID = SUPPLIER_ITEM_DETAILS.COMPANY_ID ");
            sb.Append("    LEFT JOIN T_SUPPLIER ");
            sb.Append("        ON T_SUPPLIER.SUPPLIER_KEY_ID = SUPPLIER_ITEM_DETAILS.SUPPLIER_ID ");
            sb.Append("        AND T_SUPPLIER.SHOP_ID = SUPPLIER_ITEM_DETAILS.SHOP_ID ");
            sb.Append("    LEFT JOIN T_SUPPLIER_CATEGORY ");
            sb.Append("        ON SUPPLIER_ITEM_DETAILS.CAT_ID = T_SUPPLIER_CATEGORY.CAT_ID ");
            sb.Append("        AND SUPPLIER_ITEM_DETAILS.COMPANY_ID = T_SUPPLIER_CATEGORY.COMPANY_ID ");
            sb.Append("        AND SUPPLIER_ITEM_DETAILS.SHOP_ID = T_SUPPLIER_CATEGORY.SHOP_ID ");
            sb.Append("    LEFT JOIN REFRENCES ");
            sb.Append("        ON REFRENCES.ITEM_PRICE_TYPE = ITEM_SUPPLIER.ITEM_PRICE_TYPE ");
            sb.Append("        AND REFRENCES.COMPANY_ID = SUPPLIER_ITEM_DETAILS.COMPANY_ID ");
            sb.Append("WHERE ");
            sb.Append("    SUPPLIER_ITEM_DETAILS.COMPANY_ID = @COMPANY_ID");
            sb.Append("    AND SUPPLIER_ITEM_DETAILS.SHOP_ID = @SHOP_ID ");
            using (SqlCommand cmd = new SqlCommand(sb.ToString(), con))
           {
                cmd.Parameters.AddWithValue("@COMPANY_ID", Auth.CompanyId);
                cmd.Parameters.AddWithValue("@SHOP_ID", Auth.ShopId);
                con.Open();
                var dataReader = cmd.ExecuteReader();

                DataTable ItemTable = new DataTable("DT_ITEM");
                ItemTable.Load(dataReader);

                model.WorkDataset.TempDataSet.Tables.Add(ItemTable);

                }
            }
            catch (Exception ex)
            {
                SystemException.ReferenceEquals(model, ex);
            }
            finally
            {
                con.Close();
            }
        }
    }
}