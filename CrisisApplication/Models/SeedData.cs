using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrisisApplication.Models
{
    public class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices
            .GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();

            //Base Events
            if (!context.Events.Any())
            {
                context.Events.AddRange(
                new Event
                {
                    EventName = "Event 1",
                    EventDescr = "Event Descr"
                },
                 new Event
                 {
                     EventName = "Event 2",
                     EventDescr = "Event Descr"
                 },
                 new Event
                 {
                     EventName = "Event 3",
                     EventDescr = "Event Descr"
                 }
                );                
            }

            //Base Contacts
            if (!context.Contacts.Any())
            {
                context.Contacts.Add(new Contact { FirstName = "Test FName", LastName = "Test LName", Email = "comp231projecttestacc@gmail.com", StudentID = 1 });
            }

            if (!context.Respondents.Any())
            {
                context.Respondents.Add(new Respondent { FirstName = "Crisis", LastName = "Respondent", Email = "crisis.respondent@gmail.com" });
            }

            context.SaveChanges();
        }
    }
}
