using EntityLayer;
using EntityLayer.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class OperationDTO
    {
        public List<Personal> Personals { get; set; }
        public List<Currency> Currencies { get; set; }
        public List<Technique> Techniques { get; set; }
        public List<Patient> patients { get; set; }
        public List<PatientOperationImg> patientOperationImgs { get; set; }
        public List<List<PatientOperationImg>> patientOperationImgsGruplar { get; set; }

        public Personal Personal { get; set; }
        public Patient Patient { get; set; }
        public Operation Operation { get; set; }    
        public PatientOperationImg PatientOperationImg { get; set; }

        public int PatientOperationIDDTO { get; set; }


        public List<ReminderDate> reminders { get; set; }




    }
}
