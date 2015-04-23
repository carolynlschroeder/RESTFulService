using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace RESTFulService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IRESTFulSvc" in both code and config file together.
    [ServiceContract]
    public interface IRESTFulSvc
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "CreatePerson", Method = "POST")]
        Person CreatePerson(Person createPerson);

        [OperationContract]
        [WebGet(UriTemplate = "GetAllPersons")]
        List<Person> GetAllPersons();

        [OperationContract]
        [WebGet(UriTemplate = "GetAPerson/{id}")]
        Person GetAPerson(string id);

        [OperationContract]
        [WebInvoke(UriTemplate = "UpdatePerson/{id}", Method = "PUT")]
        Person UpdatePerson(string id, Person updatePerson);

        [OperationContract]
        [WebInvoke(UriTemplate = "DeletePerson/{id}", Method = "DELETE")]
        int DeletePerson(string id);
    }
}
