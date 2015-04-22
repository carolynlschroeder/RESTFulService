using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using RESTFulService;

namespace RESTFulService
{

    [AspNetCompatibilityRequirements
        (RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class RESTFulSvc : IRESTFulSvc
    {
        private List<Person> persons = new List<Person>()
        {
            new Person() {PersonId = 1, PersonFirst = "Joe", PersonLast = "Cat", Income = 60000},
            new Person() {PersonId = 2, PersonFirst = "Frankie", PersonLast = "Cat", Income = 65000}
        };


        public Person CreatePerson(Person createPerson)
        {
            createPerson.PersonId = persons.Count + 1;
            persons.Add(createPerson);
            return createPerson;

        }

        public List<Person> GetAllPerson()
        {
            return persons.ToList();
        }

        public Person GetAPerson(int id)
        {
            return persons.FirstOrDefault(p => p.PersonId == id);
        }

        public Person UpdatePerson(int id, Person updatePerson)
        {
            var person = GetAPerson(id);
            person.PersonFirst = updatePerson.PersonFirst;
            person.PersonLast = updatePerson.PersonLast;
            person.Income = updatePerson.Income;
            return person;
        }

        public void DeletePerson(int id)
        {
            var person = GetAPerson(id);
            persons.Remove(person);
        }
    }
}
