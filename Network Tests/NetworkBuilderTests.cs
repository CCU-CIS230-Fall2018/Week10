using System;
using System.Net;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Network_Builder;

namespace Network_Tests
{
    [TestClass]
    public class NetworkBuilderTests
    {
        [TestMethod]
        public void NetworkerTest()
        {
            var thread = new Thread(() =>
            {
                Program.Networker();
            });
            thread.Start();
        }
    }
}
