using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Week10Networking
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter the desired port number for this web request");
            string userInput = Console.ReadLine();


            if (Regex.IsMatch(userInput, @"^\d{4}$"))
            {
                var port = Convert.ToInt32(userInput);
                MyWebServer.ListenForResponseAsync(port);

                Console.WriteLine($"webServer innitialized on port{port}.");
                Console.WriteLine("Head to the browser to make calculations");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine($"Invalid string. Please enter a digit.");
            }
        }
    }
}
