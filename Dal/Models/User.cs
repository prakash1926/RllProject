using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Models
{
    public  class User
    {
        [Key]
        public int UserId { get; set; }
        public int UserTypeId { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
        public string ContactNo { get; set; } = null!;

        public virtual UserType UserType { get; set; } = null!;
        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<PostJob> PostJobs { get; set; }
    }
}
