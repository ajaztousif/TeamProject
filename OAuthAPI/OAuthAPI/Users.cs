using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace OAuthAPI
{

    [DataContract]
    public class Users
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string Phno { get; set; }
        [DataMember]
        public int DeptId { get; set; }
        [DataMember]
        public int RId { get; set; }
    }

    [DataContract]
    public class UserName
    {
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public int RoleId { get; set; }
    }

    [DataContract]
    public class TimeSheets
    {
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public int ProjectId { get; set; }
        [DataMember]
        public string TotalTime { get; set; }
        [DataMember]
        public string EntryDay { get; set; }
    }
}