using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CrisisApplication.Controllers
{
    public class ContactController : Controller
    {
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
