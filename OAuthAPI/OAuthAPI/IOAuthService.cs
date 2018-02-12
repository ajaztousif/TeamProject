using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace OAuthAPI
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IOAuthService" in both code and config file together.
    [ServiceContract]
    public interface IOAuthService
    {
        [OperationContract]
        string InsertData(User user);
    }

    [DataContract]
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
