using System.Linq;

namespace CrisisApplication.Models
{
    public interface IRespondentsRepository
    {
        IQueryable<Respondent> Respondent { get; }

        void DeleteContact(int id);
        Respondent GetRespondent(int id);
        void SaveRespondent(Respondent respondent);
    }
}