using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Configuration;

namespace RESTFulServiceClient
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var runProgram = new RunProgram();
            runProgram.RunGetAllPersons();
            runProgram.RunGetAPerson("1");
            runProgram.RunUpdatePerson();
            runProgram.RunCreatePerson();
            runProgram.RunDeletePerson("3");
            Console.Read();
        }
    }

    public class RunProgram
        {
            public void RunGetAllPersons()
            {
                var uri = String.Format(@"{0}/{1}", ConfigurationManager.AppSettings["serviceUri"], "GetAllPersons");
                var req = WebRequest.Create(uri);

                req.Method = "GET";
                var resp = req.GetResponse() as HttpWebResponse;
                if (resp.StatusCode == HttpStatusCode.OK)
                {
                    using (var respStream = resp.GetResponseStream())
                    {

                        var strReader = new StreamReader(respStream, Encoding.UTF8);
                        var reader = new StringReader(XmlHandling.RemoveAllNamespaces(strReader.ReadToEnd()));
                        var xmlSer = new System.Xml.Serialization.XmlSerializer(typeof (List<Person>));
                        var personList = (List<Person>) xmlSer.Deserialize(reader);

                        foreach (var p in personList)
                        {
                            Console.WriteLine(String.Format("{0} {1} {2} {3}", p.PersonId, p.PersonFirst, p.PersonLast,
                                p.Income));
                        }
                    }
                }
                else
                {
                    Console.WriteLine(string.Format("Status Code: {0}, Status Description: {1}", resp.StatusCode,
                        resp.StatusDescription));
                }

            }

            public void RunCreatePerson()
            {
                var uri = String.Format(@"{0}/{1}", ConfigurationManager.AppSettings["serviceUri"], "CreatePerson");
                var method = "POST";
                
                var req = MakePersonRequest("72000","Test","0","Cat",uri, method);
                GetAPersonResponse(req);
            }

            public void RunUpdatePerson()
            {
                var uri = String.Format(@"{0}/{1}", ConfigurationManager.AppSettings["serviceUri"], "UpdatePerson");
                var method = "PUT";

                var req = MakePersonRequest("75000", "Frankie", "2", "Cat", uri, method);
                GetAPersonResponse(req);
            }

            public void RunGetAPerson(string id)
            {
                var uri = String.Format(@"{0}/{1}/{2}", ConfigurationManager.AppSettings["serviceUri"], "GetAPerson",
                    id);
                var req = WebRequest.Create(uri);

                req.Method = "GET";
                GetAPersonResponse(req);
            }

            public void RunDeletePerson(string id)
            {
                var uri = String.Format(@"{0}/{1}/{2}", ConfigurationManager.AppSettings["serviceUri"], "DeletePerson",
                    id);
                var req = WebRequest.Create(uri);

                req.Method = "DELETE";
                var resp = req.GetResponse() as HttpWebResponse;
                if (resp.StatusCode == HttpStatusCode.OK)
                {
                    using (var respStream = resp.GetResponseStream())
                    {
                        var strReader = new StreamReader(respStream, Encoding.UTF8);
                        var xElement = XElement.Parse(strReader.ReadToEnd());
                        var numberOfPersons = xElement.Value;
                        Console.WriteLine("Number of persons is {0}", numberOfPersons);
                    }
                }
                else
                {
                    Console.WriteLine(string.Format("Status Code: {0}, Status Description: {1}", resp.StatusCode,
                        resp.StatusDescription));
                }

            }

            private static HttpWebRequest MakePersonRequest(string income, string personFirst, string personId, string personLast, string uri, string method)
            {
                var sbXml = new StringBuilder();
                sbXml.Append(
                    "<Person xmlns=\"http://schemas.datacontract.org/2004/07/RESTFulService\" xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\">");
                sbXml.AppendFormat(@"<Income>{0}</Income>", income);
                sbXml.AppendFormat(@"<PersonFirst>{0}</PersonFirst>", personFirst);
                sbXml.AppendFormat(@"<PersonId>{0}</PersonId>", personId);
                sbXml.AppendFormat(@"<PersonLast>{0}</PersonLast>", personLast);
                sbXml.Append(@"</Person>");

                string postData = sbXml.ToString();
                byte[] data = Encoding.ASCII.GetBytes(postData);
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri);
                req.Method = method;
                req.ContentType = "application/xml";
                req.ContentLength = data.Length;
                var stream = req.GetRequestStream();
                stream.Write(data, 0, data.Length);
                stream.Close();
                return req;
            }
            
            private static void GetAPersonResponse(WebRequest req)
            {
                var resp = req.GetResponse() as HttpWebResponse;
                if (resp.StatusCode == HttpStatusCode.OK)
                {
                    using (var respStream = resp.GetResponseStream())
                    {
                        var strReader = new StreamReader(respStream, Encoding.UTF8);
                        var reader = new StringReader(XmlHandling.RemoveAllNamespaces(strReader.ReadToEnd()));
                        var xmlSer = new System.Xml.Serialization.XmlSerializer(typeof (Person));
                        var person = (Person) xmlSer.Deserialize(reader);


                        Console.WriteLine(String.Format("{0} {1} {2} {3}", person.PersonId, person.PersonFirst,
                            person.PersonLast, person.Income));
                    }
                }
                else
                {
                    Console.WriteLine(string.Format("Status Code: {0}, Status Description: {1}", resp.StatusCode,
                        resp.StatusDescription));
                }
            }

        }


}
