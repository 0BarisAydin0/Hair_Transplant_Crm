using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Email eksik ya da hatalı")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Lütfen Şifrenizi Girin")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Şifre Eşleşmedi")]
        public string ConfirmPassword { get; set; }


        [Required(ErrorMessage = "Şirket Adı Zorunlu")]

        public string? CompanyName { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
    }
}
