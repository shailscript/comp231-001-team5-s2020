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
                context.SaveChanges();
            }
        }
    }
}
