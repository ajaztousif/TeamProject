using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DALayer
{
    public class DBcon
    {
        public int Registration(string name, string username, string password)
        {
            int res;
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spInsertUser", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter parameterName = new SqlParameter("@name", name);
                SqlParameter parameterUserName = new SqlParameter("@username", username);
                SqlParameter parameterPassword = new SqlParameter("@password", password);
                cmd.Parameters.Add(parameterName);
                cmd.Parameters.Add(parameterUserName);
                cmd.Parameters.Add(parameterPassword);
                conn.Open();
                res = cmd.ExecuteNonQuery();
                //result = Convert.ToBoolean(res);
            }
            return res;
        }

        public int Login(string username, string password)
        {
            int res=1;
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spLogin", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter parameterUserName = new SqlParameter("@username", username);
                SqlParameter parameterPassword = new SqlParameter("@password", password);
                cmd.Parameters.Add(parameterUserName);
                cmd.Parameters.Add(parameterPassword);
                conn.Open();
                res = cmd.ExecuteNonQuery();
                //result = Convert.ToBoolean(res);
            }
            return res;
        }
    }
}
//open connection
//create command
//give command type
//add parameters