using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrisisApplication.Models
{
    public class EFRespondentsRepository : IRespondentsRepository
    {
        private ApplicationDbContext context;

        public EFRespondentsRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Respondent> Respondent =>
            context.Respondents.AsQueryable();

        public void SaveRespondent(Respondent respondent)
        {
            if (respondent.RespondentID == 0)
            {
                context.Respondents.Add(respondent);
            }
            else
            {
                var dbEntry = context.Respondents
                    .FirstOrDefault(c => c.RespondentID == respondent.RespondentID);

                if (dbEntry != null)
                {
                    respondent.RespondentID = dbEntry.RespondentID;
                    dbEntry = respondent;
                }
            }
            context.SaveChanges();
        }

        public Respondent GetRespondent(int id)
        {
            return context.Respondents
                 .FirstOrDefault(c => c.RespondentID == id);
        }

        public void DeleteContact(int id)
        {
            var dbEntry = GetRespondent(id);

            if (dbEntry != null)
            {
                context.Respondents.Remove(dbEntry);
                context.SaveChanges();
            }


        }
    }
}
