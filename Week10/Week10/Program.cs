using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;
using System.Web;

namespace Week10
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Gets the port number from the user.
            Console.WriteLine("Enter the desired port number (8080): ");
            string portNumber = Console.ReadLine();

            // Asks the user if they would like to enter numbers to be added.
            Console.WriteLine("Enter numbers to be added? (yes/no) enter no for testing");
            string ans = Console.ReadLine();

            string x = null;
            string y = null;
            bool throughLoop = false;

            if (ans == "yes")
            {
                // Gets the first number from the user.
                Console.WriteLine("Enter the first number: ");
                x = Console.ReadLine();

                // Gets the second number from the user.
                Console.WriteLine("Enter the second number: ");
                y = Console.ReadLine();

                throughLoop = true;
            }
            else if (ans != "yes" && ans != "no")
            {
                Console.WriteLine("Not recognized.. moving on");
            }


            // The Entire UR put together.
            // string testWebPage = string.Format("http://localhost:{0}/?x={1}&y={2}/", portNumber, x, y); .
            string testWebPage = string.Format("http://localhost:{0}/", portNumber);
            var uriBuilder = new UriBuilder(testWebPage);

            if (throughLoop == true)
            {
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                query["x"] = x;
                query["y"] = y;
                uriBuilder.Query = query.ToString();
            }
            testWebPage = uriBuilder.ToString();

            int number1 = Convert.ToInt32(x);
            int number2 = Convert.ToInt32(y);

            int result = number1 + number2;

            Console.WriteLine(testWebPage);
            var webServer = new WebServer(SendResponse, testWebPage);
            webServer.Run();
            Console.WriteLine("My test webserver. Press any key to exit.");
            Console.ReadKey();
            webServer.Stop();
        }

        public static string SendResponse(HttpListenerRequest request)
        {
            return string.Format("<HTML><BODY>Your returned value is: {0} </BODY></HTML>", "hopefully 7");
        }

        
    }
}
