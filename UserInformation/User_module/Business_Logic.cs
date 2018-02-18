using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace User_module
{
    public class User_Properties
    {
        public string User_Name { set; get; }
        public string Name { set; get; }
        public string Last_Name { set; get; }
        public int Age { set; get; }
        public int Deparment_ID { set; get; }
        int Role { set; get; }
        public string Email { set; get; }
    }

    public class UserBl
    {
        public DataSet CheckUserAuthentication(User_Properties ob)
        {
            Creator c = new MssqlCreator();
            DBcontextTime d = c.FactoryMethod();
            DataSet result = d.Login(ob.User_Name);
            return result;
        }
        public  void Registeration()

        {

        }



    }

    
  
    

    


}
