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
    public class Currency: BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CurrencyID { get; set; }

        [Required]
        [DisplayName("Para Birimi Kodu")]
        public string CurrencyCode { get; set; }

        [Required]

        [DisplayName("Para Birimi")]
        public string Title { get; set; }
    }
}
