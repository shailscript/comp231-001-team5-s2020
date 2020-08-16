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
        private static ApplicationDbContext context;

        public static void EnsurePopulated(IApplicationBuilder app)
        {
            context = app.ApplicationServices
            .GetRequiredService<ApplicationDbContext>();

            context.Database.Migrate();

            foreach (var i in context.Responses)
            {
                context.Remove(i);
            }

            //Base Events
            if (!context.Events.Any())
            {
                context.Events.AddRange(
                new Event
                {
                    EventName = "Example Event 1",
                    EventDescr = "Example Event Description",
                    RespondentMetaInfo = "Meta Data here"                    
                });                
            }

            //Base Contacts
            if (!context.Contacts.Any())
            {
                context.Contacts.Add(new Contact { FirstName = "Test FName", LastName = "Test LName", Email = "comp231projecttestacc@gmail.com", StudentID = 300000001 });
            }

            if (!context.Respondents.Any())
            {
                context.Respondents.Add(new Respondent { FirstName = "Crisis", LastName = "Respondent", Email = "crisis.respondent@gmail.com" });
            }

            context.SaveChanges();
        }

        public void ClearAll()
        {
            foreach (var i in context.Respondents)
            {
                context.Remove(i);
            }
            
            foreach (var i in context.Contacts)
            {
                context.Remove(i);
            }
            foreach (var i in context.Events)
            {
                context.Remove(i);
            }
        }
    }
}
