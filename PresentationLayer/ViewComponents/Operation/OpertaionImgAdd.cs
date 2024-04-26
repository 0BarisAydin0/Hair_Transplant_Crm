using DataAccessLayer.Concrate;
using DataAccessLayer.DTOs;
using EntityLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PresentationLayer.ViewComponents.Operation
{
    public class OpertaionImgAdd : ViewComponent
    {
        Context context = new Context();
        OperationDetailsViewModel OperationDetailsViewModel = new OperationDetailsViewModel();
        public IViewComponentResult Invoke(int id)
        {

            OperationDetailsViewModel.reminderDatelist=context.ReminderDates.ToList();
            OperationDetailsViewModel.Operation=context.Operations.Where(x=>x.OperationID==id).FirstOrDefault();
            
            PatientOperationImg patientOperationImg = new PatientOperationImg();

            return View(Tuple.Create(OperationDetailsViewModel,patientOperationImg));

        }


       
    }
}
