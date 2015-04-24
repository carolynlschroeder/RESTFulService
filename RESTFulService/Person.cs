using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace RESTFulService
{
    [DataContract]
    [XmlType("Person")]
    public class Person
    {
        [DataMember]
        [XmlElement("PersonId")]
        public int PersonId { get; set; }
        [DataMember]
        [XmlElement("PersonFirst")]
        public string PersonFirst { get; set; }
        [DataMember]
        [XmlElement("PersonLast")]
        public string PersonLast { get; set; }
        [DataMember]
        [XmlElement("Income")]
        public double Income { get; set; }
    }
}