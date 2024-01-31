using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Models
{
    public  class PrimarySkill
    {
        [Key]
        public int PrimarySkill_Id { get; set; }

        [Required(ErrorMessage = "Primary skill name is required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long.", MinimumLength = 3)]
        public string? PrimarySkill_Name { get; set; }
    }
}
