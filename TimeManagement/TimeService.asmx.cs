using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
namespace TimeManagement
{
    /// <summary>
    /// Summary description for TimeService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class TimeService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string CheckUserAuthentication(string userName, string password)
        {
            string response = "Success";
            bool serviceResponseStatus = (response == "Success") ? true : false;

            if (serviceResponseStatus)
            {
                var objUser = new User_module.User_Properties();
                objUser.User_Name = userName.Trim();

                var objuserBL = new User_module.UserBl();

                DataSet userData = objuserBL.CheckUserAuthentication(objUser);

                if (userData.Tables.Count > 0 && userData.Tables[0].Rows.Count > 0)
                {

                    HttpContext.Current.Session["UserId"] = userData.Tables[0].Rows[0]["userid"].ToString();
                    HttpContext.Current.Session["RoleID"] = userData.Tables[0].Rows[0]["RoleID"].ToString();
                    HttpContext.Current.Session["UserName"] = objUser.User_Name;
                    return "200";
                }
                else
                {
                    return "201";
                }


            }

            return "100";
        }
    }
}
