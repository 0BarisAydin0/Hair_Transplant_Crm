using DataAccessLayer.Concrate;
using DataAccessLayer.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.ViewComponents.Patients
{
    public class PatientOffers:ViewComponent
    {
        Context context = new Context();
        public IViewComponentResult Invoke(int id)
        {


            var offerlist = context.Offers.Where(x => x.PatientID == id).ToList();
            

            return View(offerlist);

        }

    }
}
