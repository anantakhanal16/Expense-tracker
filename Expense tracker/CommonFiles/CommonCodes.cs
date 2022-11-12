using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Expense_tracker.CommonFiles
{
    public class CommonCodes
    {
        public static string RequestFormData(HttpRequestBase p_Request, string name)
        {
            return FilterString(p_Request.Form[name]);
        }

        public static string FilterString(string p_string, int p_type = 0)
        {
            if (p_string == null)
            {
                p_string = "";
            }
            if (p_string.Contains(','))
            {
                p_string = p_string.Replace(",", "");

            }
            string strData = p_string;
            return strData;
        }
    }
}