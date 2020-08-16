using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrisisApplication.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options) { }
        
        public DbSet<Event> Events { get; set; }

        public DbSet<Response> Responses { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Respondent> Respondents { get; set; }
    }
}
