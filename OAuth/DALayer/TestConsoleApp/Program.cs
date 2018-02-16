using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALayer;

namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Creator c = new MssqlCreator();
            DBcontext d = c.FactoryMethod();
            bool result = d.Registration("Ajaz", "ajaz123", "ajaz456");
            if (result)
            {
                Console.WriteLine("success");
            }
            else
            {
                Console.WriteLine("failed");
            }
            Console.WriteLine();
        }
    }
}
