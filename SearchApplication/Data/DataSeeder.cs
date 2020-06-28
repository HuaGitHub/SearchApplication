using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using SearchApplication.Data.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SearchApplication.Data
{
    public class DataSeeder
    {
        private readonly DatabaseContext _context;
        private readonly IHostEnvironment _hosting;

        public DataSeeder(DatabaseContext context, IHostEnvironment hosting)
        {
            _context = context;
            _hosting = hosting;
        }

        public void Seed()
        {
            _context.Database.EnsureCreated();

            //Seed Contacts 
            if (!_context.Contacts.Any())
            {
                var filePath = Path.Combine($"{_hosting.ContentRootPath}/Data/Contacts.json");
                var json = File.ReadAllText(filePath);
                var contacts = JsonConvert.DeserializeObject<IEnumerable<Contact>>(json);
                _context.AddRange(contacts);

                _context.SaveChanges();
            }
        }

    }
}
