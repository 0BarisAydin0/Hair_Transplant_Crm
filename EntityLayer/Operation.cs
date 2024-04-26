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
    public class Operation
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OperationID { get; set; }

        [Required]
        [DisplayName("Greft Sayısı")]
        public int GreftCount { get; set; }

        [Required]
        [DisplayName("Kullanılan Teknik")]
        public string Technique { get; set; }

        [Required]
        [DisplayName("Para Birimi")]
        public string Currency { get; set; }

        [Required]
        [DisplayName("Fiyatı")]
        public int Price { get; set; }

        [DisplayName("Notlar")]
        public string? Note { get; set; }

        [DisplayName("Durumu")]
        public bool IsActive { get; set; } = true;

        [DisplayName("Katıt Tarihi")]
        public DateTime DateOfRecord { get; set; } = DateTime.Now;

        [DisplayName("Operasyon Tarihi")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime OperationDate { get; set; }



        //ilişkili tablolar


        public int? PersonalID { get; set; }
        public Personal? Personal { get; set; }


        public int? PatientID { get; set; }
        public Patient? Patient { get; set; }



        public ICollection<PatientOperationImg> patientOperationImgs { get; set; }
    }
}
