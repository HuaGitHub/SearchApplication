using SearchApplication.Data.Classes;
using System.Collections.Generic;

namespace SearchApplication.Services
{
    public interface IContactService
    {
        IEnumerable<Contact> GetAllContacts();
        IEnumerable<Contact> GetContact(string name);
        void AddContact(Contact contact);
    }

}