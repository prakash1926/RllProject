using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Models
{
    public  class JobCategory
    {
        [Key]
        public int JobCategoryId { get; set; }
        public string? JobCategoryName { get; set; } 
        public string? Description { get; set; }
    }
}
