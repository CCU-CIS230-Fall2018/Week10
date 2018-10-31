using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebClient
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("open of program");
			using (HttpListener listener = new HttpListener())
			{
				UriBuilder myUri = new UriBuilder("http", "localhost", 8081tt);
				listener.Prefixes.Add(myUri.ToString());
				listener.Start();
				Console.WriteLine("Working, before blocks");
				// Note: The GetContext method blocks while waiting for a request.
				HttpListenerContext context = listener.GetContext();
				HttpListenerRequest request = context.Request;
				Console.WriteLine("after blocks");


				string responseString = "Tester";

				// Get a response stream and write the response to it.
				byte[] buffer = Encoding.UTF8.GetBytes(responseString);
				var response = context.Response;
				var output = response.OutputStream;
				output.Write(buffer,0,buffer.Length);
			}

		}
	}
}
