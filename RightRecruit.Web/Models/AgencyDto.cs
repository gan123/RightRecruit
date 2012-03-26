using System;
using System.ComponentModel.DataAnnotations;

namespace RightRecruit.Web.Models
{
    public class AgencyDto
    {
        [Required(ErrorMessage = "Can't leave this blank")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Can't leave this blank")]
        public string AdminId { get; set; }

        [Required(ErrorMessage = "Please provide a company website")]
        [RegularExpression(@"^(http|https)\://[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}(/\S*)?$", ErrorMessage = "Please enter a valid website url")]
        public string Website { get; set; }

        [Required(ErrorMessage = "Please provide a contact number")]
        [RegularExpression(@"^(\(?\+?[0-9]*\)?)?[0-9_\- \(\)]*$", ErrorMessage = "Please enter a valid phone number")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Please provide a email address")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Please provide a valid email address")]
        public string Email { get; set; }
    }

    public class RecruiterLineDto
    {
        public string Role { get; set; }
        public string Product { get; set; }
        public double Cost { get; set;}

        [Required(ErrorMessage = "Please provide a name")]
        public string Name { get; set; }
        public string Email { get; set; }
        public string Id { get; set; }
    }

    public class RoleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class PlanDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class DateDto
    {
        public string Text { get; set; }
        public DateTime EndDate { get; set; }
    }
}