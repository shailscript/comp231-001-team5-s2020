using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrisisApplication.Models
{
    public class EFContactRepository : IContactRepository
    {
        private ApplicationDbContext context;

        public EFContactRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Contact> Contacts =>
            context.Contacts.AsQueryable();

        public void SaveContact(Contact contact)
        {
            if (contact.ContactID == 0)
            {
                context.Contacts.Add(contact);
            }
            else
            {
                var dbEntry = context.Contacts
                    .FirstOrDefault(c => c.ContactID == contact.ContactID);

                if (dbEntry != null)
                {
                    contact.ContactID = dbEntry.ContactID;
                    dbEntry = contact;
                }                
            }
            context.SaveChanges();
        }

        public Contact GetContact(int id)
        {
            return context.Contacts
                 .FirstOrDefault(c => c.StudentID == id);
        }

        public void DeleteContact(int id)
        {
            var dbEntry = GetContact(id);

            if (dbEntry != null)
            {
                context.Contacts.Remove(dbEntry);
                context.SaveChanges();
            }


        }
    }
}
