using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;
using System.IO;

namespace Week10
{
    public class WebServer
    {
        private HttpListener myListener = new HttpListener();
        private Func<HttpListenerRequest, string> responder;

        public WebServer(ICollection<string> prefixes, Func<HttpListenerRequest, string> method)
        {
            foreach (var pre in prefixes)
            {
                myListener.Prefixes.Add(pre);
            }

            responder = method;
            myListener.Start();
        }

        public WebServer(Func<HttpListenerRequest, string> method, params string[] prefixes)
           : this(prefixes, method)
        {
        }

        public void Run()
        {
            ThreadPool.QueueUserWorkItem(o =>
            {
                Console.WriteLine("Webserver started properly");
                try
                {
                    while (myListener.IsListening)
                    {
                        ThreadPool.QueueUserWorkItem(c =>
                        {
                            var listenerContext = c as HttpListenerContext;
                            try
                            {
                                if (listenerContext == null)
                                {
                                    return;
                                }

                                int x = Convert.ToInt32(listenerContext.Request.QueryString["x"]);
                                int y = Convert.ToInt32(listenerContext.Request.QueryString["y"]);
                                int result = x + y;

                                listenerContext.Response.ContentLength64 = Encoding.UTF8.GetByteCount(result.ToString());

                                var rstr = responder(listenerContext.Request);
                                var buf = Encoding.UTF8.GetBytes(rstr);
                                listenerContext.Response.ContentLength64 = buf.Length;
                                listenerContext.Response.OutputStream.Write(buf, 0, buf.Length);
                            }
                            finally
                            {
                                listenerContext.Response.OutputStream.Close();
                            }
                        }, myListener.GetContext());
                    }
                }
                catch (Exception ex)
                {
                }
            });
        }

        public void Stop()
        {
            myListener.Stop();
            myListener.Close();
        }
    }
}
