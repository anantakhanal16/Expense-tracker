using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Expense_tracker.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public string Authoritytype { get; set; }

        public string CompanyId { get; set; }
        public string MailAddress { get; set; }

    }
}