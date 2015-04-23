using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RESTFulServiceClient
{
    [XmlType("Person")]
    public class Person
    {
        [XmlElement("PersonId")]
        public int PersonId { get; set; }
        [XmlElement("PersonFirst")]
        public string PersonFirst { get; set; }
        [XmlElement("PersonLast")]
        public string PersonLast { get; set; }
        [XmlElement("Income")]
        public double Income { get; set; }
    }

}
