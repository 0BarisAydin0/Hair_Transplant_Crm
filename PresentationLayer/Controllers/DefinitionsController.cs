﻿using DataAccessLayer.Concrate;
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
            var list = context.ChronicProblems.ToList();
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
                var id = chronicProblemsDTOs.ChronicProblemsID;
                var problem = context.ChronicProblems.FirstOrDefault(x => x.ChronicProblemsID == id);

                if (problem != null)
                {
                    context.ChronicProblems.Remove(problem);
                    context.SaveChanges();
                    TempData["Message"] = "delete";
                    return Ok(new { success = true, message = "Silme işlemi başarılı!" });
                }
                return BadRequest("Geçersiz veri alındı");
            }

        }
    }
}