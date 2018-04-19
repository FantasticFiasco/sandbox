namespace Server.Contacts
{
    public class Contact
    {
        public Contact(int id, string firstName, string surname)
        {
            Id = id;
            FirstName = firstName;
            Surname = surname;
        }

        public int Id { get; }

        public string FirstName { get; }

        public string Surname { get; }
    }
}
