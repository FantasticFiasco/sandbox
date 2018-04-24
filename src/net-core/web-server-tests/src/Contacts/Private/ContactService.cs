using System.Collections.Generic;
using System.Linq;

namespace Server.Contacts.Private
{
    public class ContactService : IContactService
    {
        private readonly IDictionary<int, Contact> contacts;

        public ContactService()
        {
            contacts = new Dictionary<int, Contact>();
        }

        public Contact Add(string firstName, string surname)
        {
            var contact = new Contact(NextId(), firstName, surname);

            contacts.Add(contact.Id, contact);

            return contact;
        }

        public Contact[] GetAll()
        {
            return contacts.Values.ToArray();
        }

        public Contact Get(int id)
        {
            contacts.TryGetValue(id, out var contact);
            return contact;
        }

        public Contact Update(int id, string firstName, string surname)
        {
            if (!contacts.ContainsKey(id))
            {
                return null;
            }

            contacts[id] = new Contact(id, firstName, surname);

            return contacts[id];
        }

        public bool Remove(int id)
        {
            return contacts.Remove(id);
        }

        private int NextId()
        {
            if (!contacts.Any())
            {
                return 1;
            }

            return contacts.Values.Max(contact => contact.Id) + 1;
        }
    }
}
