using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CrisisApplication.Controllers
{
    public class AccountController : Controller
    {
        public ViewResult SignIn()
        {
            return View();
        }

        public ViewResult SignUp()
        {
            return View();
        }
    }
}