using System;
using System.Collections.Generic;
using System.Linq;
using RightRecruit.Web.Models;

namespace RightRecruit.Web.ViewModels
{
    public class AdminRecruitersViewModel
    {
        public string Plan { get; set; }
        public DateTime EndDate { get; set; }
        
        public IEnumerable<DateDto> MonthlyEndDates
        {
            get { return Enumerable.Range(1, 20).Select(date => new DateDto { Text = DateTime.Now.AddMonths(date).Date.ToString("dd-MMM-yyyy"), EndDate = DateTime.Now.AddMonths(date).Date }); }
        }

        public IEnumerable<DateDto> AnnualEndDates
        {
            get { return Enumerable.Range(1, 20).Select(date => new DateDto { Text = DateTime.Now.AddYears(date).Date.ToString("dd-MMM-yyyy"), EndDate = DateTime.Now.AddYears(date).Date }); }
        }

        public IEnumerable<RoleDto> Roles
        {
            get
            {
                yield return new RoleDto {Id = 1, Name = "Recruiter"};
                yield return new RoleDto { Id = 2, Name = "Team Lead" };
                yield return new RoleDto { Id = 3, Name = "Manager" };
                yield return new RoleDto { Id = 4, Name = "Contract" };
            }
        }

        public IEnumerable<PlanDto> Plans
        {
            get
            {
                yield return new PlanDto {Value = "Trial", Name = "30 day trial"};
                yield return new PlanDto { Value = "Monthly", Name = "Monthly" };
                yield return new PlanDto { Value = "Annual", Name = "Annual" };
            }
        }

        public IEnumerable<ProductDto> Products
        {
            get
            {
                yield return new ProductDto {Id = 1, Name = "Basic"};
                yield return new ProductDto { Id = 2, Name = "Intermediate" };
                yield return new ProductDto { Id = 3, Name = "Pro" };
            }
        }
        public List<RecruiterLineDto> Recruiters { get; set; }
    }
}