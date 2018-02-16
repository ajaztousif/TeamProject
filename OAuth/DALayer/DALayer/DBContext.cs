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
        public abstract bool Login(string username, string password);
        public abstract bool Registration(string name, string username, string password);
        public abstract bool ChangePassword(string username, string oldPassword, string newPassword);
        public abstract bool DeleteUser(string username);
    }
    public class MssqlContext : DBcontext
    {
        public override bool Login(string username, string password)
        {
            bool result = false;

            SqlParameter[] objParam = new SqlParameter[2];
            objParam[0] = new SqlParameter("username", SqlDbType.VarChar);
            objParam[1] = new SqlParameter("password", SqlDbType.VarChar);
            //objParam[0].Value = catType;
            Utilitycs.SQLHelper dbHelper = new Utilitycs.SQLHelper();
            result = Convert.ToBoolean(dbHelper.RunSp("spLogin", objParam));
            return result;            
        }

        public override bool Registration(string name, string username, string password)
        {
            bool result = false;

            SqlParameter[] objParam = new SqlParameter[3];
            objParam[0] = new SqlParameter("name", SqlDbType.VarChar);
            objParam[1] = new SqlParameter("username", SqlDbType.VarChar);
            objParam[2] = new SqlParameter("password", SqlDbType.VarChar);
            Utilitycs.SQLHelper dbHelper = new Utilitycs.SQLHelper();
            result = Convert.ToBoolean(dbHelper.RunSp("spInsertUser", objParam));
            return result;
        }
        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            bool result = false;

            SqlParameter[] objParam = new SqlParameter[3];
            objParam[0] = new SqlParameter("username", SqlDbType.VarChar);
            objParam[1] = new SqlParameter("oldPassword", SqlDbType.VarChar);
            objParam[2] = new SqlParameter("newPassword", SqlDbType.VarChar);
            Utilitycs.SQLHelper dbHelper = new Utilitycs.SQLHelper();
            result = Convert.ToBoolean(dbHelper.RunSp("spChangePassword", objParam));
            return result;
        }
        public override bool DeleteUser(string username)
        {
            bool result = false;

            SqlParameter[] objParam = new SqlParameter[1];
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