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
    public class Settings
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SettingsID { get; set; }


        [StringLength(30)]
        [DisplayName("Firma Adı")]
        public string CompanyName { get; set; }

        [DisplayName("V.K.N.")]
        public string TaxNumber { get; set; }

        [DisplayName("Vergi Dairesi")]
        public string TaxOffice { get; set; }
        public string LogoPath { get; set; }

        [EmailAddress]
        public string Mail { get; set; }

        [StringLength(100)]
        public string Address { get; set; }
        public int  PhoneNumber { get; set; }
    }
}
