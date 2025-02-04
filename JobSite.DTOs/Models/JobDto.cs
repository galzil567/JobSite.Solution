using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSite.DTOs.Models
{
    public class JobDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Salary { get; set; }
        public string CompanyName { get; set; }
        public string CompanyDescription { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyPhone { get; set; }
    }
}
