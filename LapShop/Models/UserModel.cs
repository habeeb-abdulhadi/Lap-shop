using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace LapShop.Models
{
    public class UserModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "FirstName must be less than 100")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "LastName must be less than 100")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(100, ErrorMessage = "Email must be less than 100")]
        [Remote(action:"VerifyEmail", controller:"Users", ErrorMessage ="email found")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter valid Password")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*?[#?!@$%^&*-]).{8,15}$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string Password { get; set; }
        [ValidateNever]
        public string ReturnUrl { get; set; } = "";

      

    }
}
