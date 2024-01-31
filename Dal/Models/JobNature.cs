using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Models
{
    public  class JobNature
    {
        [Key]
        public int JobNatureId { get; set; }
        public string JobNatureName { get; set; } = null!;
    }
}
