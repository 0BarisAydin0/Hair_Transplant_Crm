using EntityLayer.Definition;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class PatientOperationImg
    {
        [Key]
        public int PatientImgID { get; set; }
        public string? POImg { get; set; }

        public DateTime Dateofrecord { get; set; }=DateTime.Now;
        public bool IsActive { get; set; } = true;


        public int ReminderDateID { get; set; }
        public ReminderDate reminderDate { get; set; }

        public int OperationID { get; set; }
        public Operation operation { get; set; }

    }
}
