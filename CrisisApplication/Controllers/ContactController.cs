using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using CrisisApplication.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CrisisApplication.Controllers
{
    public class ContactController : Controller
    {
        private IContactRepository repository;

        private readonly IHostingEnvironment _hostingEnvironment;
        

        public ContactController(IContactRepository repository, IHostingEnvironment hostingEnvironment)
        {
            this.repository = repository;
            _hostingEnvironment = hostingEnvironment;
        }

        public ViewResult ContactHome()
        {
            return View();
        }
        public ViewResult Contactresponseconfrm()
        {
            return View();
        }

        public ActionResult ViewContacts()
        {
            return View(repository.Contacts);
        }
        
        public ActionResult FireEvent(Event eventToFire)
        {
            int expectedResponses = 0;
            if(repository.Contacts.Any())
            {
                foreach(var c in repository.Contacts)
                {
                    expectedResponses++;
                    try
                    {
                        var sender = new MailAddress("crisis.sender@gmail.com", "Sender") ;
                        var destEmail = new MailAddress(c.Email, "Reciever");
                        var subject = eventToFire.EventName;
                        var body = eventToFire.EventDescr;
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
                    }
                    catch(Exception e)
                    {
                        ViewBag.Error = e.Message;
                    }                    
                }
            }
            return RedirectToAction("Events", "CrisisManager");
        }
        
        private MailMessage GetEmailTemplate(string subject, int id, MailAddress sender, MailAddress reciever)
        {
            StreamReader r = new StreamReader(Path.Combine(_hostingEnvironment.WebRootPath, "Email/email1.html"));
            string emailBody = r.ReadToEnd();
            string responseURLString = Request.GetEncodedUrl().Replace("Contact/FireEvent", "Response/RespondToEvent/" + id);


            MailMessage mailMessage = new MailMessage(sender, reciever)
            {
                Subject = subject,
                Body = emailBody.Replace("ResponseURL", responseURLString ),
                IsBodyHtml = true
            };

            return mailMessage;
        }
    }
}
