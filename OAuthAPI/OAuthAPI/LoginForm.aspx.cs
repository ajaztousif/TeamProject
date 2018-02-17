using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OAuthAPI
{
    public partial class LoginForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        ServiceReference1.OAuthServiceClient srv = new ServiceReference1.OAuthServiceClient();
        protected void Button1_Click(object sender, EventArgs e)
        {
            string username = TextBox1.Text;
            string password = TextBox2.Text;
            var result = srv.Login(username, password);
            if (result == "Fail")
            {
                Label1.Text = "Email Id and Password doesn't match";
            }
            else
            {
                Response.Redirect("~/SuccessPage.aspx");
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
                string name = Nametxt.Text;
                string username = UserTxt.Text;
                string password = Pwdtxt.Text;
                var result = srv.Register(name, username, password);
                if (result == "Fail")
                {
                    Label2.Text = "Email Already exists please try new one";
                }
                else
                {
                    Response.Redirect("~/SuccessPage.aspx");
                }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            string username = Uname.Text;
            string password = Pwd.Text;
            string confirmpassword = CPwdtxt.Text;
            var result = srv.ChangePassword(username, password, confirmpassword);
            if (result == "Fail")
            {
                Label3.Text = "Email & Password doesn't match !!!";
            }
            else
            {
                Response.Redirect("~/SuccessPage.aspx");
            }
        }
    }
}