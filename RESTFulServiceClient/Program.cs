using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RESTFulServiceClient
{
    class Program
    {
        static void Main(string[] args)
        {
            WebRequest req = WebRequest.Create(@"http://localhost:52285/RESTFulSvc.svc/GetAllPersons");

            req.Method = "GET";

            HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
            if (resp.StatusCode == HttpStatusCode.OK)
            {
                using (Stream respStream = resp.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(respStream, Encoding.UTF8);
                    Console.WriteLine(reader.ReadToEnd());
                }
            }
            else
            {
                Console.WriteLine(string.Format("Status Code: {0}, Status Description: {1}", resp.StatusCode, resp.StatusDescription));
            }
            Console.Read();
        }
    }
}
