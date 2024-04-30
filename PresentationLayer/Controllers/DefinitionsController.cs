using DataAccessLayer.Concrate;
using DataAccessLayer.DTOs;
using EntityLayer.Definition;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{

    public class DefinitionsController : Controller
    {
        Context context = new Context();

        [HttpGet]
        public IActionResult ChronicProblems()
        {
            var list = context.ChronicProblems.Where(x => x.IsActive == true).ToList();
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
                    context.ChronicProblems.Add(chronicProblem);
                    context.SaveChanges();
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
                var chronicProblem = context.ChronicProblems.FirstOrDefault(cp => cp.ChronicProblemsID == chronicProblemsDTOs.ChronicProblemsID);

                try
                {
                    if (chronicProblem == null)
                    {
                        return NotFound("Güncellenecek kayıt bulunamadı.");
                    }
                    chronicProblem.Title = chronicProblemsDTOs.Title;
                    context.SaveChanges();

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
                var chronicProblemToDelete = context.ChronicProblems.FirstOrDefault(cp => cp.ChronicProblemsID == chronicProblemsDTOs.ChronicProblemsID);

                if (chronicProblemToDelete != null)
                {
                    chronicProblemToDelete.IsActive = false; 
                    context.SaveChanges();
                    TempData["Message"] = "delete";
                    return Ok(new { success = true, message = "Silme işlemi başarılı!" });
                }
                return NotFound("Silinecek kayıt bulunamadı");
            }

        }


      

        [HttpGet]
        public IActionResult InfectiousDisease()
        {
            var list = context.InfectiousDiseases.Where(x=>x.IsActive==true).ToList();
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
                    context.InfectiousDiseases.Add(infectiousDisease);
                    context.SaveChanges();
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
                var infectiousDisease = context.InfectiousDiseases.FirstOrDefault(cp => cp.InfectiousDiseaseID == ınfectiousDiseasesDTOs.InfectiousDiseaseID);

                try
                {
                    if (infectiousDisease == null)
                    {
                        return NotFound("Güncellenecek kayıt bulunamadı.");
                    }
                    infectiousDisease.Title = ınfectiousDiseasesDTOs.Title;
                    context.SaveChanges();

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
               
                var InfectiousDiseasesDelete = context.InfectiousDiseases.FirstOrDefault(x => x.InfectiousDiseaseID == ınfectiousDiseasesDTOs.InfectiousDiseaseID);

                if (InfectiousDiseasesDelete != null)
                {
                    InfectiousDiseasesDelete.IsActive = false;
                    context.SaveChanges();
                    TempData["Message"] = "delete";
                    return Ok(new { success = true, message = "Silme işlemi başarılı!" });
                }
                return BadRequest("Geçersiz veri alındı");
            }

        }


        [HttpGet]
        public IActionResult ReminderDate()
        {
            var list = context.ReminderDates.Where(x => x.IsActive == true).ToList();
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
                    context.ReminderDates.Add(reminderDate);
                    context.SaveChanges();
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
                var reminderDateupdate = context.ReminderDates.FirstOrDefault(cp => cp.ReminderDateID == reminderDatesDTO.ReminderDateID);
                try
                {
                    if (reminderDateupdate == null)
                    {
                        return NotFound("Güncellenecek kayıt bulunamadı.");
                    }
                    reminderDateupdate.RemindDayCount = reminderDatesDTO.RemindDayCount;
                    reminderDateupdate.RemindDayCountName = reminderDatesDTO.RemindDayCountName;
                    context.SaveChanges();

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
                var reminderdateDelete = context.ReminderDates.FirstOrDefault(x => x.ReminderDateID == reminderDatesDTO.ReminderDateID);

                if (reminderdateDelete != null)
                {
                    reminderdateDelete.IsActive = false;
                    context.SaveChanges();
                    TempData["Message"] = "delete";
                    return Ok(new { success = true, message = "Silme işlemi başarılı!" });
                }
                return BadRequest("Geçersiz veri alındı");
            }

        }
    }
}
