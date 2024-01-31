using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Models
{
    public  class PostJob
    {
        [Key]
        public int PostJobId { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public int JobCategoryId { get; set; }
        public string JobTitle { get; set; } = null!;
        public string JobDescription { get; set; } = null!;
        public int MinSalary { get; set; }
        public int MaxSalary { get; set; }
        public string Location { get; set; } = null!;
        public int Vacancy { get; set; }
        public int JobNatureId { get; set; }
        public DateTime PostDate { get; set; }
        public DateTime ApplicationDeadline { get; set; }
        public DateTime LastDate { get; set; }
        public int JobStatusId { get; set; }

        public virtual Company Company { get; set; } = null!;
        public virtual JobCategory JobCategory { get; set; } = null!;
        public virtual JobNature JobNature { get; set; } = null!;
        public virtual JobStatus JobStatus { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<JobRequirementDetails> JobRequirementDetailsList { get; set; }
    }
}
