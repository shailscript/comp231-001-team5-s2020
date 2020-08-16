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
    }
}
