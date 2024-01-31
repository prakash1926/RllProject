using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Models
{
    public  class JobRequirements
    {
        [Key]
        public int JobRequirementId { get; set; }
        public string JobRequirementText { get; set; } = null!;

        public virtual ICollection<JobRequirementDetails> JobRequirementDetailsList { get; set; }
    }
}
