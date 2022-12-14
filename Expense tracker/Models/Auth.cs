using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Expense_tracker.Models
{
    public class Auth 
    {
    
        public UserAuth UserLogAuth  = new UserAuth();
    
        public DataSets WorkDataset = new DataSets();
        public class UserAuth
        {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public string Authoritytype { get; set; }

        public string CompanyId { get; set; }
        public string MailAddress { get; set; }
        
        public string LoginSession = "UserSession";
        }
        public class DataSets
        {

            public DataSet TempDataSet = new DataSet();

            public DataSet User = new DataSet();
        }

    }
}