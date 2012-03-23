using System.ComponentModel.DataAnnotations;

namespace RightRecruit.Web.Models
{
    public class LoginDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}