using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using CrisisApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace CrisisApplication.Controllers
{
    public class ContactController : Controller
    {
        private IContactRepository repository;

        public ContactController(IContactRepository repository)
        {
            this.repository = repository;
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
            if(repository.Contacts.Any())
            {
                foreach(var c in repository.Contacts)
                {
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
                        using (var mess = new MailMessage(sender, destEmail)
                        {
                            Subject = subject,
                            Body = body
                        })
                        {
                            smtp.Send(mess);
                        }
                        return RedirectToAction("Events", "CrisisManager");

                    }
                    catch(Exception e)
                    {
                        ViewBag.Error = e.Message;
                    }                    
                }
            }
            return RedirectToAction("Events", "CrisisManager");
        }        
    }
}
