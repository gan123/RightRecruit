using System;
using System.Collections.Generic;
using System.Linq;
using RightRecruit.Web.Models;

namespace RightRecruit.Web.ViewModels
{
    public class AdminRecruitersViewModel
    {
        private string _plan;

        public AdminRecruitersViewModel()
        {
            Recruiters = new List<RecruiterLineDto>
                             {
                                 new RecruiterLineDto {Name = "Test"}
                             };
        }

        public string Plan { get; set; }
        public DateTime EndDate { get; set; }

        public DateDto TrialEndDate
        {
            get { return new DateDto {Text = DateTime.Now.AddDays(30).Date.ToString("dd-MMM-yyyy"), EndDate = DateTime.Now.AddDays(30).Date}; }
        }

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
                yield return new PlanDto {Id = 1, Name = "30 day trial"};
                yield return new PlanDto { Id = 2, Name = "Monthly" };
                yield return new PlanDto { Id = 3, Name = "Annual" };
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