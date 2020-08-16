using CrisisApplication.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace CrisisApplication.Controllers
{
    public class EventController : Controller
    {
        private IEventRepository eventRepo;
        private IContactRepository contactRepo;
        private IRespondentsRepository respondentsRepo;
        private readonly IHostingEnvironment hostingEnvironment;

        public EventController(IEventRepository eventRepo, IContactRepository contactRepo, IRespondentsRepository respondentsRepo, IHostingEnvironment hostingEnvironment)
        {
            this.eventRepo = eventRepo;
            this.contactRepo = contactRepo;
            this.respondentsRepo = respondentsRepo;
            this.hostingEnvironment = hostingEnvironment;
        }

        public ViewResult Index()
        {
            return View(eventRepo.Events);
        }

        public ViewResult AddEvent()
        {
            return View();
        }

        public ViewResult EventDetails(int eventID)
        {
            return View("EventDetails", eventRepo.GetEvent(eventID));
        }

        public ViewResult Edit(int eventID) =>
            View("EditEvent", eventRepo.Events
            .FirstOrDefault(e => e.EventID == eventID));

        [HttpPost]
        public IActionResult Edit(Event events)
        {
            if (ModelState.IsValid)
            {
                eventRepo.SaveEvent(events);
                TempData["message"] = $"{events.EventName} has been saved";
                return RedirectToAction("Index");
            }
            return View("Index");
        }

        public ViewResult Create() => View("AddEvent", new Event());

        public IActionResult Delete(int eventID)
        {
            Event deletedEvent = eventRepo.DeleteEvent(eventID);
            if (deletedEvent != null)
            {
                TempData["message"] = $"{deletedEvent.EventName} was deleted";
            }
            return RedirectToAction("Index");
        }

        public ActionResult FireEvent(int eventID)
        {
            var targetEvent = eventRepo.Events.FirstOrDefault(e => e.EventID == eventID);
            SendContactEmail(targetEvent);
            SendRespondentEmail(targetEvent);
            
            return RedirectToAction("Events", "CrisisManager");
        }

        private int SendContactEmail(Event targetEvent)
        {
            int expectedResponses = 0;
            if (contactRepo.Contacts.Any())
            {
                foreach (var c in contactRepo.Contacts)
                {                    
                    try
                    {
                        var sender = new MailAddress("crisis.sender@gmail.com", "Sender");
                        var destEmail = new MailAddress(c.Email, "Reciever");
                        var subject = targetEvent.EventName;
                        var smtp = new SmtpClient
                        {
                            Host = "smtp.gmail.com",
                            Port = 587,
                            EnableSsl = true,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            UseDefaultCredentials = false,
                            Credentials = new NetworkCredential(sender.Address, "Secret123$")
                        };
                        using (var mess = GetEmailTemplate(subject, c.ContactID, sender, destEmail))
                        {
                            smtp.Send(mess);
                        }
                        expectedResponses++;
                    }
                    catch (Exception e)
                    {
                        ViewBag.Error = e.Message;
                    }
                }
            }
            return expectedResponses;
        }
        
        //For Contacts
        private MailMessage GetEmailTemplate(string subject, int id, MailAddress sender, MailAddress reciever)
        {
            string baseUri = new Uri(Request.GetDisplayUrl()).GetLeftPart(UriPartial.Authority);

            StreamReader r = new StreamReader(Path.Combine(hostingEnvironment.WebRootPath, "Email/email1.html"));
            string emailBody = r.ReadToEnd();
            string responseURLString = baseUri + "/Response/RespondToEvent/" + id; //Request.GetDisplayUrl().Replace("Event/FireEvent", "Response/RespondToEvent/" + id);


            MailMessage mailMessage = new MailMessage(sender, reciever)
            {
                Subject = subject,
                Body = emailBody.Replace("ResponseURL", responseURLString),
                IsBodyHtml = true
            };

            return mailMessage;            
        }

        public void SendRespondentEmail(Event targetEvent)
        {
            if (respondentsRepo.Respondent.Any())
            {
                foreach (var r in respondentsRepo.Respondent)
                {
                    try
                    {
                        var sender = new MailAddress("crisis.sender@gmail.com", "Sender");
                        var destEmail = new MailAddress(r.Email, "Reciever");
                        var subject = targetEvent.EventName;
                        var smtp = new SmtpClient
                        {
                            Host = "smtp.gmail.com",
                            Port = 587,
                            EnableSsl = true,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            UseDefaultCredentials = false,
                            Credentials = new NetworkCredential(sender.Address, "Secret123$")
                        };
                        using (var mess = GetEmailTemplate(targetEvent, sender, destEmail))
                        {
                            smtp.Send(mess);
                        }
                    }
                    catch (Exception e)
                    {
                        ViewBag.Error = e.Message;
                    }
                }
            }
        }

        //For Respondents
        private MailMessage GetEmailTemplate(Event targetEvent, MailAddress sender, MailAddress reciever)
        {
            MailMessage mailMessage = new MailMessage(sender, reciever)
            {
                Subject = targetEvent.EventName,
                Body = targetEvent.RespondentMetaInfo,
                IsBodyHtml = false
            };

            return mailMessage;
        }
    }
}