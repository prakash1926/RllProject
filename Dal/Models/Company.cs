using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Models
{
    public  class Company
    {
        [Key]
        public int CompanyId { get; set; }

        public int UserId { get; set; }
        public string CompanyName { get; set; } = null!;
        public string ContactNo { get; set; } = null!;
        public string PhoneNo { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;

        [NotMapped]
        public IFormFile? Logo {  get; set; }
        public string? LogoPath { get; set; }
        public string? Description { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<PostJob>? PostJobs { get; set; }
    }
}
