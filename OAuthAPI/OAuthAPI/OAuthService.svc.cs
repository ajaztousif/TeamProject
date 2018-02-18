﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using Newtonsoft.Json;

using System.Web.Script.Serialization;

namespace OAuthAPI
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "OAuthService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select OAuthService.svc or OAuthService.svc.cs at the Solution Explorer and start debugging.
    public class OAuthService : IOAuthService
    {
        private SqlCommand mCmd;
        string conn = ConfigurationManager.ConnectionStrings["UserInformation"].ConnectionString;
        public string ChangePassword(string username, string password, string confirmpassword)
        {
            string Message;
            //SQLHelper sql = new SQLHelper();
            //SqlParameter[] param =
            //{
            //    new SqlParameter("Username",username),
            //    new SqlParameter("Password", password),
            //    new SqlParameter("CPassword", confirmpassword)
            //};
            //sql.PrepareCommand("spcpwd", param);
            string conn = ConfigurationManager.ConnectionStrings["UserInformation"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conn))
            {

                SqlCommand cmd = new SqlCommand("spcpwd", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Username", username);
                cmd.Parameters.AddWithValue("Password", password);
                cmd.Parameters.AddWithValue("cPassword", confirmpassword);
                //cmd.Parameters.AddWithValue("pCnfirm", user.ConfirmPassword);
                //cmd.Parameters.AddWithValue("pFirstName", user.FirstName);
                //cmd.Parameters.AddWithValue("pLastName", user.LastName);
                con.Open();
                int returnCode = (int)cmd.ExecuteScalar();
                if (returnCode == -1)
                {
                    Message = "Fail";
                }
                else
                {
                    Message = "Success";
                    con.Close();
                }
            }
            return (new JavaScriptSerializer().Serialize(Message));
        }
        public string DeleteUser(string username)
        {
            string Message = string.Empty;
            //SQLHelper sql = new SQLHelper();
            //sql.OpenConnection();
            //SqlCommand cmd = new SqlCommand("spAuthenticateUser", )
            //string conn = ConfigurationManager.ConnectionStrings["UserTimeConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conn))
            {

                SqlCommand cmd = new SqlCommand("deluser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Username", username);
                con.Open();
                int returnCode = (int)cmd.ExecuteScalar();
                if (returnCode == -1)
                {
                    Message = "Fail";
                }
                else
                {
                    Message = "Success";
                    con.Close();
                }

            }
            return (new JavaScriptSerializer().Serialize(Message));
        }
        public void GetUserData()
        {
            
        }
        public string Login(string username, string password)
        {
            string Message;
            #region
            {
                //SQLHelper sql = new SQLHelper();
                //sql.OpenConnection();
                //SqlCommand cmd = new SqlCommand("spAuthenticateUser", )
               
                using (SqlConnection con = new SqlConnection(conn))
                {
                    SqlCommand cmd = new SqlCommand("LoginAuth", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@email", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    //cmd.Parameters.AddWithValue("pCnfirm", user.ConfirmPassword);
                    //cmd.Parameters.AddWithValue("pFirstName", user.FirstName);
                    //cmd.Parameters.AddWithValue("pLastName", user.LastName);
                    con.Open();
                    int returnCode = (int)cmd.ExecuteScalar();
                    if (returnCode == -1)
                    {
                        Message = "Fail";
                    }
                    else
                    {
                        Message = "Success";
                        con.Close();
                    }
                }
                return (new JavaScriptSerializer().Serialize(Message));
            }
            #endregion
            //Creator c = new MssqlCreator();
            //DBcontext d = c.FactoryMethod();
            //int result = d.Login(username, password);
            //if (result == 1)
            //{
            //    Message = "Success";
            //}
            //else
            //{
            //    Message = "Fail";
            //}
           
        }
        public string Register(string name, string username, string password)
        {
            string Message = string.Empty;
            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("spnrgstr", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);
                //cmd.Parameters.AddWithValue("pFirstName", user.FirstName);
                //cmd.Parameters.AddWithValue("pLastName", user.LastName);
                con.Open();
                int returnCode = (int)cmd.ExecuteScalar();
                if (returnCode == -1)
                {
                    Message = "Fail";
                }
                else
                {
                    Message = "Success";
                    con.Close();
                }
            }
            return (new JavaScriptSerializer().Serialize(Message));
        }
    }
}
