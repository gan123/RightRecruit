using System;
using System.Collections.Generic;

namespace RightRecruit.Web.Models
{
    public class SavePlanDto
    {
        public string Plan { get; set; }
        public DateTime EndDate { get; set; }
        public string Product { get; set; }
        public List<RecruiterLineDto> Recruiters { get; set; }
    }
}