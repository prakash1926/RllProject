using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Models
{
    public  class UserType
    {
        [Key]
        public int UserTypeId { get; set; }
        public string? UserTypeText { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
