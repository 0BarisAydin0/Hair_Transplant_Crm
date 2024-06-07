using DataAccessLayer.Concrate;
using DataAccessLayer.DTOs;
using EntityLayer.Definition;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{

    public class DefinitionsController : Controller
    {
        private readonly Context _context;

        public DefinitionsController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult ChronicProblems()
        {
            var list = _context.ChronicProblems.Where(x => x.IsActive == true).ToList();
            return View(list);
        }

        [HttpPost]
        public IActionResult ChronicProblems([FromBody] ChronicProblemsDTOs chronicProblemsDTOs)
        {

            if (chronicProblemsDTOs.Add == true)
            {
                try
                {
                    // Veritabanına ekleme işlemi
                    ChronicProblems chronicProblem = new ChronicProblems
                    {
                        Title = chronicProblemsDTOs.Title
                        // Diğer özellikleri buraya ekle
                    };

                    // Veritabanı işlemi
                    _context.ChronicProblems.Add(chronicProblem);
                    _context.SaveChanges();
                    TempData["Message"] = "add";
                    return Ok(new { success = true, message = "Ekleme işlemi başarılı!" });
                }
                catch (Exception ex)
                {
                    return BadRequest("Ekleme işlemi başarısız! Hata: " + ex.Message);
                }
            }
            if (chronicProblemsDTOs.Update == true)
            {
                var chronicProblem = _context.ChronicProblems.FirstOrDefault(cp => cp.ChronicProblemsID == chronicProblemsDTOs.ChronicProblemsID);

                try
                {
                    if (chronicProblem == null)
                    {
                        return NotFound("Güncellenecek kayıt bulunamadı.");
                    }
                    chronicProblem.Title = chronicProblemsDTOs.Title;
                    _context.SaveChanges();

                    TempData["Message"] = "update";
                    return Ok(new { success = true, message = "Güncelleme işlemi başarılı!" });
                }
                catch (Exception ex)
                {

                    return BadRequest("Güncelleme işlemi başarısız! Hata: " + ex.Message);
                }
            }
            else 
            {
                var chronicProblemToDelete = _context.ChronicProblems.FirstOrDefault(cp => cp.ChronicProblemsID == chronicProblemsDTOs.ChronicProblemsID);

                if (chronicProblemToDelete != null)
                {
                    chronicProblemToDelete.IsActive = false; 
                    _context.SaveChanges();
                    TempData["Message"] = "delete";
                    return Ok(new { success = true, message = "Silme işlemi başarılı!" });
                }
                return NotFound("Silinecek kayıt bulunamadı");
            }

        }


      

        [HttpGet]
        public IActionResult InfectiousDisease()
        {
            var list = _context.InfectiousDiseases.Where(x=>x.IsActive==true).ToList();
            return View(list);
        }

        [HttpPost]
        public IActionResult InfectiousDisease([FromBody] InfectiousDiseasesDTOs ınfectiousDiseasesDTOs)
        {

            if (ınfectiousDiseasesDTOs.Add == true)
            {
                try
                {
                    // Veritabanına ekleme işlemi
                    InfectiousDisease infectiousDisease = new InfectiousDisease
                    {
                        Title = ınfectiousDiseasesDTOs.Title
                        // Diğer özellikleri buraya ekle
                    };

                    // Veritabanı işlemi
                    _context.InfectiousDiseases.Add(infectiousDisease);
                    _context.SaveChanges();
                    TempData["Message"] = "add";
                    return Ok(new { success = true, message = "Ekleme işlemi başarılı!" });
                }
                catch (Exception ex)
                {
                    return BadRequest("Ekleme işlemi başarısız! Hata: " + ex.Message);
                }
            }
            if (ınfectiousDiseasesDTOs.Update == true)
            {
                var infectiousDisease = _context.InfectiousDiseases.FirstOrDefault(cp => cp.InfectiousDiseaseID == ınfectiousDiseasesDTOs.InfectiousDiseaseID);

                try
                {
                    if (infectiousDisease == null)
                    {
                        return NotFound("Güncellenecek kayıt bulunamadı.");
                    }
                    infectiousDisease.Title = ınfectiousDiseasesDTOs.Title;
                    _context.SaveChanges();

                    TempData["Message"] = "update";
                    return Ok(new { success = true, message = "Güncelleme işlemi başarılı!" });
                }
                catch (Exception ex)
                {

                    return BadRequest("Güncelleme işlemi başarısız! Hata: " + ex.Message);
                }
            }
            else
            {
               
                var InfectiousDiseasesDelete = _context.InfectiousDiseases.FirstOrDefault(x => x.InfectiousDiseaseID == ınfectiousDiseasesDTOs.InfectiousDiseaseID);

                if (InfectiousDiseasesDelete != null)
                {
                    InfectiousDiseasesDelete.IsActive = false;
                    _context.SaveChanges();
                    TempData["Message"] = "delete";
                    return Ok(new { success = true, message = "Silme işlemi başarılı!" });
                }
                return BadRequest("Geçersiz veri alındı");
            }

        }


        [HttpGet]
        public IActionResult ReminderDate()
        {
            var list = _context.ReminderDates.Where(x => x.IsActive == true).ToList();
            return View(list);
        }

        [HttpPost]
        public IActionResult ReminderDate([FromBody] ReminderDatesDTO reminderDatesDTO)
        {
            if (reminderDatesDTO.Add == true)
            {
                try
                {
                    ReminderDate reminderDate = new ReminderDate
                    {
                        RemindDayCount = reminderDatesDTO.RemindDayCount,
                        RemindDayCountName= reminderDatesDTO.RemindDayCountName,
                    };
                    // Veritabanı işlemi
                    _context.ReminderDates.Add(reminderDate);
                    _context.SaveChanges();
                    TempData["Message"] = "add";
                    return Ok(new { success = true, message = "Ekleme işlemi başarılı!" });
                }
                catch (Exception ex)
                {
                    return BadRequest("Ekleme işlemi başarısız! Hata: " + ex.Message);
                }
            }
            if (reminderDatesDTO.Update == true)
            {
                var reminderDateupdate = _context.ReminderDates.FirstOrDefault(cp => cp.ReminderDateID == reminderDatesDTO.ReminderDateID);
                try
                {
                    if (reminderDateupdate == null)
                    {
                        return NotFound("Güncellenecek kayıt bulunamadı.");
                    }
                    reminderDateupdate.RemindDayCount = reminderDatesDTO.RemindDayCount;
                    reminderDateupdate.RemindDayCountName = reminderDatesDTO.RemindDayCountName;
                    _context.SaveChanges();

                    TempData["Message"] = "update";
                    return Ok(new { success = true, message = "Güncelleme işlemi başarılı!" });
                }
                catch (Exception ex)
                {

                    return BadRequest("Güncelleme işlemi başarısız! Hata: " + ex.Message);
                }
            }
            else
            {
                var reminderdateDelete = _context.ReminderDates.FirstOrDefault(x => x.ReminderDateID == reminderDatesDTO.ReminderDateID);

                if (reminderdateDelete != null)
                {
                    reminderdateDelete.IsActive = false;
                    _context.SaveChanges();
                    TempData["Message"] = "delete";
                    return Ok(new { success = true, message = "Silme işlemi başarılı!" });
                }
                return BadRequest("Geçersiz veri alındı");
            }

        }


        [HttpGet]
        public IActionResult Scope()
        {
            var list = _context.Scopes.Where(x => x.IsActive == true).ToList();
            return View(list);
        }

        [HttpPost]
        public IActionResult Scope([FromBody] ScopeDTOs scopeDTOs)
        {

            if (scopeDTOs.Add == true)
            {
                try
                {
                    // Veritabanına ekleme işlemi
                    Scope scope = new Scope
                    {
                        Title = scopeDTOs.Title
                        // Diğer özellikleri buraya ekle
                    };

                    // Veritabanı işlemi
                    _context.Scopes.Add(scope);
                    _context.SaveChanges();
                    TempData["Message"] = "add";
                    return Ok(new { success = true, message = "Ekleme işlemi başarılı!" });
                }
                catch (Exception ex)
                {
                    return BadRequest("Ekleme işlemi başarısız! Hata: " + ex.Message);
                }
            }
            if (scopeDTOs.Update == true)
            {
                var scope = _context.Scopes.FirstOrDefault(cp => cp.ScopeID == scopeDTOs.ScopeID);

                try
                {
                    if (scope == null)
                    {
                        return NotFound("Güncellenecek kayıt bulunamadı.");
                    }
                    scope.Title = scopeDTOs.Title;
                    _context.SaveChanges();

                    TempData["Message"] = "update";
                    return Ok(new { success = true, message = "Güncelleme işlemi başarılı!" });
                }
                catch (Exception ex)
                {

                    return BadRequest("Güncelleme işlemi başarısız! Hata: " + ex.Message);
                }
            }
            else
            {

                var scopeDelete = _context.Scopes.FirstOrDefault(x => x.ScopeID == scopeDTOs.ScopeID);

                if (scopeDelete != null)
                {
                    scopeDelete.IsActive = false;
                    _context.SaveChanges();
                    TempData["Message"] = "delete";
                    return Ok(new { success = true, message = "Silme işlemi başarılı!" });
                }
                return BadRequest("Geçersiz veri alındı");
            }

        }


        [HttpGet]
        public IActionResult Technique()
        {
            var list = _context.Techniques.Where(x => x.IsActive == true).ToList();
            return View(list);
        }

        [HttpPost]
        public IActionResult Technique([FromBody] TechniqueDTOs techniqueDTOs)
        {

            if (techniqueDTOs.Add == true)
            {
                try
                {
                  
                    Technique technique = new Technique
                    {
                        Title = techniqueDTOs.Title                      
                    };

                    // Veritabanı işlemi
                    _context.Techniques.Add(technique);
                    _context.SaveChanges();
                    TempData["Message"] = "add";
                    return Ok(new { success = true, message = "Ekleme işlemi başarılı!" });
                }
                catch (Exception ex)
                {
                    return BadRequest("Ekleme işlemi başarısız! Hata: " + ex.Message);
                }
            }
            if (techniqueDTOs.Update == true)
            {
                var technique = _context.Techniques.FirstOrDefault(cp => cp.TechniqueID == techniqueDTOs.TechniqueID);

                try
                {
                    if (technique == null)
                    {
                        return NotFound("Güncellenecek kayıt bulunamadı.");
                    }
                    technique.Title = techniqueDTOs.Title;
                    _context.SaveChanges();

                    TempData["Message"] = "update";
                    return Ok(new { success = true, message = "Güncelleme işlemi başarılı!" });
                }
                catch (Exception ex)
                {

                    return BadRequest("Güncelleme işlemi başarısız! Hata: " + ex.Message);
                }
            }
            else
            {

                var techniqueDelete = _context.Techniques.FirstOrDefault(x => x.TechniqueID == techniqueDTOs.TechniqueID);

                if (techniqueDelete != null)
                {
                    techniqueDelete.IsActive = false;
                    _context.SaveChanges();
                    TempData["Message"] = "delete";
                    return Ok(new { success = true, message = "Silme işlemi başarılı!" });
                }
                return BadRequest("Geçersiz veri alındı");
            }

        }
    }
}
