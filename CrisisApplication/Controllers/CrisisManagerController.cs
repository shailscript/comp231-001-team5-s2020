using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrisisApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace CrisisApplication.Controllers
{
    public class CrisisManagerController : Controller
    {
        public ViewResult CrisisManagerHome()
        {
            return View();
        }

        public ActionResult ViewContacts()
        {
            return RedirectToAction("ViewContacts", "Contact");
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