using NIClientServer.Models;
using Shared;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NIClientServer.Controllers
{
    public class SurgeryController : ApiController
    {
        private static IPersonRepository personRepository = new PersonRepository();

        [HttpGet]
        public IEnumerable<Person> Get()
        {
            return personRepository.GetPeople();
        }

        [HttpGet]
        public Person Get(string socSecNum)
        {
            return personRepository.GetPeople().Where(x => x.SocialSecurityNumber == socSecNum).SingleOrDefault();
        }

        [HttpPost]
        public void Post([FromBody] Person person)
        {
            if (person == null || !personRepository.AddPerson(person))
            {
                throw new HttpResponseException(new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = "The person is already exists." });
            }
        }

        [HttpDelete]
        public void Delete([FromBody] Person person)
        {
            if (person != null)
            {
                personRepository.DeletePerson(person);
            }
        }
    }
}
