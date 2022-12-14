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
    [Authorize]
    public class SupplierandExpenseController : Controller
    {
       //get supplierdata List 
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
        //add supplier Form  
        public ActionResult SupplierListAddForm()
        {
            Supplier model = new Supplier();
            return View(model);
        }

        [HttpPost]
        public ActionResult SupplierListAddFormComplete()
        {
            Supplier model = new Supplier();
            if (Session["SupplierandExpense"] != null)
            {
                model.SupDetail = (Supplier.SupplierDetail)Session["SupplierandExpense"];
            }
            model.SupDetail.SupplierName = RequestFormData(Request, "venusername");
            model.SupDetail.Email = RequestFormData(Request, "mailaddress"); 
            model.SupDetail.Description = RequestFormData(Request, "desc");
            model.SupDetail.ContactNo = RequestFormData(Request, "phone");
            model.SupDetail.MailAddress = RequestFormData(Request, "venaddress");
            if (!(model.SupDetail.Mode == "0") && !(model.SupDetail.Mode == "1"))
            {
                InsertSupplier(model);
            }
            if (model.SupDetail.Mode == "0")
            {
                DeleteSupplierbyid(model);
            }
            if (model.SupDetail.Mode == "1")
            {
                UpdateSupplier(model);
            }
            Session.Remove("SupplierandExpense");
            return RedirectToAction("SupplierList");
        }

        //Edit supplierList
        //[HttpPost]
        public ActionResult EditSupplier(string id)
        {
            Supplier model = new Supplier();
            try 
            {
                getSupplierById(model, id);
                foreach (DataRow row in model.WorkDataset.TempDataSet.Tables[0].Rows)
                {
                    model.SupDetail.SupplierId = row["SUPPLIER_KEY_ID"].ToString();
                    model.SupDetail.SupplierName = row["SUPPLIER_NAME"].ToString();
                    model.SupDetail.Email = row["MAIL"].ToString();
                    model.SupDetail.ContactNo = row["CONTACT_NUMBER"].ToString();
                }
                model.SupDetail.Mode = "1";
                Session["SupplierandExpense"] = model.SupDetail;
                return View("~/Views/SupplierandExpense/SupplierListAddForm.cshtml", model);
            }
            catch (Exception ex)
            {
                Exceptionmessage = ex.Message.ToString();
                return View("~/Views/Shared/Error.cshtml");
            }
        }
        //Delete Supplier
        public ActionResult DeleteSupplier(string id)
        {
            Supplier model = new Supplier();
            try
            {
                getSupplierById(model, id);
                foreach (DataRow row in model.WorkDataset.TempDataSet.Tables[0].Rows)
                {
                    model.SupDetail.SupplierId = row["SUPPLIER_KEY_ID"].ToString();
                    model.SupDetail.SupplierName = row["SUPPLIER_NAME"].ToString();
                    model.SupDetail.Email = row["MAIL"].ToString();
                    model.SupDetail.ContactNo = row["CONTACT_NUMBER"].ToString();
                    
                }
                model.SupDetail.Mode = "0";
                Session["SupplierandExpense"] = model.SupDetail;
                return View("~/Views/SupplierandExpense/SupplierListAddForm.cshtml", model);
            }
            catch(Exception ex)
            {
              return View("~/Views/Shared/Error.cshtml");
            }
              
        }
        //getdata
        //categoryList 
        public ActionResult CategoryList()
        {
            Supplier model = new Supplier();
            CategoryList(model);
            return View("~/Views/SupplierandExpense/CategoryList.cshtml",model);
        }
        //add
        //category form 
        public ActionResult CategoryListAddForm()
        {
            Supplier model = new Supplier();
            SupplierList(model);
            foreach (DataRow suppiler in model.WorkDataset.TempDataSet.Tables[0].Rows)
            {
                model.CatForm.SuppliersLists.Add(suppiler["SUPPLIER_KEY_ID"].ToString(), suppiler["SUPPLIER_NAME"].ToString());
            }

            return View("~/Views/SupplierandExpense/CategoryListAddForm.cshtml", model);
        }
        [HttpPost]
        public ActionResult CategoryAddComplete()
        {
            Supplier model = new Supplier();
            model.CatForm.CategoryName = RequestFormData(Request, "catgory_name");
            model.CatForm.SupplierId = RequestFormData(Request, "supplierid");
            model.CatForm.AvilableFlg = RequestFormData(Request, "aviableflg");
            model.CatForm.Description = RequestFormData(Request, "desc");
            InsertCategory(model);
            return RedirectToAction("CategoryList");
            
        }
      
        public ActionResult EditCategory(string id)
        {
            try
            {
                Supplier model = new Supplier();
                GetCatgoryById(model, id);
                foreach(DataRow Row in model.WorkDataset.TempDataSet.Tables[0].Rows)
                {
                    model.CatForm.CategoryName = Row["CATEGORY_NAME"].ToString();
                    model.CatForm.SupplierId =   Row["SUPPLIER_KEY_ID"].ToString();
                    //model.CatForm.AvilableFlg =  Row["SUPPLIER_KEY_ID"].ToString();
                }
                SupplierList(model);
                foreach (DataRow suppiler in model.WorkDataset.TempDataSet.Tables[1].Rows)
                {
                    model.CatForm.SuppliersLists.Add(suppiler["SUPPLIER_KEY_ID"].ToString(), suppiler["SUPPLIER_NAME"].ToString());
                }
                return View("~/Views/SupplierandExpense/CategoryListAddForm.cshtml", model);
            }
            catch(Exception ex)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            finally
            {
               
            }
        }
       
        public ActionResult DeleteCategory(string id)
        {
            return null;
        }
        //item List form 
      
        public ActionResult ItemsList()
        {
            Supplier model = new Supplier();
            ItemList(model);
            return View(model);
        }
        //item addd form 
      
        public ActionResult ItemListAddForm()
        {
            Supplier model = new Supplier();
            SelectboxData(model);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ItemListAddFormComplete()
        {
            Supplier model = new Supplier();
            model.ItemFormData.SupplierID = RequestFormData(Request, "supplierid");
            model.ItemFormData.CategoryID = RequestFormData(Request, "categoryid");
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
        //insert
        //supplier sql 
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
        //insert category sql 
        private void InsertCategory(Supplier model)
        {
            StringBuilder sb = new StringBuilder();
            SqlConnection con = new SqlConnection(cs);
            try
            {
                
                sb.Append("INSERT ");
				sb.Append("INTO T_SUPPLIER_CATEGORY( ");
	        	sb.Append("     COMPANY_ID ");
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
        //Select category data 
        private void CategoryList(Supplier model, string supplierId = null)
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
                sb.Append("    T_SUPPLIER_CATEGORY");
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
                if(!string.IsNullOrEmpty(supplierId))
                {
                     sb.Append("    AND T_SUPPLIER_CATEGORY.SUPPLIER_KEY_ID=@SUPPLIER_KEY_ID");
                }
                sb.Append("  ORDER BY");
                sb.Append("    CAT_ID ");

                using (SqlCommand cmd = new SqlCommand(sb.ToString(), con))
                {
                    cmd.Parameters.AddWithValue("@COMPANY_ID", Auth.CompanyId);
                    cmd.Parameters.AddWithValue("@SHOP_ID", Auth.ShopId);
                    if (!string.IsNullOrEmpty(supplierId))
                    {
                        cmd.Parameters.AddWithValue("@SUPPLIER_KEY_ID", supplierId);
                    }
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
                con.Close();
            }
        }
        //select supplier list data 
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
        // insert item sql 
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
                sb.Append("    , CAT_ID");
                sb.Append("    , REG_USER ");
                sb.Append(") ");
                sb.Append("VALUES ( ");
                sb.Append("    @COMPANY_ID ");
                sb.Append("    , @SHOP_ID ");
                sb.Append("    , @SUPPLIER_ID ");
                sb.Append("    , @ITEM_NAME ");
                sb.Append("    , @CAT_ID");
                sb.Append("    , @REG_USER ");
               
                sb.Append(");SELECT SCOPE_IDENTITY(); ");
                using (SqlCommand cmd = new SqlCommand(sb.ToString(), con))
                {

                    //cmd.Parameters.AddWithValue("@CATEGORY_NAME", model.ItemFormData.CategoryName);
                    cmd.Parameters.AddWithValue("@SUPPLIER_ID", model.ItemFormData.SupplierID);
                    cmd.Parameters.AddWithValue("@ITEM_NAME", model.ItemFormData.ItemName);
                    cmd.Parameters.AddWithValue("@CAT_ID", model.ItemFormData.CategoryID);

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
                sb.Append("INTO ITEM_SUPPLIER(ITEM_KEY_ID,COMPANY_ID, ITEM_PRICE_TYPE, AMOUNT) ");
                sb.Append("VALUES (@ITEM_KEY_ID, @COMPANY_ID, @ITEM_PRICE_TYPE, @AMOUNT) ");
             
                using (SqlCommand cmd = new SqlCommand(sb.ToString(), con))
                {
                    cmd.Parameters.AddWithValue("@ITEM_KEY_ID", model.ItemFormData.ItemID);
                    cmd.Parameters.AddWithValue("@COMPANY_ID", Auth.CompanyId);
                    cmd.Parameters.AddWithValue("@ITEM_PRICE_TYPE", model.ItemFormData.ItemType);
                    cmd.Parameters.AddWithValue("@AMOUNT", model.ItemFormData.ItemPrice);
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
        //select item sql 
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

        [HttpPost]
        public JsonResult SupplierChange(string supplierId)
        {
            Supplier model = new Supplier();  
            CategoryList(model, supplierId);
            foreach (DataRow suppiler in model.WorkDataset.TempDataSet.Tables[0].Rows)
            {
                model.ItemFormData.CategoryLists.Add(suppiler["CAT_ID"].ToString(), suppiler["CATEGORY_NAME"].ToString());

            };
            var CatList = model.ItemFormData.CategoryLists;
            var returndata = new
            {
                catList = CatList

            };
            return Json(returndata, JsonRequestBehavior.AllowGet);
        }

        //supplierbyid
        //supplierCrud
        private bool getSupplierById(Supplier model,string supplierId)
        {
            StringBuilder sb = new StringBuilder();
            SqlConnection con = new SqlConnection(cs);
            sb.Append(" ");
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
            sb.Append("    T_SUPPLIER ");
            sb.Append("WHERE ");
            sb.Append("    SUPPLIER_KEY_ID = @SUPPLIER_KEY_ID");
            sb.Append("   AND COMPANY_ID = @COMPANY_ID");
            sb.Append("   AND  SHOP_ID = @SHOP_ID ");
            using (SqlCommand cmd = new SqlCommand(sb.ToString(), con))
            {
                cmd.Parameters.AddWithValue("@COMPANY_ID", Auth.CompanyId);
                cmd.Parameters.AddWithValue("@SHOP_ID", Auth.ShopId);
                cmd.Parameters.AddWithValue("@SUPPLIER_KEY_ID", supplierId);
              
                con.Open();
                var dataReader = cmd.ExecuteReader();
                var Supplierbyid = new DataTable("SupplierbyId");
                Supplierbyid.Load(dataReader);

                model.WorkDataset.TempDataSet.Tables.Add(Supplierbyid);
            }


            return true;
        }

        //get
        //category id
        private bool GetCatgoryById(Supplier model, string CategoryId)
        {
            StringBuilder sb = new StringBuilder();
            SqlConnection con = new SqlConnection(cs);
            sb.Append("SELECT ");
            sb.Append("     CATEGORY_NAME ");
            sb.Append("    , REG_USER ");
            sb.Append("    , SUPPLIER_KEY_ID ");
            sb.Append("FROM ");
            sb.Append("    T_SUPPLIER_CATEGORY ");
            sb.Append("WHERE ");
            sb.Append("    CAT_ID = @CAT_ID ");
            sb.Append("ORDER BY ");
            sb.Append("    CAT_ID ");
            using (SqlCommand cmd = new SqlCommand(sb.ToString(), con))
            {
                cmd.Parameters.AddWithValue("@CAT_ID", CategoryId);
                con.Open();
                var dataReader = cmd.ExecuteReader();
                DataTable CatbyCatid = new DataTable("CatbyCatid");
                CatbyCatid.Load(dataReader);
                model.WorkDataset.TempDataSet.Tables.Add(CatbyCatid);
                con.Close();
            }
            return true;   
        }
        private bool UpdateSupplier(Supplier model)
        {
            StringBuilder sb = new StringBuilder();
            SqlConnection con = new SqlConnection(cs);

            sb.Append("UPDATE T_SUPPLIER ");
            sb.Append("SET ");
            sb.Append("     SUPPLIER_NAME = @SUPPLIER_NAME ");
            sb.Append("    , CONTACT_NUMBER = @CONTACT_NUMBER ");
            sb.Append("    , MAIL = @MAIL ");
            sb.Append("    , UPD_USER = @UPD_USER ");
            sb.Append("    , UPD_DTM = @UPD_DTM ");
            sb.Append("    , ADDRESS = @ADDRESS ");
            sb.Append("WHERE ");
            sb.Append("    SUPPLIER_KEY_ID = @SUPPLIER_KEY_ID");
            using (SqlCommand cmd = new SqlCommand(sb.ToString(), con))
            {

                cmd.Parameters.AddWithValue("@SUPPLIER_NAME", model.SupDetail.SupplierName);
                cmd.Parameters.AddWithValue("@CONTACT_NUMBER", model.SupDetail.ContactNo);
                cmd.Parameters.AddWithValue("@ADDRESS", model.SupDetail.MailAddress);
                cmd.Parameters.AddWithValue("@MAIL", model.SupDetail.Email);
                cmd.Parameters.AddWithValue("@SUPPLIER_KEY_ID", model.SupDetail.SupplierId);
                cmd.Parameters.AddWithValue("@UPD_USER", Auth.Authoritytype);
                cmd.Parameters.AddWithValue("@UPD_DTM", (DateTime.Now.TimeOfDay).ToString());
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            return true;
        }

        private bool DeleteSupplierbyid(Supplier model)
        {
            StringBuilder sb = new StringBuilder();
            SqlConnection con = new SqlConnection(cs);

            sb.Append("DELETE ");
            sb.Append("FROM ");
            sb.Append("    T_SUPPLIER ");
            sb.Append("WHERE ");
            sb.Append("    SUPPLIER_KEY_ID = @SUPPLIER_KEY_ID ");
            using (SqlCommand cmd = new SqlCommand(sb.ToString(), con))
            {
                cmd.Parameters.AddWithValue("@SUPPLIER_KEY_ID", model.SupDetail.SupplierId);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            return true;
        }
    }
}