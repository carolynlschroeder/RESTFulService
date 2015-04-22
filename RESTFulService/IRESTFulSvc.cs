using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace RESTFulService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IRESTFulSvc" in both code and config file together.
    [ServiceContract]
    public interface IRESTFulSvc
    {
        [OperationContract]
        Person CreatePerson(Person createPerson);

        [OperationContract]
        List<Person> GetAllPerson();

        [OperationContract]
        Person GetAPerson(int id);

        [OperationContract]
        Person UpdatePerson(int id, Person updatePerson);

        [OperationContract]
        void DeletePerson(int id);
    }
}
