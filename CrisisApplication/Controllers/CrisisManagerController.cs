using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CrisisApplication.Controllers
{
    public class CrisisManagerController : Controller
    {
        public ViewResult CrisisManagerHome()
        {
            return View();
        }

        public ViewResult ViewContacts()
        {
            return View();
        }
        public ViewResult ViewStatus()
        {
            return View();
        }

        public ActionResult Events()
        {
            return RedirectToAction("Index", "Event");
        }
    }
}