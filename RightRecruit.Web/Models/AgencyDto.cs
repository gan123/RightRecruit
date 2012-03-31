using System;

namespace RightRecruit.Web.Models
{
    public class AgencyDto
    {
        public string CompanyName { get; set; }
        public string AdminId { get; set; }
        public string Website { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }

    public class RecruiterLineDto
    {
        public string Role { get; set; }
        public string Product { get; set; }
        public double Cost { get; set;}
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