using Newtonsoft.Json;
using Shared;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace NIClientServer.Models
{
    public class PersonRepository : IPersonRepository
    {
        private static List<Person> _people;

        public PersonRepository()
        {
            var json = File.ReadAllText(HttpContext.Current.Server.MapPath("~/App_Data/Entities.json"));
            _people = JsonConvert.DeserializeObject<List<Person>>(json) ?? new List<Person>();
        }

        public IEnumerable<Person> GetPeople() => _people.AsEnumerable();

        public bool AddPerson(Person person)
        {
            if (_people.Exists(x => string.CompareOrdinal(x.SocialSecurityNumber, person.SocialSecurityNumber) == 0))
            {
                return false;
            }
            _people.Add(person);
            this.PersistPeople();
            return true;
        }

        public void DeletePerson(Person person)
        {
            _people.Remove(person);
            this.PersistPeople();
        }

        private void PersistPeople()
        {
            var json = JsonConvert.SerializeObject(_people);
            File.WriteAllText(HttpContext.Current.Server.MapPath("~/App_Data/Entities.json"), json);
        }
    }
}