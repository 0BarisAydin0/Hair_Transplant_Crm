using BusinessLayer;
using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrate;
using DataAccessLayer.DTOs;
using EntityLayer;
using EntityLayer.Definition;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;

namespace PresentationLayer.Controllers
{
    public class PatientController : Controller
    {

        Context context = new Context();
        PatientDTO patientDTO = new PatientDTO();
        private IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {

            _patientService = patientService;
        }


        async void PatientViewSelectModel()
        {
            List<InfectiousDisease> ınfectiousDiseases = await _patientService.InfectiousDiseaseSelect();
            ViewData["InfectiousDisease"] = ınfectiousDiseases.Select(c => new SelectListItem
            {
                Text = c.Title,
                Value = c.Title.ToString()
            });

            List<Country> countries = await _patientService.CountrySelect();
            ViewData["countries"] = countries.Select(c => new SelectListItem
            {
                Text = c.CountryName,
                Value = c.CountryName.ToString()
            });


            List<ChronicProblems> chronicProblems = await _patientService.ChronicProblemsSelect();
            ViewData["chronicProblems"] = chronicProblems.Select(c => new SelectListItem
            {
                Text = c.Title,
                Value = c.ChronicProblemsID.ToString()
            });
        }


        public IActionResult Index()
        {

            var patientlist = _patientService.GetAll();


            return View(patientlist);


        }




        [HttpGet]
        public IActionResult PatientAdd()
        {
            patientDTO.chronicProblemslist = context.ChronicProblems.ToList();
            patientDTO.ınfectiousDiseases = context.InfectiousDiseases.ToList();
            patientDTO.patients = context.Patients.ToList();
            patientDTO.countries = context.Countries.ToList();
            return View(patientDTO);
        }


        [HttpPost]
        public IActionResult PatientAdd(Patient patient)
        {

            var validate = new PatientAddValidation();
            var result = validate.Validate(patient);

            string answer = _patientService.CheckCreate(patient);

            int.TryParse(answer, out int resultid);
            var patientId = patient.PatientID;

            if (result.IsValid)
            {
                if (answer == "success")
                {
                    TempData["Message"] = "add";
                    return RedirectToAction("PatientDetails", new { id = patientId });
                }

                else if (resultid > 0)
                {

                    TempData["Message"] = "duplicate";
                    return RedirectToAction("PatientDetails", new { id = resultid });
                }
            }
            else
            {

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }


                TempData["ValidationErrors"] = result.Errors.Select(e => e.ErrorMessage).ToList();

            }
            return RedirectToAction("PatientAdd");





        }


        public IActionResult PatientDetails(int id)
        {



            var operation = context.Operations
                       .Include(patient => patient.Patient)
                       .Include(patient => patient.Personal) 
                        .Where(patient => patient.Patient.PatientID == id)
                        .FirstOrDefault();

            var operationlist = context.Operations.Where(x => x.PatientID == id).ToList();
            ViewBag.OperationCount = operationlist.Count();

            var offerlist = context.Offers.Where(x => x.PatientID == id).ToList();
            ViewBag.OfferCount = offerlist.Count();



            patientDTO.operation = operation;
            patientDTO.patient = _patientService.GetById(id);


            return View(patientDTO);
        }

        [HttpGet]
        public IActionResult PatientUpdate(int id)
        {
            string chronicProblems = context.Patients
                .Where(x => x.PatientID == id)
                .Select(x => x.ChronicProblem)
                .FirstOrDefault();
            string[] chronicProblemlist = chronicProblems.Split(',').Select(p => p.Trim()).ToArray();

            // List<string> chronicProblemlistVM = new List<string>(chronicProblemlist);  eğer sadeceliste halinde dönmek istersek bunu kullanabiliriz.
            patientDTO.chronicProblemlistVM = chronicProblemlist.ToList();


            string InfectiousDisease = context.Patients
                .Where(x => x.PatientID == id)
                .Select(x => x.InfectiousDisease)
                .FirstOrDefault();
            string[] InfectiousDiseaselist = InfectiousDisease.Split(',').Select(p => p.Trim()).ToArray();
            patientDTO.ınfectiousDiseasesVM = InfectiousDiseaselist.ToList();


            patientDTO.patient = context.Patients.Where(x => x.PatientID == id).FirstOrDefault();

            patientDTO.countries = context.Countries.ToList();

            patientDTO.chronicProblemslist = context.ChronicProblems.ToList();
            patientDTO.ınfectiousDiseases = context.InfectiousDiseases.ToList();

            return View(patientDTO);

        }


        [HttpPost]
        public IActionResult PatientUpdate(Patient patient)
        {

            bool answer = _patientService._Update(patient);
            if (answer == true)
            {
                TempData["Message"] = "update";

            }
            else
            {
                TempData["Message"] = "error";

            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult PatientDelete(int id)
        {
            var value = _patientService.GetById(id);

            if (value == null || id == null || id == 0)
            {
                return NotFound();
            }
            else
            {
                bool answer = _patientService._Delete(id);
                if (answer == true)
                {
                    TempData["Message"] = "delete";

                }
                else
                {
                    TempData["Message"] = "error";

                }
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
