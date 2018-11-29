using NIClientServer.Models;
using Shared;
using System.Collections.Generic;
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

        [HttpPost]
        public void Post([FromBody] Person person)
        {
            if (person == null || !personRepository.AddPerson(person))
            {
                throw new HttpResponseException(new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = "The person is already exists." });
            }
        }

        [HttpDelete]
        public void Delete(string id)
        {
            if (id == null)
            {
                throw new HttpResponseException(new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = "A social security number must be provided." });
            }
            personRepository.DeletePerson(id);
        }
    }
}
