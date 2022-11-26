using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Expense_tracker.Models
{

    public class Supplier
    {
        public SupplierDetail SupDetail = new SupplierDetail();
        public CategoryForm CatForm = new CategoryForm();
        public ItemForm ItemFormData = new ItemForm();
        public DataSets WorkDataset = new DataSets();

        public class SupplierDetail
        {
            public int UserId { get; set; }
            public string SupplierName { get; set; }
            public string SupplierEmail { get; set; }
            public string Authoritytype { get; set; }
            public string CompanyId { get; set; }
            public string ShopId { get; set; }
            public string ContactNo { get; set; }
            public string MailAddress { get; set; }
            public string Description { get; set; }

            public Dictionary<string, string> SuppliersLists = new Dictionary<string, string>();

        }
        public class CategoryForm
        {
            public string CategoryName { get; set; }

            public string SupplierId { get; set; }

            public string AvilableFlg { get; set; }

            public string Description { get; set; }

        }
        public class ItemForm
        {
            public int ItemID { get; set; }
            public string SupplierID { get; set; }
            public string CategoryName { get; set; }
            public string ItemName { get; set; }
            public string ItemPrice { get; set; }
            
            public string AvaliableFlg { get; set; }
            public string ItemType { get; set; }
            public string Remarks { get; set; }

            public Dictionary<string, string> SuppliersLists = new Dictionary<string, string>();
            public Dictionary<string, string> CategoryLists = new Dictionary<string, string>();
        }
      public class DataSets
      {

        public DataSet TempDataSet = new DataSet();

        public DataSet User = new DataSet();
       }
    }
}