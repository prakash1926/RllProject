using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Models
{
    public  class JobStatus
    {
        [Key]
        public int JobStatusId { get; set; }
        public string JobStatusName { get; set; } = null!;
        public string? StatusMessage { get; set; }

        public virtual ICollection<PostJob> PostJobs { get; set; }
    }
}
