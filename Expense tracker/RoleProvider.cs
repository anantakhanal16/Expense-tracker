using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using static Expense_tracker.CommonFiles.CommonCodes;
using static Expense_tracker.CommonFiles.DatabaseUtilClass;

namespace Expense_tracker
{
    public class WebRoleProvider : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            var result = RoleArray(username);
            string []RoleList = result.ToArray();
            return RoleList;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            
                throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
        private  List<string> RoleArray(string  username)
        {
            StringBuilder SqlBuilder = new StringBuilder();
            SqlConnection con = new SqlConnection(cs);

            try
            {
                SqlBuilder.AppendLine("      SELECT");
                SqlBuilder.AppendLine("      ROLE");
                SqlBuilder.AppendLine("     FROM ");
                SqlBuilder.AppendLine("     T_USERROLE");
                SqlBuilder.AppendLine("     join");
                SqlBuilder.AppendLine("     T_USER");
                SqlBuilder.AppendLine("     ON");
                SqlBuilder.AppendLine("     T_USER.USER_ID=T_USERROLE.USER_ID");
                SqlBuilder.AppendLine("     WHERE");
                SqlBuilder.AppendLine("     USERNAME = @USER_NAME");

                using (SqlCommand cmd = new SqlCommand(SqlBuilder.ToString(), con))
                {

                    cmd.Parameters.AddWithValue("@USER_NAME", username);
                   
                    var UserDataTable = new DataTable("UserList");
                    con.Open();
                    var dataReader = cmd.ExecuteReader();
                    UserDataTable.Load(dataReader);
                    var FriendsList = new List<string>();
                    foreach (DataRow friend in UserDataTable.Rows)
                    {
                        FriendsList.Add(friend["ROLE"].ToString());
                    }
                    return FriendsList;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;

            }
            finally
            {
                con.Close();
            }

        }
    }
}