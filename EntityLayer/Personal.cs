using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace EntityLayer
{
    public class Personal
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PersonalID { get; set; }

        [Required(ErrorMessage ="boş olmaz")]
        [DisplayName("Adı")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Soyadı")]
        public string Surname { get; set; }


        [DisplayName("Doğum Tarihi")]
        
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateOfBirth { get; set; }


        [DisplayName("Cinsiyet")]
        [StringLength(5,ErrorMessage ="Maksimum 5 Karakter")]
        public string Gender { get; set; }

        [DisplayName("Telefon")]
        public string? PhoneNumber { get; set; }

        [StringLength(100)]
        [EmailAddress]

        [DisplayName("Mail")]
        public string Mail { get; set; }

        [StringLength(100)]
        [DisplayName("Adres")]
        public string Address { get; set; }

        [DisplayName("Detay")]
        public string Scope { get; set; }

        public bool IsActive { get; set; }=true;

        //İlişkili Tablolar
        public ICollection<Operation> Operations { get; set; }


    }
}
