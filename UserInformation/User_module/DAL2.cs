using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace User_module
{
    //public class User
    //{
    //    public string name;
    //    public string username;
    //    public string password;

    //}
    public abstract class DBcontextTime
    {
         public abstract DataSet Login(string username);
       
        public abstract int Registration(string firstname, string lastname, string email, string username, string password, string phno, string deptid);

        public abstract bool ChangePassword(string username, string oldPassword, string newPassword);
        public abstract bool DeleteUser(string username);
    }
    public class MssqlContext : DBcontextTime
    {
        public override DataSet Login(string username)
        {
            DataSet ds=new DataSet() ;
            SqlParameter[] objParam = new SqlParameter[1];
            objParam[0] = new SqlParameter("@username", username);

            Utilitycs.SQLHelper dbHelper = new Utilitycs.SQLHelper();
            ds = dbHelper.RunSpReturnDs("spReturnUserId", objParam);
            return ds;
        }

        public override int Registration(string firstname, string lastname, string email, string username, string password, string phno, string deptid)
        {
            int result;

            SqlParameter[] objParam = new SqlParameter[3];
            objParam[0] = new SqlParameter("@firstname", firstname);
            objParam[1] = new SqlParameter("@lastname", lastname);
            objParam[2] = new SqlParameter("@email", email);
            objParam[3] = new SqlParameter("@username", username);
            objParam[4] = new SqlParameter("@password", password);
            objParam[5] = new SqlParameter("@phno", phno);
            objParam[6] = new SqlParameter("@deptid", deptid);
           Utilitycs.SQLHelper dbHelper = new Utilitycs.SQLHelper();
            result = Convert.ToInt32(dbHelper.RunSpReturnScalar("spInsertUser", objParam));
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
        public abstract DBcontextTime FactoryMethod();
    }
    public class MssqlCreator : Creator
    {
        public override DBcontextTime FactoryMethod()
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