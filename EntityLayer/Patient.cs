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
    public class Patient
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PatientID { get; set; }

        [Required]
        [DisplayName("Adı")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Soyadı")]
        public string Surname { get; set; }

        [Required]
        [DisplayName("Doğum Tarihi")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [DisplayName("Cinsiyet")]
        [StringLength(5)]
        public string Gender{ get; set; }

        [DisplayName("Boy")]
        public int? Height { get; set; }

        [DisplayName("Kilo")]
        public int? Kilo { get; set; }


        [DisplayName("Tansiyon")]
        public string? BloodPressure { get; set; }

        [DisplayName("Nabız")]
        public int? Pulse { get; set; }


        [Required]
        [DisplayName("Telefon")]
        public string? PhoneNumber { get; set; }

        [EmailAddress]
        [DisplayName("Mail")]
        public string? Mail { get; set; }

        [DisplayName("Ülkesi")]
        public string Country { get; set; }



      
        private string _ChronicProblem { get; set; }

        [DisplayName("Kronik Hast.")]
        public string ChronicProblem 
        {
            get {
                return _ChronicProblem; 
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _ChronicProblem = value;
                }
                else
                {
                    _ChronicProblem = " ";
                }
            } 
        }

        private string _InfectiousDisease { get; set; }

        [DisplayName("Bulaşıcı Hast.")]
        public string InfectiousDisease  
        {
            get
            {
                return _InfectiousDisease;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _InfectiousDisease = value;
                }
                else
                {
                    _InfectiousDisease = " ";
                }
            }
        }

        [DisplayName("Sürekli Kullandığı İlaçlar")]
        public string? Medications { get; set; }

        [DisplayName("Alkol Kullanımı")]
        public bool IsAlcohol  { get; set; }

        [DisplayName("Sigara Kullanımı")]
        public bool IsSmoke  { get; set; }

        [DisplayName("Durumu")]
        public bool IsActive { get; set;} = true;

        [DisplayName("Kayıt Tarihi")]
        public DateTime DateOfRecord { get; set;}= DateTime.Now;

        [DisplayName("Notlar")]
        public string? Note { get; set; }


        //İlişkili Tablolar
       

        public ICollection<Operation> Operations { get; set; }
        public ICollection<Offer> Offers { get; set; }

    }
}
