using EntityLayer;
using EntityLayer.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class PatientDTO
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }


        public List<Patient>? patients { get; set; }
        public List<Operation>? patientoperationlist { get; set; }

        public Operation operation { get; set; }
        public Patient patient { get; set; }
        public string patientoperation { get; set; }
        public List<Personal>? personal { get; set; }   
        public List<Country> countries { get; set; }
        public string country { get; set; }
        public List<Offer> offers { get; set; }

        public List<ChronicProblems> chronicProblemslist { get; set; }
        public ChronicProblems chronicProblem { get; set; }

        public List<InfectiousDisease> ınfectiousDiseases { get; set; }
        public InfectiousDisease ınfectiousDisease { get; set; }

        public List<string> chronicProblemlistVM { get; set; }
        public List<ChronicProblems> chronicProblemlistVMDTO { get; set; }
        public List<string> ınfectiousDiseasesVM { get; set; }
        public List<InfectiousDisease> ınfectiousDiseasesVMDTO { get; set; }

        public List<Operation> operationlist { get; set; }
        public string SelectedChronicProblem { get; set; }
    }





    
}
