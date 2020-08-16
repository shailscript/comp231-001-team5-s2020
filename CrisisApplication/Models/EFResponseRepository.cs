using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrisisApplication.Models
{
    public class EFResponseRepository : IResponseRepository
    {
        private ApplicationDbContext context;

        public EFResponseRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Response> Responses => context.Responses
            .AsQueryable<Response>();

        public void SaveResponse(Response response)
        {
            context.Responses.Add(response);
            context.SaveChanges();
        }
    }
}
