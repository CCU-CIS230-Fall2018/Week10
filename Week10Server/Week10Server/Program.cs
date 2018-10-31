using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Http;

 namespace Week10Server
{
    public class Program
    {
        static void Main(string[] args)
        {
            int portNumber = 9020;
            if (portNumber == 9020)
            {
                StartServer(portNumber);
                Console.WriteLine($"using port {portNumber}, press any key to stop");
            }
            else
            {
                Console.WriteLine("Must input number");
            }
           
            
        }
        public async static void StartServer(int portNum)
        {
            UriBuilder uri = new UriBuilder("http", "localhost", portNum);

            HttpListener listener = new HttpListener();
            listener.Prefixes.Add(uri.ToString());
            listener.Start();

            while (true)
            {
                try
                {
                    var context = await listener.GetContextAsync();
                    await Task.Run(() => MathCalc(context));
                }
                catch (HttpListenerException)
                {
                    break;
                }
                catch (InvalidOperationException)
                {
                    break;
                }
            }
            listener.Stop();
        }
        private static void MathCalc(HttpListenerContext context)
        {
            string x = context.Request.QueryString["x"];
            string y = context.Request.QueryString["y"];

            int numx;
            int numy;

            bool bolx = int.TryParse(x, out numx);
            bool boly = int.TryParse(y, out numy);

            if (!string.IsNullOrWhiteSpace(x) && !string.IsNullOrWhiteSpace(y))
            {
                int result = numx + numy;
                if(result == 0)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    context.Response.OutputStream.Close();
                    return;
                }
                using(var writer = new StreamWriter(context.Response.OutputStream))
                {
                    writer.Write(result);
                    Console.WriteLine(result);
                }
            }
            else
            {
                using(var writer = new StreamWriter(context.Response.OutputStream))
                {
                    writer.Write("Numbers input are not valid");
                }
            }
        }
    }
}
