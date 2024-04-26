using DataAccessLayer.Concrate;
using DataAccessLayer.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PresentationLayer.ViewComponents.Patients
{
    public class PatientOperations : ViewComponent
    {
        Context context = new Context();
        PatientDTO patientDTO= new PatientDTO();
        public IViewComponentResult Invoke(int id)
        {


            var operationlist=context.Operations.Where(x=>x.PatientID==id).ToList();
            patientDTO.patientoperationlist=operationlist;
            

           

            return View(patientDTO);

        }


    }
}
