using EntityLayer;
using EntityLayer.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class OperationDetailsViewModel
    {
        public int ReminderDateID { get; set; }
        public string ReminderDateName { get; set; }
        public string PatientName { get; set; }
        public string PAtientSurname { get; set; }
        public int OperationIDVM { get; set; }
        public List<PatientOperationImg> PatientOperationImgs { get; set; }
      
        public PatientOperationImg PatientOperationImg { get; set; }
        public Patient Patient { get; set; }





        public List<ReminderDate> reminderDatelist { get; set; }
        public ReminderDate ReminderDate { get; set; }
        public Operation Operation { get; set; }
    }
}
