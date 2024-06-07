using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrate;
using DataAccessLayer.DTOs;
using EntityLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PresentationLayer.Controllers
{
    public class OfferController : Controller
    {

        private readonly Context _context;
        private readonly IOfferDAL _offerDAL;
        private readonly IPatientDAL _patientDAL;

        public OfferController(Context context, IOfferDAL offerDAL, IPatientDAL patientDAL)
        {
            _context = context;
            _offerDAL = offerDAL;
            _patientDAL = patientDAL;
        }


        [HttpGet]
        public IActionResult AddOffer(int id)
        {
            var patientFind = _patientDAL.GetById(id);

            var offerDTO = new OfferDTO
            {
                Techniques = _context.Techniques.ToList(),
                currencies = _context.Currencies.ToList(),
                Patient = patientFind
            };

            return View(offerDTO);
        }


        [HttpPost]
        public IActionResult AddOffer(Offer offer)
        {
            _offerDAL.Create(offer);

            return RedirectToAction("PatientDetails", "Patient" , new { id = offer.PatientID });
        }

        [HttpGet]
        public IActionResult Index() 
        {
            var offer=_context.Offers.Include(x=>x.Patient).Where(x=>x.IsActive==true).ToList();
            var list = _offerDAL.GetAll(x => x.IsActive == true);
            return View(offer);
        }
    }
}
