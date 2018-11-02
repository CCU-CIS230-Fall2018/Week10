using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebClientAddtion
{
	public class Program
	{
		static void Main(string[] args)
		{
			WebAddition(args);
		}

		public static void WebAddition(string[] args)
		{
			string responseString;
			string portInput;
			short portNumber;


			using (HttpListener listener = new HttpListener())
			{
				// Checking if args is empty
				if (args.Length == 0)
				{
					portNumber = 4200;
					Console.WriteLine("No port number provided, defaulting to 4200.");
				}
				else
				{


					portInput = Convert.ToString(args[0]);

					// Checking if given argument meets 4 digit format.
					if (Regex.IsMatch(portInput, @"^[1-9]{1}\d{3}$"))
					{
						portNumber = Convert.ToInt16(portInput);
					}
					else
					{
						throw new FormatException("Doesn't match 4-digit port format.");
					}

				}

				// Building the URI
				UriBuilder myUri = new UriBuilder("http", "localhost", portNumber);
				listener.Prefixes.Add(myUri.ToString());
				listener.Start();


				// The GetContext method blocks while waiting for a request.
				HttpListenerContext context = listener.GetContext();
				HttpListenerRequest request = context.Request;
				HttpListenerResponse response = context.Response;

				// Making sure values given can be added
				try
				{
					var x = Convert.ToInt32(request.QueryString.GetValues(0)[0]);
					var y = Convert.ToInt32(request.QueryString.GetValues(1)[0]);
					responseString = Convert.ToString(x + y);
				}
				catch (Exception e)
				{
					responseString = "The provided values were invalid";
					Console.WriteLine(responseString);
					throw e;
				}


				// Get a response stream and write the response to it.
				byte[] buffer = Encoding.UTF8.GetBytes(responseString);
				string result = Encoding.UTF8.GetString(buffer);
				var output = response.OutputStream;
				output.Write(buffer, 0, buffer.Length);

				// Cleaning up
				output.Close();
				listener.Stop();
			}
		
		}
	}
}
