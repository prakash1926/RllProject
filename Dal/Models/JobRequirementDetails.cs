using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Models
{
    public  class JobRequirementDetails
    {
        [Key]
        public int JobRequirementDetailsId { get; set; }
        public int JobRequirementId { get; set; }
        public string JobRequirementDetailsText { get; set; } = null!;
        public int? PostJobId { get; set; }

        public virtual JobRequirements JobRequirement { get; set; } = null!;
        public virtual PostJob PostJob { get; set; } = null!;
    }
}
