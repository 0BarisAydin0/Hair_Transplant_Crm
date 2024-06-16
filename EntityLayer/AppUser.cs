using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class AppUser : IdentityUser<int>
    {

        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? CompanyName { get; set; }
       
        public int? ConfirmCode { get; set; }

        public bool IsActive { get; set; } = true;
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
    }
}
