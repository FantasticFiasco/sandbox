using System.ComponentModel.DataAnnotations;

namespace Server.Contacts
{
    public class ContactResponse
    {
        public ContactResponse()
        {
        }

        public ContactResponse(Contact contact)
        {
            Id = contact.Id;
            FirstName = contact.FirstName;
            Surname = contact.Surname;
        }

        [Required]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string Surname { get; set; }
    }
}
