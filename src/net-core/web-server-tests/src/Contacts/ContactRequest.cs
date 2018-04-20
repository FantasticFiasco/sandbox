using System.ComponentModel.DataAnnotations;

namespace Server.Contacts
{
    public class ContactRequest
    {
        public ContactRequest()
        {
        }

        public ContactRequest(string firstName, string surname)
        {
            FirstName = firstName;
            Surname = surname;
        }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string Surname { get; set; }
    }
}
