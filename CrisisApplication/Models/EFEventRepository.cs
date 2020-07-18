using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrisisApplication.Models
{
    public class EFEventRepository : IEventRepository
    {
        private ApplicationDbContext context;
        public EFEventRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Event> Events =>
            context.Events
            .AsQueryable<Event>();

        public void SaveEvent(Event events) 
        {
            if (events.EventID == 0)
            {
                context.Events.Add(events);
            }
            else
            {
                Event dbEntry = context.Events
                .FirstOrDefault(e => e.EventID == events.EventID);
                if (dbEntry != null)
                {
                    dbEntry.EventName = events.EventName;
                    dbEntry.EventDescr = events.EventDescr;
                 
                }
            }
            context.SaveChanges();
        }

    

        public Event GetEvent(int eventID) //displays event
        {
            return context.Events
                .FirstOrDefault(e => e.EventID == eventID);
        }

        public Event DeleteEvent(int eventID) //will delete the Event
        {
            Event dbEntry = context.Events
            .FirstOrDefault(e => e.EventID == eventID);
            if (dbEntry != null)
            {
                context.Events.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
