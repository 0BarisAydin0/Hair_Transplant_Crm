using DataAccessLayer.Concrate;
using DataAccessLayer.DTOs;
using EntityLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PresentationLayer.ViewComponents.Operation
{
    public class OpertaionImgAdd : ViewComponent
    {
        private readonly Context _context;
        OperationDetailsViewModel OperationDetailsViewModel = new OperationDetailsViewModel();
        public IViewComponentResult Invoke(int id)
        {

            OperationDetailsViewModel.reminderDatelist=_context.ReminderDates.ToList();
            OperationDetailsViewModel.Operation=_context.Operations.Where(x=>x.OperationID==id).FirstOrDefault();
            
            PatientOperationImg patientOperationImg = new PatientOperationImg();

            return View(Tuple.Create(OperationDetailsViewModel,patientOperationImg));

        }


       
    }
}
