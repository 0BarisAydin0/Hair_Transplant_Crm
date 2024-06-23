using BusinessLayer.Abstract;
using BusinessLayer.Concrate;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrate;
using DataAccessLayer.DTOs;
using DataAccessLayer.EntityFramework;
using EntityLayer;
using EntityLayer.Definition;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace PresentationLayer.Controllers
{
    public class PersonalController : Controller
    {
        private IPersonalService _personalservice;       

        private readonly Context _context;
        public PersonalController(IPersonalService personalservice, Context context)
        {
            _personalservice = personalservice;
            _context = context;

        }

      

        async void ScopeList()
        {
            List<Scope> scopes = await _personalservice.ScopeSelect();
            ViewData["scopelist"] = scopes.Select(c => new SelectListItem { Text = c.Title, Value = c.Title.ToString() });
        }

        public IActionResult Index()
        {

            var personalist = _personalservice.GetAll();
            


            return View(personalist);

           
        }

        [HttpGet]
        public async Task<IActionResult> PersonalAdd()
        {

            List<Scope> scopes = await _personalservice.ScopeSelect();
            ViewData["scopelist"] = scopes.Select(c => new SelectListItem { Text = c.Title, Value = c.Title.ToString() });
            return View();
        }



        [HttpPost]
        public IActionResult PersonalAdd(Personal personal)
        {
            var validate = new PersonalAddValidation();
            var result = validate.Validate(personal);


            string answer = _personalservice.CheckCreate(personal);
            int.TryParse(answer, out int resultid);
            var personalid = personal.PersonalID;
            if (result.IsValid)
            {
                if (answer == "success")
                {
                    TempData["Message"] = "add";
                    return RedirectToAction("PersonalDetails", new { id = personalid });

                }
                else
                {
                    TempData["Message"] = "duplicate";
                    return RedirectToAction("PersonalDetails", new { id = resultid });
                }
            }
            else
            {

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }


                TempData["ValidationErrors"] = result.Errors.Select(e => e.ErrorMessage).ToList();


                //TempData["Message"] = /*"HATA!! Marka Eklenemedi"*/ "hata";


            }

            return RedirectToAction("PersonalAdd");
        }


        [ActionName("Delete")]
        public IActionResult PersonalDelete(int id)
        {
            var value = _personalservice.GetById(id);

            if (value == null || id == null || id == 0)
            {
                return NotFound();
            }
            else
            {
                bool answer = _personalservice._Delete(id);
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

        public async Task<IActionResult> PersonalUpdate(int id)
        {
            List<Scope> scopes = await _personalservice.ScopeSelect();
            ViewData["scopelist"] = scopes.Select(c => new SelectListItem { Text = c.Title, Value = c.Title.ToString() });
            var person = await _context.Personals.FirstOrDefaultAsync(x => x.PersonalID == id);
            return View(person);
        }


        [HttpPost, ActionName("Update")]
        public IActionResult PersonalUpdate(Personal personal)
        {
            bool answer = _personalservice._Update(personal);
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


        public IActionResult PersonalDetails(int id)
        {

           

            var operation = _context.Operations
    .Include(patient => patient.Patient)
    .Include(patient => patient.Personal) // Operations yerine Operation olmalı, doğru ilişki adını kullanmalısınız
    .Where(patient => patient.Personal.PersonalID == id)
    .ToList();
           

            PersonalDTO personal = new PersonalDTO
            {
                personaldto= _personalservice.GetById(id),
                operationlist=operation.ToList()
            };

            return View(personal);
        }

    }
}






