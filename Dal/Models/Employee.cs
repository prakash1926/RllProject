using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Models
{
    public  class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        public int UserId { get; set; }
        public string EmployeeName { get; set; } = null!;
        public DateTime? Dob { get; set; }
        public string? Education { get; set; }
        public string? WorkExperience { get; set; }
        public string? Skills { get; set; }
        public string EmailAddress { get; set; } = null!;
        public string Gender { get; set; } = null!;
   
        public int PassedOutYear { get; set; }
        public string? PermanentAddress { get; set; }
        public string? JobReference { get; set; }
        public string? Description { get; set; }

        public byte[]? Photo { get; set; } = null!;
        public byte[]? Resume { get; set; } = null!;
        public string? PostJobID { get; set; }
        public string? JobTitle { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
