using EntityLayer;
using EntityLayer.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class PersonalDTO
    {
        public Patient patientdto { get; set; }
        public Personal personaldto { get; set; }
        public Operation operationdto { get; set; }

        public List<Patient> patientlist { get; set; }
        public List<Personal> personallist { get; set; }
        public List<Operation> operationlist { get; set; }
 
    }
}
