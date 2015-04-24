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

        public List<Person> GetAllPersons()
        {
            return persons.ToList();
        }

        public Person GetAPerson(string id)
        {
            return persons.FirstOrDefault(p => p.PersonId == Convert.ToInt32(id));
        }

        public Person UpdatePerson(Person updatePerson)
        {
            var person = GetAPerson(updatePerson.PersonId.ToString());
            person.PersonFirst = updatePerson.PersonFirst;
            person.PersonLast = updatePerson.PersonLast;
            person.Income = updatePerson.Income;
            return person;
        }

        public int DeletePerson(string id)
        {
            var person = GetAPerson(id);
            persons.Remove(person);
            return persons.Count;
        }
    }
}
