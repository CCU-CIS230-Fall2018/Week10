using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace Networking
{
    class Program
    {
        static void Main(string[] args)
        {
            string SendResponse(HttpListenerRequest request)
            {
                return string.Format("<HTML><BODY>My web page.<br>{0}</BODY></HTML>", DateTime.Now);
            }

            var ws = new Webserver(SendResponse, "http://localhost:8080/test/");
            ws.Run();

            Console.WriteLine("Webserver running...");
            Console.WriteLine("Please enter number 1:");
            string n1 = Console.ReadLine();
            Console.WriteLine("Please enter number 2:");
            string n2 = Console.ReadLine();
            int num1 = Int32.Parse(n1);
            int num2 = Int32.Parse(n2);
            var sum = num1 + num2;
            Console.WriteLine(sum);

            ws.Stop();
        }  
    }
}
