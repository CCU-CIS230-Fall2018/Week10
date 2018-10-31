using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Week10Server;
using System.Web;
using System.Net;
using System.Net.Http;

namespace Week10ServerTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CorrectInput()
        {
            var port = 9039;
            Program.StartServer(port);
            string searchUrl = "http://localhost:" + port + "/?x=2&y=4";
            using(var client = new HttpClient())
            {
                Assert.AreEqual("6", client.GetStringAsync(searchUrl).Result);
            }
        }
        [TestMethod]
        public void WrongInput()
        {
            var port = 5902;
            Program.StartServer(port);
            var webRequest = (HttpWebRequest)WebRequest.Create("http://localhost" + port + "/?x=two&y=four");
            Assert.ThrowsException<WebException>(() => (HttpWebResponse)webRequest.GetResponse());
        }
    }
}
