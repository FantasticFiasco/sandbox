namespace Server.Contacts
{
    public interface IContactService
    {
        Contact Add(string firstName, string surname);

        Contact[] GetAll();

        Contact Get(int id);

        Contact Update(int id, string firstName, string surname);

        bool Remove(int id);
    }
}
