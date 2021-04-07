using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace btLinq
{
    class Program
    {
        static void Main(string[] args)
        {

            // connection string  
            string connString = @"server = .\SQLEXPRESS;integrated security = true;database = btLinq";
           

            string chon, stop = "";


            while (stop == "")
            {
                Console.Write("Moi ban con 1 bai bat ky 1-5: ");
                chon = Console.ReadLine();
                switch (chon)
                {
                    case "1":
                        var req1 = new request1();
                        req1.Run(connString);
                        break;
                    case "2":
                        var req2 = new request2();
                        req2.Run(connString);
                        break;
                    case "3":
                        var req3 = new request3();
                        req3.Run(connString);
                        break;
                    case "4":
                        var req4 = new request4();
                        req4.Run(connString);
                        break;
                    case "5":
                        var req5 = new request5();
                        req5.Run(connString);
                        break;
                    default:
                        stop = "stop";
                        break;
                }
            }


            Console.ReadKey();
        }
    }
}
