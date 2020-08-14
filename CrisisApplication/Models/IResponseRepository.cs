using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrisisApplication.Models
{
   public  interface IResponseRepository
    {
         IQueryable<Response> Responses { get; }

        void SaveResponse(Response response);
    }
}
