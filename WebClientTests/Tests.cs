using System;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace WebClientTests
{
	[TestClass]
	public class Tests
	{
		[TestMethod]
		public void AdditionTest()
		{
			int portNumber = 8080;
			int x = new Random().Next(1, 100);
			int y = new Random().Next(1, 100);
			string Uri = $"http://localhost:{portNumber}/?x={x}&y={y}";
			// WebClientAddtion.Program.WebAddition(new string[] { $"{portNumber}" });

			using (var web = new HttpClient())
			{
				int result = Convert.ToInt32(web.GetStringAsync(Uri).Result);

				Assert.AreEqual((x + y), result);
			}



		}
	}
}
