using System.ComponentModel.DataAnnotations;

namespace Server.Contacts
{
    public class ContactRequest
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string Surname { get; set; }
    }
}
