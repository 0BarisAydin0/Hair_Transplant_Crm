using DataAccessLayer.Concrate;
using DataAccessLayer.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.ViewComponents.Patients
{
    public class PatientOffers:ViewComponent
    {
        private readonly Context _context;

        public PatientOffers(Context context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(int id)
        {


            var offerlist = _context.Offers.Where(x => x.PatientID == id).ToList();
            

            return View(offerlist);

        }

    }
}
