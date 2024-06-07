using DataAccessLayer.Concrate;
using DataAccessLayer.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PresentationLayer.ViewComponents.Patients
{
    public class PatientOperations : ViewComponent
    {
        private readonly Context _context;

        public PatientOperations(Context context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(int id)
        {
            var operationList = _context.Operations
                .Where(x => x.PatientID == id)
                .ToList();

            var patientDTO = new PatientDTO
            {
                patientoperationlist = operationList
            };
            

            return View(patientDTO);
        }


    }
}
