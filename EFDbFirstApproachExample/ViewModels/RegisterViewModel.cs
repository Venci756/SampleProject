using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EFDbFirstApproachExample.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "User Name field cannot be empty!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password field cannot be empty!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password field cannot be empty!")]
        [Compare("Password", ErrorMessage = "Passwords do not match!")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Email field cannot be empty!")]
        [EmailAddress(ErrorMessage = "Invalid email address!")]

        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
       
    }
}