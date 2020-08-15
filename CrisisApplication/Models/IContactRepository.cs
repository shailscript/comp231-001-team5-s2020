using System.Linq;

namespace CrisisApplication.Models
{
    public interface IContactRepository
    {
        IQueryable<Contact> Contacts { get; }

        void DeleteContact(int id);
        Contact GetContact(int id);
        void SaveContact(Contact contact);
    }
}