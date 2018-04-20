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
        public IActionResult Post([FromBody] ContactRequest body)
        {
            var contact = service.Add(body.FirstName, body.Surname);

            return Created($"/contacts/{contact.Id}", new ContactResponse(contact));
        }

        [HttpGet]
        public IEnumerable<ContactResponse> Get()
        {
            var contacts = service.GetAll();

            return contacts.Select(contact => new ContactResponse(contact));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var contact = service.Get(id);
            if (contact == null)
            {
                return NotFound();
            }

            return Ok(new ContactResponse(contact));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ContactRequest body)
        {
            var contact = service.Update(id, body.FirstName, body.Surname);

            if (contact == null)
            {
                return NotFound();
            }

            return Ok(new ContactResponse(contact));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool success = service.Remove(id);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
