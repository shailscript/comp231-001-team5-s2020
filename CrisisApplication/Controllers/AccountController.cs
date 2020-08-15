using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using CrisisApplication.Models.ViewsModel;
using CrisisApplication.Models.ViewsModels;

namespace CrisisApplication.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;
        public AccountController(UserManager<IdentityUser> userMgr,
        SignInManager<IdentityUser> signInMgr)
        {
            userManager = userMgr;
            signInManager = signInMgr;
        }

        [AllowAnonymous]
        public ViewResult SignIn(string returnUrl)
        {
            return View(new SigninModel
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(SigninModel loginModel)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user =
                await userManager.FindByNameAsync(loginModel.Name);
                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    if ((await signInManager.PasswordSignInAsync(user,
                    loginModel.Password, false, false)).Succeeded)
                    {
                        return Redirect("/CrisisManager/CrisisManagerHome");
                    }
                }
            }
            ModelState.AddModelError("Name", "Invalid name or password");
            return View(loginModel);
        }

        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }

        public ViewResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(SignupModel user)
        {

            IdentityUser appUser = new IdentityUser
            {
                UserName = user.Name,
                Email = user.Email
            };

            var result = await userManager.CreateAsync(appUser, user.Password);

            if (result.Succeeded)
            {
                await signInManager.SignInAsync(appUser, isPersistent: false);
                return Redirect(user?.ReturnUrl ?? "/Event/Index");
            }
            else
            {
                var error = result.Errors.First();
                if (ModelState.IsValid)
                    ModelState.AddModelError("", error.Description);
                return View(user);
            }
        }

        public async Task<RedirectResult> SignOut(string returnUrl = "/Account/SignIn")
        {
            await signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }

    }
}