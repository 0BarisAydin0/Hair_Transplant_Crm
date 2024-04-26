using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrate;
using DataAccessLayer.DTOs;
using EntityLayer;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    public class OfferController : Controller
    {

        Context context = new Context();
        OfferDTO offerDTO= new OfferDTO();
        private IOfferDAL _offerDAL;
        private IPatientDAL _patientDAL;

        public OfferController(IOfferDAL offerDAL, IPatientDAL patientDAL)
        {
            _offerDAL = offerDAL;
            _patientDAL = patientDAL;
        }



        [HttpGet]
        public IActionResult AddOffer(int id)
        {
            var patientfind=_patientDAL.GetById(id);
            

            offerDTO.Techniques=context.Techniques.ToList();
            offerDTO.currencies=context.Currencies.ToList();
            offerDTO.Patient = patientfind;

            return View(offerDTO);
        }


        [HttpPost]
        public IActionResult AddOffer(Offer offer)
        {
            _offerDAL.Create(offer);

            return RedirectToAction("PatientDetails", "Patient" , new { id = offer.PatientID });
        }
    }
}
