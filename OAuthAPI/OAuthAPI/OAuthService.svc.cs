using System;
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
using DALayer;

using System.Web.Script.Serialization;

namespace OAuthAPI
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "OAuthService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select OAuthService.svc or OAuthService.svc.cs at the Solution Explorer and start debugging.
    public class OAuthService : IOAuthService
    {
        private SqlCommand mCmd;
        string conn = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        public string ChangePassword(string username, string password, string confirmpassword)
        {
            string Message;
            Creator c = new MssqlCreator();
            DBcontext d = c.FactoryMethod();
            bool result = d.ChangePassword(username, password, confirmpassword);
            if (result == true)
            {
                Message = "Success";
            }
            else
            {
                Message = "Fail";
            }
            return (new JavaScriptSerializer().Serialize(Message));
        }
        public string DeleteUser(string username)
        {
            string Message;
            Creator c = new MssqlCreator();
            DBcontext d = c.FactoryMethod();
            bool result = d.DeleteUser(username);
            if (result == true)
            {
                Message = "Success";
            }
            else
            {
                Message = "Fail";
            }
            return (new JavaScriptSerializer().Serialize(Message));
        }
        public string InsertTimeData(string UID, string PId, string Eday, string Thours)
        {
            string Message;
            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("spEnterTimesheet", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@userid", UID);
                cmd.Parameters.AddWithValue("@projectid", PId);
                cmd.Parameters.AddWithValue("@entryday", Eday);
                cmd.Parameters.AddWithValue("@totalhours", Thours);
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
            return Message;
        }
        public string Login(string username, string password)
        {
            string Message;
            
            Creator c = new MssqlCreator();
            DBcontext d = c.FactoryMethod();
            int result = d.Login(username, password);
            if (result == 1)
            {
                Message = "Success";
            }
            else
            {
                Message = "Fail";
            }
            return (new JavaScriptSerializer().Serialize(Message));
        }
        public string Register(string name, string username, string password)
        {
            string Message;
            Creator c = new MssqlCreator();
            DBcontext d = c.FactoryMethod();
            int result = d.Registration(name, username, password);
            if (result == 1)
            {
                Message = "Success";
            }
            else
            {
                Message = "Fail";
            }
            return (new JavaScriptSerializer().Serialize(Message));
        }
        public string TRegistration(string fname, string lname, string email, string uname, string pwd, string pno, string dept)
        {
            string Message;
            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("spRegisterUser", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@firstname",fname);
                cmd.Parameters.AddWithValue("@lastname", lname);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@username", uname);
                cmd.Parameters.AddWithValue("@password", pwd);
                cmd.Parameters.AddWithValue("@phno", pno);
                cmd.Parameters.AddWithValue("@deptid", dept);
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
            return Message;
        }
        public string GetUserId(string username)
        {

            UserName u = new UserName();
            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("spReturnUserId", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@username", username);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    u.UserId = Convert.ToInt32(dr["userid"]);
                    u.RoleId = Convert.ToInt32(dr["roleid"]);
                  
                }
            }
            return new JavaScriptSerializer().Serialize(u);
        }

        public string DeleteTUser(string username)
        {
            string Message = string.Empty;
            using (SqlConnection con = new SqlConnection(conn))
            {

                SqlCommand cmd = new SqlCommand("spDeleteUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("username", username);
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
            return Message;
        }

        public string GetTimeSheets(string Id)
        {
            TimeSheets time = new TimeSheets();
            using (SqlConnection con = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("spReturnTimeSheet", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@userid", Id);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    time.UserId = Convert.ToInt32(dr["userid"]);
                    time.ProjectId = Convert.ToInt32(dr["projectid"]);
                    time.EntryDay = dr["entryday"].ToString();
                    time.TotalTime = dr["totaltime"].ToString();
                }
            }
            return JsonConvert.SerializeObject(time);
        }

        public string ChangeTPassword(string uname, string pwd, string npwd)
        {
            string Message;
            using (SqlConnection con = new SqlConnection(conn))
            {

                SqlCommand cmd = new SqlCommand("spChangePassword", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@username", uname);
                cmd.Parameters.AddWithValue("@oldPassword", pwd);
                cmd.Parameters.AddWithValue("@newPassword", npwd);
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
