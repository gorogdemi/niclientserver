using System.Collections.Generic;

namespace NIClientServer.Models
{
    interface IPersonRepository
    {
        IEnumerable<Person> GetPeople();
        void AddPerson(Person person);
        void DeletePerson(Person person);
    }
}
