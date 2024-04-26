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
    public class ReminderDate
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

       
        public int ReminderDateID { get; set; }

        [DisplayName("Hatırlatma Zamanı")]
        public int? RemindDayCount { get; set; }

        [DisplayName("Hatırlatma Zamanı Periyodu")]
        public string? RemindDayCountName { get; set; }

        public bool IsActive { get; set; } = true;


        


        public ICollection<PatientOperationImg> patientOperationImgs { get; set; }
    }
}
