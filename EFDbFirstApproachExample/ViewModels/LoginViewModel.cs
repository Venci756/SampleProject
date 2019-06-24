using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EFDbFirstApproachExample.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Please enter the user name.")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="Please enter the password.")]
        public string Password { get; set; }
    }
}