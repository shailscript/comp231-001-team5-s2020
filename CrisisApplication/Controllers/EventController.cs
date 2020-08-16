using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrisisApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace CrisisApplication.Controllers
{
    public class EventController : Controller
    {
        private IEventRepository repository;

        public EventController(IEventRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.Events);
        }

        public ViewResult AddEvent()
        {
            return View();
        }

        public ViewResult EventDetails(int eventID)
        {
            return View("EventDetails", repository.GetEvent(eventID));
        }

        public ViewResult Edit(int eventID) =>
            View("EditEvent", repository.Events
            .FirstOrDefault(e => e.EventID == eventID));

        [HttpPost]
        public IActionResult Edit(Event events)
        {
            if (ModelState.IsValid)
            {
                repository.SaveEvent(events);
                TempData["message"] = $"{events.EventName} has been saved";
                return RedirectToAction("Index");
            }
            return View("Index");
        }

        public ViewResult Create() => View("AddEvent", new Event());

        public IActionResult Delete(int eventID)
        {
            Event deletedEvent = repository.DeleteEvent(eventID);
            if (deletedEvent != null)
            {
                TempData["message"] = $"{deletedEvent.EventName} was deleted";
            }
            return RedirectToAction("Index");
        }

        public ActionResult FireEvent(int eventID)
        {

            return RedirectToAction("FireEvent","Contact");
        }
    }
}