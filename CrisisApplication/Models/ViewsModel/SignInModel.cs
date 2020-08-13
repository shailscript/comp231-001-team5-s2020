using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CrisisApplication.Models.ViewsModel
{
    public class SigninModel
    {
       [Required(ErrorMessage = "Please enter name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; } = "/";
    }
}
