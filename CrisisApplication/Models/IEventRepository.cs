using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrisisApplication.Models
{
    public interface IEventRepository
    {
        IQueryable<Event> Events { get; }
        void SaveEvent(Event events);
        Event GetEvent(int eventID);

        Event DeleteEvent(int eventID);
    }
}
