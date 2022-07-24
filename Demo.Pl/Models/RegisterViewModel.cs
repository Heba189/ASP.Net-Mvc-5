using System.ComponentModel.DataAnnotations;

namespace Demo.Pl.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage="Email is required")]
        [EmailAddress(ErrorMessage ="Invalid Email")]
        public string Email { get; set; }
        [Required(ErrorMessage="Password is required")]
        [MinLength(5, ErrorMessage ="Minimum Password Length is 5")]
        public string Password { get; set; } 
        [Required(ErrorMessage ="Confirm Password is required")]
        [Compare("Password", ErrorMessage ="confirm Password does not match Password")]
        public string ConfirmPassword { get; set; }
        public bool IsAgree { get; set; }

    }
}
