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
            //Creator c = new MssqlCreator();
            //DBcontext d = c.FactoryMethod();
            //int result = d.Registration("vicky", "vicky123", "vicky456");
            //if (result == 1)
            //{
            //    Console.WriteLine("success");
            //}
            //else
            //{
            //    Console.WriteLine("failed");
            //}

            //int result = d.Login("ajaz123", "ajaz456");
            //Console.WriteLine(result);
            //if (result == 1)
            //{
            //    Console.WriteLine("Logged in successfully!");
            //}
            //else
            //{
            //    Console.WriteLine("failed");
            //}
            //Console.ReadLine();


            DBcon d = new DBcon();
            int result = d.Login("ajaz123", "ajaz456");
            Console.WriteLine(result);
            if (result == 1)
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
