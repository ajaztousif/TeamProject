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
        public abstract int Login(string username, string password);
        public abstract int Registration(string name, string username, string password);
        public abstract bool ChangePassword(string username, string oldPassword, string newPassword);
        public abstract bool DeleteUser(string username);
    }
    public class MssqlContext : DBcontext
    {
        public override int Login(string username, string password)
        {
            //int result;
            //SqlParameter[] objParam = new SqlParameter[2];
            //objParam[0] = new SqlParameter("@username", username);
            //objParam[1] = new SqlParameter("@password", password);
            //Utilitycs.SQLHelper dbHelper = new Utilitycs.SQLHelper();
            //result = (dbHelper.RunSPTemp("spLogin", objParam));
            //return result;

            int res;
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spLogin", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@uname", username);
                cmd.Parameters.AddWithValue("@password", password);
                conn.Open();
                res = (int)cmd.ExecuteScalar();
                //result = Convert.ToBoolean(res);
            }
            return res;
        }

        public override int Registration(string name, string username, string password)
        {
            int result;

            SqlParameter[] objParam = new SqlParameter[3];
            objParam[0] = new SqlParameter("@name", name);
            objParam[1] = new SqlParameter("@username", username);
            objParam[2] = new SqlParameter("@password", password);
            Utilitycs.SQLHelper dbHelper = new Utilitycs.SQLHelper();
            result = (dbHelper.RunSPTemp("spInsertUser", objParam));
            return result;
        }
        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            bool result = false;

            SqlParameter[] objParam = new SqlParameter[3];
            objParam[0] = new SqlParameter("@username", username);
            objParam[1] = new SqlParameter("@oldPassword", oldPassword);
            objParam[2] = new SqlParameter("@newPassword", newPassword);
            Utilitycs.SQLHelper dbHelper = new Utilitycs.SQLHelper();
            result = Convert.ToBoolean(dbHelper.RunSPTemp("spChangePassword", objParam));
            return result;
        }
        public override bool DeleteUser(string username)
        {
            bool result = false;

            SqlParameter[] objParam = new SqlParameter[1];
            objParam[0] = new SqlParameter("@username", username);
            Utilitycs.SQLHelper dbHelper = new Utilitycs.SQLHelper();
            result = Convert.ToBoolean(dbHelper.RunSp("spDeleteUser", objParam));
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