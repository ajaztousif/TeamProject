using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace test_web_method
{
    class Program
    {
        static void Main(string[] args)
        {
            string response = Login("vicky");
        }

        private static string Login(string userName)
        {
            bool serviceResponseStatus = true;

            if (serviceResponseStatus)
            {
                var objUser = new User_module.User_Properties();
                objUser.User_Name = userName.Trim();

                var objuserBL = new User_module.UserBl();

                DataSet userData = objuserBL.CheckUserAuthentication(objUser);

                if (userData.Tables.Count > 0 && userData.Tables[0].Rows.Count > 0)
                {


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
