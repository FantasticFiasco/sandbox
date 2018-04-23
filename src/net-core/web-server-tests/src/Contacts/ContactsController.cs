using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Server.Contacts
{
    [Route("contacts")]
    public class ContactsController : Controller
    {
        private readonly IContactService service;

        public ContactsController(IContactService service)
        {
            this.service = service;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ContactResponse), 201)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody] ContactRequest body)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var contact = service.Add(body.FirstName, body.Surname);

            return Created($"/contacts/{contact.Id}", new ContactResponse(contact));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ContactResponse[]), 200)]
        public IActionResult Get()
        {
            var contacts = service.GetAll();

            return Ok(contacts
                .Select(contact => new ContactResponse(contact))
                .ToArray());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ContactResponse), 200)]
        [ProducesResponseType(404)]
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
        [ProducesResponseType(typeof(ContactResponse), 200)]
        [ProducesResponseType(404)]
        public IActionResult Put(int id, [FromBody] ContactRequest body)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var contact = service.Update(id, body.FirstName, body.Surname);

            if (contact == null)
            {
                return NotFound();
            }

            return Ok(new ContactResponse(contact));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
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
