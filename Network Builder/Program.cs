using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Network_Builder
{
    public class Program
    {
        // My test URL: http://localhost:8001/?x=2&y=5.
        public System.Net.HttpListenerRequest Request { get; }
        static void Main(string[] args)
        {
            Console.WriteLine("Please now load the website.");
            Console.WriteLine("Example URL: http://localhost:8001/?x=2&y=5.");
            Networker();           
        }
        public static void Networker()
        {
            UriBuilder uri = new UriBuilder("http://localhost:8001/");
            using (HttpListener listener = new HttpListener())
            {
                listener.Start();
                listener.Prefixes.Add(uri.ToString());

                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;
                Console.WriteLine("Inputed URL:" + " " + request.Url.ToString());
                string x = request.QueryString["x"];
                string y = request.QueryString["y"];
                if (x == null)
                {
                    Console.WriteLine("No value for x was given");
                    Console.WriteLine("Press enter to quit the program.");
                    Console.ReadLine();
                    return;
                }
                if (y == null)
                {
                    Console.WriteLine("No value for y was given");
                    Console.WriteLine("Press enter to quit the program.");
                    Console.ReadLine();
                    return;
                }

                if (int.TryParse(x, out int firstValue) && int.TryParse(y, out int secondValue))
                {
                    Console.WriteLine("Both values are numbers!");
                }
                else
                {
                    Console.WriteLine("An invalid value was given!");
                    Console.WriteLine("Press enter to quit the program.");
                    Console.ReadLine();
                    return;
                }
                Console.WriteLine("The value for x is: " + x);
                Console.WriteLine("The value for y is: " + y);
                int math = Int32.Parse(x) + Int32.Parse(y);
                Console.WriteLine("When added these numbers equal: " + math);
            }
            Console.ReadLine();

        }
    }
}
