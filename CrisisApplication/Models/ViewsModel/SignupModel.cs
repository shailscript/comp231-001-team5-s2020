using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CrisisApplication.Models.ViewsModels
{
    public class SignupModel
    {

        public int UserID { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter Confirm password")]
        [Compare("Password", ErrorMessage = "Password and confirm password should be matched")]
        public string ConfirmPassword { get; set; }

        public Boolean isAdmin;

        [Required(ErrorMessage = "Please enter Email")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        public string ReturnUrl { get; set; } = "/";
    }
}
