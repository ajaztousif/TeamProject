using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace OAuthAPI
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "OAuthService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select OAuthService.svc or OAuthService.svc.cs at the Solution Explorer and start debugging.
    public class OAuthService : IOAuthService
    {
        public string InsertData(User user)
        {
            string Message;
            string conn = ConfigurationManager.ConnectionStrings["UserTimeConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conn))
            {

                SqlCommand cmd = new SqlCommand("spnrgstr", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("pEmail", user.Email);
                cmd.Parameters.AddWithValue("pPassword", user.Password);
                cmd.Parameters.AddWithValue("pCnfirm", user.ConfirmPassword);
                cmd.Parameters.AddWithValue("pFirstName", user.FirstName);
                cmd.Parameters.AddWithValue("pLastName", user.LastName);
                con.Open();
                int returnCode = (int)cmd.ExecuteScalar();
                if (returnCode == -1)
                {
                    Message = "Email Id already Exists, Please give another email Id";
                }
                else
                {
                    Message = "User successfully Inserted";
                    con.Close();
                }
            }
            return Message;
        }
    }
}
