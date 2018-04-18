using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Server.Contacts
{
    [Route("contacts")]
    public class ContactsController : Controller
    {
        private readonly ContactService service;

        public ContactsController(ContactService service)
        {
            this.service = service;
        }

        [HttpPost]
        public ContactResponse Post([FromBody] ContactRequest body)
        {
            var contact = service.Add(body.FirstName, body.Surname);

            return new ContactResponse(contact);
        }

        [HttpGet]
        public IEnumerable<ContactResponse> Get()
        {
            var contacts = service.GetAll();

            return contacts.Select(contact => new ContactResponse(contact));
        }

        [HttpGet("{id}")]
        public ContactResponse Get(int id)
        {
            var contact = service.Get(id);

            return new ContactResponse(contact);
        }

        [HttpPut("{id}")]
        public ContactResponse Put(int id, [FromBody] ContactRequest body)
        {
            var contact = service.Update(id, body.FirstName, body.Surname);

            return new ContactResponse(contact);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            service.Remove(id);
        }
    }
}
