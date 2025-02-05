using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSite.DTOs.Models
{
    public class UpdateJobDto
    {
        public string title { get; set; }
        public string type { get; set; }
        public string description { get; set; }
        public string location { get; set; }
        public string salary { get; set; }
        public string companyName { get; set; }
        public string companyDescription { get; set; }
        public string contactEmail { get; set; }
        public string contactPhone { get; set; }
    }
}
