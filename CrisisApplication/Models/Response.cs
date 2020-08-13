using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CrisisApplication.Models
{
    public class Response
    {
        public int ResponseID { get; set; }

        [Required(ErrorMessage = "Please enter email")]
        public String Email { get; set; }

        [Required(ErrorMessage = "Please select Status")]
        public String Status { get; set; }

    }
}
