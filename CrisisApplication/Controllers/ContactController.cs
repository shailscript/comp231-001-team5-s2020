using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrisisApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace CrisisApplication.Controllers
{
    public class ContactController : Controller
    {
        private EFContactRepository repository;

        public ContactController(EFContactRepository repository)
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
    }
}
