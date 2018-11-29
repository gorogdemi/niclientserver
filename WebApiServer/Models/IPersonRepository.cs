using Shared;
using System.Collections.Generic;

namespace NIClientServer.Models
{
    interface IPersonRepository
    {
        IEnumerable<Person> GetPeople();
        bool AddPerson(Person person);
        void DeletePerson(string socSecNum);
    }
}
