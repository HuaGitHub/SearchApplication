using Microsoft.EntityFrameworkCore;
using SearchApplication.Data.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchApplication.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options)
        {
        }       
    }
}
