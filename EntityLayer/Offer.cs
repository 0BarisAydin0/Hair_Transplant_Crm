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
    public class Offer
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OfferID { get; set; }

        [DisplayName("Teklif Başlangıç Tarihi")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime OfferStartDate { get; set; }

        [DisplayName("Teklif Bitiş Tarihi")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime OfferEndDate { get; set; }
        [DisplayName("Para Birimi")]
        public string Currency { get; set; }

        [DisplayName("Fiyat")]
        public int Price { get; set; }

        [DisplayName("Greft Sayısı")]
        public int GreftCount { get; set; }

        [DisplayName("Kullanılan Teknik")]
        public string Technique { get; set; }

        [DisplayName("Notlar")]
        public string Note { get; set; }

        [DisplayName("Durumu")]
        public bool IsActive { get; set; } = true;

        [DisplayName("Kayıt Tarihi")]
        public DateTime DateOfRecord { get; set; } = DateTime.Now;


        public int? PatientID { get; set; }
        public Patient? Patient { get; set; }

    }
}
