using Newtonsoft.Json;
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
            _people = JsonConvert.DeserializeObject<List<Person>>(json);
        }

        public IEnumerable<Person> GetPeople()
        {
            return _people.AsEnumerable();
        }

        public void AddPerson(Person person)
        {
            _people.Add(person);
            this.PersistPeople();
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