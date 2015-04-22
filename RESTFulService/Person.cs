using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RESTFulService
{
    [DataContract]
    public class Person
    {
        [DataMember]
        public int PersonId { get; set; }
        [DataMember]
        public string PersonFirst { get; set; }
        [DataMember]
        public string PersonLast { get; set; }
        [DataMember]
        public double Income { get; set; }
    }
}