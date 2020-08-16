using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using CrisisApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CrisisApplication.Controllers
{
    public class ResponseController : Controller
    {

        private IResponseRepository responseRepository;
        private IContactRepository contactRepository;

        public ResponseController(IResponseRepository responseRepository, IContactRepository contactRepository)
        {
            this.responseRepository = responseRepository;
            this.contactRepository = contactRepository;

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
                responseRepository.SaveResponse(response);
                return View("SavedResponse", response); //nadine 8.11.2020
            }
            return View("ResponseUi", response);
        }

        public ViewResult SavedResponse()
        {
            return View();
        }       
        
        public ActionResult RespondToEvent(int id)
        {
            Response response = new Response();
            Contact contact = contactRepository.Contacts.FirstOrDefault(c => c.ContactID == id);

            if(contact != null)
            {
                response.Email = contact.Email;
            }
            else
            {
                response.Email = "";
            }

            return View("ResponseUI", response);
        }       
    }
}