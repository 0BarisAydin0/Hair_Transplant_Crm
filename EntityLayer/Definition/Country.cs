using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace EntityLayer.Definition
{
    public class Country
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CountryID { get; set; }

        [Required]
        [DisplayName("Ülke Kodu")]
        public string CountryCode { get; set; }

        [Required]
        [DisplayName("Ülke Adı")]
        public string CountryName { get; set; }
        
    }
}
