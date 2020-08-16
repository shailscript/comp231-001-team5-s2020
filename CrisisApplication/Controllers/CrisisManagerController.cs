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
        private IResponseRepository responseRepository;
public CrisisManagerController(IResponseRepository responseRepository)
        {
            this.responseRepository = responseRepository;
        }

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

        public ViewResult ViewEventStatus(int expectedResponses)
        {
            ViewBag.Expected = expectedResponses;
            ViewBag.Safe = responseRepository.Responses.Where(r => r.Status.Contains("Safe")).Count();
            ViewBag.Unsafe = responseRepository.Responses.Where(r => r.Status.Contains("Not Safe")).Count();
            return View("ViewStatus");
        }

        public ActionResult Events()
        {
            return RedirectToAction("Index", "Event");
        }
    }
}