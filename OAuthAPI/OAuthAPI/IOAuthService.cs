using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel.Web;
using System.ServiceModel;
using System.Text;

namespace OAuthAPI
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IOAuthService" in both code and config file together.
    [ServiceContract]
    public interface IOAuthService
    {
        [OperationContract]
        string Login(string email, string password);

        [OperationContract]
        string Register(string name, string username, string password);

        [OperationContract]
        string ChangePassword(string username, string password, string confirmpassword);

        [OperationContract]
        string DeleteUser(string username);

        //Get Users data to Admin
        [OperationContract]
        [WebInvoke(Method ="GET", UriTemplate ="/GetUsersData", 
             ResponseFormat =WebMessageFormat.Json)]
        void GetUserData();
    }

    [DataContract]
    public class User
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Password { get; set; }
    }
}
