using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

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

                    var strReader = new StreamReader(respStream, Encoding.UTF8);
                    var reader = new StringReader(RemoveAllNamespaces(strReader.ReadToEnd()));
                     System.Xml.Serialization.XmlSerializer xmlSer = new System.Xml.Serialization.XmlSerializer(typeof(List<Person>));
                        var personList = (List<Person>)xmlSer.Deserialize(reader);

                    foreach (var p in personList)
                    {
                        Console.WriteLine(String.Format("{0} {1}", p.PersonFirst, p.PersonLast));
                    }
                }
            }
            else
            {
                Console.WriteLine(string.Format("Status Code: {0}, Status Description: {1}", resp.StatusCode, resp.StatusDescription));
            }
            Console.Read();
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
