using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        
        protected void Button1_Click(object sender, EventArgs e)
        {
            string username = TextBox1.Text;
            string password = TextBox2.Text;
            string url = string.Format("http://localhost:56060/OAuthService.svc/Login/{0}/{1}", username, password);
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            request.ContentLength = 0;
            HttpWebResponse GETResponse = (HttpWebResponse)request.GetResponse();
            Stream GETResponseStream = GETResponse.GetResponseStream();
            StreamReader sr = new StreamReader(GETResponseStream);
            string val = sr.ReadToEnd().ToString();
            
            //var result = srv.Login(username, password);
            //if (result == "Fail")
            //{
            //    Label1.Text = "Email Id and Password doesn't match";
            //}
            //else
            //{
            //    Response.Redirect("~/SuccessPage.aspx");
            //}
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string name = Nametxt.Text;
            string username = UserTxt.Text;
            string password = Pwdtxt.Text;
            string url = string.Format("http://localhost:56060/OAuthService.svc/Login/{0}+{1}+{2}", username, password, name);
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            request.ContentLength = 0;
            HttpWebResponse GETResponse = (HttpWebResponse)request.GetResponse();
            Stream GETResponseStream = GETResponse.GetResponseStream();
            StreamReader sr = new StreamReader(GETResponseStream);
            string val = sr.ReadToEnd().ToString();
            //        var result = srv.Register(name, username, password);
            //        if (result == "Fail")
            //        {
            //            Label2.Text = "Email Already exists please try new one";
            //        }
            //        else
            //        {
            //            Response.Redirect("~/SuccessPage.aspx");
            //        }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            //string username = Uname.Text;
            //string password = Pwd.Text;
            //string confirmpassword = CPwdtxt.Text;
            //var result = srv.ChangePassword(username, password, confirmpassword);
            //if (result == "Fail")
            //{
            //    Label3.Text = "Email & Password doesn't match !!!";
            //}
            //else
            //{
            //    Response.Redirect("~/SuccessPage.aspx");
            //}
        }
    }
}