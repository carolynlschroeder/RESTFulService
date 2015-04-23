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
    class Program
    {
        static void Main(string[] args)
        {
            var runProgram = new RunProgram();
            //runProgram.RunGetAllPersons();
            runProgram.RunGetAPerson("1");
            Console.Read();
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
                        var reader = new StringReader(RemoveAllNamespaces(strReader.ReadToEnd()));
                        var xmlSer = new System.Xml.Serialization.XmlSerializer(typeof(List<Person>));
                        var personList = (List<Person>)xmlSer.Deserialize(reader);

                        foreach (var p in personList)
                        {
                            Console.WriteLine(String.Format("{0} {1} {2} {3}", p.PersonId, p.PersonFirst, p.PersonLast,
                                p.Income));
                        }
                    }
                }
                else
                {
                    Console.WriteLine(string.Format("Status Code: {0}, Status Description: {1}", resp.StatusCode, resp.StatusDescription));
                }

            }

            public void RunGetAPerson(string id)
            {
                var uri = String.Format(@"{0}/{1}/{2}", ConfigurationManager.AppSettings["serviceUri"], "GetAPerson",
                    id);
                var req = WebRequest.Create(uri);

                req.Method = "GET";
                var resp = req.GetResponse() as HttpWebResponse;
                if (resp.StatusCode == HttpStatusCode.OK)
                {
                    using (var respStream = resp.GetResponseStream())
                    {

                        var strReader = new StreamReader(respStream, Encoding.UTF8);
                        var reader = new StringReader(RemoveAllNamespaces(strReader.ReadToEnd()));
                        var xmlSer = new System.Xml.Serialization.XmlSerializer(typeof(Person));
                        var person = (Person)xmlSer.Deserialize(reader);


                        Console.WriteLine(String.Format("{0} {1} {2} {3}", person.PersonId, person.PersonFirst,
                            person.PersonLast, person.Income));

                    }
                }
                else
                {
                    Console.WriteLine(string.Format("Status Code: {0}, Status Description: {1}", resp.StatusCode, resp.StatusDescription));
                }

            }

        }

        public static string RemoveAllNamespaces(string xmlDocument)
        {
            XElement xmlDocumentWithoutNs = RemoveAllNamespaces(XElement.Parse(xmlDocument));

            return xmlDocumentWithoutNs.ToString();
        }

        //Core recursion function
        private static XElement RemoveAllNamespaces(XElement xmlDocument)
        {
            if (!xmlDocument.HasElements)
            {
                XElement xElement = new XElement(xmlDocument.Name.LocalName);
                xElement.Value = xmlDocument.Value;

                foreach (XAttribute attribute in xmlDocument.Attributes())
                    xElement.Add(attribute);

                return xElement;
            }
            return new XElement(xmlDocument.Name.LocalName, xmlDocument.Elements().Select(el => RemoveAllNamespaces(el)));
        }

    }
}
