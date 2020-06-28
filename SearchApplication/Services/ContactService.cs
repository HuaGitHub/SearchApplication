using Microsoft.Extensions.Logging;
using SearchApplication.Data;
using SearchApplication.Data.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchApplication.Services
{
    public class ContactService : IContactService
    {
        private readonly ILogger<ContactService> _logger;
        private readonly DatabaseContext _context;

        public ContactService(ILogger<ContactService> logger, DatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// Get all contacts from database
        /// </summary>
        /// <returns>IEnumerable<Contact></returns>
        public IEnumerable<Contact> GetAllContacts()
        {
            return _context.Contacts.ToList();
        }

        /// <summary>
        /// Get list of contacts based on the First or Last Names that begin with string 'name'
        /// </summary>
        /// <param name="name">string</param>
        /// <returns>IEnumerable<Contact></returns>
        public IEnumerable<Contact> GetContact(string name)
        {
            return _context.Contacts.Where(x => x.FirstName.StartsWith(name)|| x.LastName.StartsWith(name)).OrderBy(x => x.FirstName).ToList();
        }

        /// <summary>
        /// Add new contact into database
        /// </summary>
        /// <param name="contact">Contact</param>
        public void AddContact(Contact contact)
        {
            _context.Contacts.Add(contact);
            _context.SaveChanges();
        }
    }
}
