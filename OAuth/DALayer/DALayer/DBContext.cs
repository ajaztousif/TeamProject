using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DALayer
{
    //public class User
    //{
    //    public string name;
    //    public string username;
    //    public string password;

    //}
    public abstract class DBcontext
    {
        public abstract bool login(string username, string password);
        public abstract bool registration(string name, string username, string password);
        public abstract bool changePassword(string username, string oldPassword, string newPassword);
        public abstract bool deleteUser(string username);
    }
    public class MssqlContext : DBcontext
    {
        public override bool login(string username, string password)
        {
            bool result = false;
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spLogin", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter parameterName = new SqlParameter("@username", username);
                SqlParameter parameterPassword = new SqlParameter("@password", password);
                cmd.Parameters.Add(parameterName);
                cmd.Parameters.Add(parameterPassword);
                conn.Open();
                bool res = Convert.ToBoolean(cmd.ExecuteReader());

                return result;
            }
        }
        public override bool registration(string name, string username, string password)
        {
            bool result = false;
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
                result = Convert.ToBoolean(cmd.ExecuteReader());
            }
            return result;
        }
        public override bool changePassword(string username, string oldPassword, string newPassword)
        {
            bool result = false;
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spChangePassword", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter parameterUserName = new SqlParameter("@username", username);
                SqlParameter parameterOldPassword = new SqlParameter("@oldPassword", oldPassword);
                SqlParameter parameterNewPassword = new SqlParameter("@newPassword", newPassword);
                cmd.Parameters.Add(parameterUserName);
                cmd.Parameters.Add(parameterOldPassword);
                cmd.Parameters.Add(parameterNewPassword);
                conn.Open();
                result = Convert.ToBoolean(cmd.ExecuteReader());
            }
            return result;
        }
        public override bool deleteUser(string username)
        {
            bool result = false;
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spDeleteUser", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter parameterUserName = new SqlParameter("@username", username);
                cmd.Parameters.Add(parameterUserName);
                conn.Open();
                result = Convert.ToBoolean(cmd.ExecuteReader());
            }
            return result;
        }
    }
    //public class MysqlContext : DBcontext
    //{

    //}
    public abstract class Creator
    {
        public abstract DBcontext FactoryMethod();
    }
    public class MssqlCreator : Creator
    {
        public override DBcontext FactoryMethod()
        {

            return new MssqlContext();
        }
    }
    //public class MysqlCreator : Creator
    //{
    //    public override DBcontext FactoryMethod()
    //    {

    //        return new MysqlContext();
    //    }
    //}
}