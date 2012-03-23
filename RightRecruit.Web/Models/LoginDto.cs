using System.ComponentModel.DataAnnotations;

namespace RightRecruit.Web.Models
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Enter user name")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Enter password")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}