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
        public DataSets WorkDataset = new DataSets();
        public class SupplierDetail 
        { 
            public int UserId { get; set; }
            public string SupplierName { get; set; }
            public string SupplierEmail { get; set; }

            public string Authoritytype { get; set; }

            public string CompanyId { get; set; }
            public string ShopId { get; set; }
            public string ContactNo{ get; set; }
            public string MailAddress{ get; set; }


            public string Description { get; set; }
           
        }
      public class DataSets
      {

        public DataSet TempDataSet = new DataSet();

        public DataSet User = new DataSet();
       }
    }
}