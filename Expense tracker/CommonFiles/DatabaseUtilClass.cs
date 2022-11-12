using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Expense_tracker.CommonFiles
{
    public class DatabaseUtilClass
    {
        public static DataSet WorkDataset = new DataSet();
        public static DataSet TempDataSet = new DataSet();
        public static string  cs = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;

        public static string LoginSession = "UserSession";

    }
}