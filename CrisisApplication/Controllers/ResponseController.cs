using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrisisApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace CrisisApplication.Controllers
{
    public class ResponseController : Controller
    {

        private IResponseRepository repository;

        public ResponseController(IResponseRepository repo)
        {
            this.repository = repo;
        }

        public ViewResult SubmitResponse(String email)
        {
            ViewBag.email = email;
            return View("ResponseUi");
        }

        [HttpPost]
        public IActionResult SaveResponse(Response response)
        {

            if (ModelState.IsValid)
            {
                repository.SaveResponse(response);
                return View("SavedResponse", response); //nadine 8.11.2020
            }
            return View("ResponseUi");
        }

        public ViewResult SavedResponse()
        {
            return View();
        }
    }
}