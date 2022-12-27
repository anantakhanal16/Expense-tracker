using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Expense_tracker.CommonFiles.DatabaseUtilClass;
using static Expense_tracker.CommonFiles.CommonCodes;
using static Expense_tracker.Models.Sale;
using System.Data;

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
            SaleItem model = new SaleItem();

            return View(model);
        }
        public ActionResult SalesComplete()
        {
            return null;
        }

        [HttpPost]
        public ActionResult NewRowtable()
        {
            SaleItem model = new SaleItem();
            if (Session["SaleData"] != null)
            {
                model = (SaleItem)Session["SaleData"];
            }
            model.ItemNo = RequestFormData(Request, "itemno");
            model.ItemName = RequestFormData(Request, "itemname");
            model.Description = RequestFormData(Request, "description");
            model.UnitPrice = Convert.ToInt32(RequestFormData(Request, "unitprice"));
            model.Quantity = Convert.ToInt32(RequestFormData(Request, "quantity"));
            model.Discount = Convert.ToInt32(RequestFormData(Request, "discount"));
            model.Amount = (model.UnitPrice * model.Quantity) - model.Discount;

            if (Session["SaleData"] == null)
            {
                model.SaleTable = new DataTable();
                model.SaleTable.Columns.Add("ItemNo");
                model.SaleTable.Columns.Add("ItemName");
                model.SaleTable.Columns.Add("Description");
                model.SaleTable.Columns.Add("UnitPrice");
                model.SaleTable.Columns.Add("Quantity");
                model.SaleTable.Columns.Add("Discount");
                model.SaleTable.Columns.Add("Amount");
            }

            DataRow row = model.SaleTable.NewRow();
            row["ItemNo"] = model.ItemNo;
            row["ItemName"] = model.ItemName;
            row["Description"] = model.Description;
            row["UnitPrice"] = model.UnitPrice;
            row["Quantity"] = model.Quantity;
            row["Discount"] = model.Discount;
            row["Amount"] = model.Amount;

            model.SaleTable.Rows.Add(row);
            CalculateTotal(model);
            Session["SaleData"] = model;
            return View("~/Views/Sale/Index.cshtml", model);
        }

        public void CalculateTotal(SaleItem model)
        {
            var totalrownumber = RequestFormData(Request, "rowcount");
            model.TotalTable = new DataTable();
            model.TotalTable.Columns.Add("Total Items");
            model.TotalTable.Columns.Add("Total Discount Amount");
            model.TotalTable.Columns.Add("Total Amount");
            
            var Amount = 0;
            var DiscountAmt = 0;
            foreach (DataRow Row in model.SaleTable.Rows)
            {
                var totalamount = Convert.ToInt32(Row["Amount"].ToString()) + Amount;
                Amount = totalamount;
                var TotalDiscountAmount = Convert.ToInt32(Row["Discount"].ToString()) + DiscountAmt;
                DiscountAmt = TotalDiscountAmount;

            }
            var totalitems = model.SaleTable.Rows.Count;
            DataRow row = model.TotalTable.NewRow();
            row["Total Amount"] = Amount;
            row["Total Items"] = totalitems;
            row["Total Discount Amount"] = DiscountAmt;
            model.TotalTable.Rows.Add(row);
            
        }
    }
}