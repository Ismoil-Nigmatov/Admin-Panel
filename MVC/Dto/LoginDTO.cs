using System.ComponentModel.DataAnnotations;

namespace MVC.Dto
{
        public class LoginDTO
        {
            [Required]
            [EmailAddress(ErrorMessage = "Invalid email address.")]
            public string Email { get; set; }
            public string Password { get; set; }
        }
}
