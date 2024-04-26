using DataAccessLayer.Abstract;
using DataAccessLayer.Concrate;
using DataAccessLayer.DTOs;
using DataAccessLayer.EntityFramework;
using EntityLayer;
using EntityLayer.Definition;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.AccessControl;

namespace PresentationLayer.Controllers
{
    public class OperationController : Controller
    {
        Context context = new Context();
        OperationDTO operationDTO = new OperationDTO();
        OperationDetailsViewModel DetailsViewModel = new OperationDetailsViewModel();

        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IOperationDAL _operationDAL;
        private readonly IPatientOperationImgDAL _patientOperationImgDAL;


        public OperationController(IWebHostEnvironment webHostEnvironment, IOperationDAL operationDAL, IPatientOperationImgDAL patientOperationImgDAL)
        {
            _webHostEnvironment = webHostEnvironment;
            _operationDAL = operationDAL;
            _patientOperationImgDAL = patientOperationImgDAL;
        }


        [HttpGet]
        public IActionResult OperationAdd(int id)
        {
            var patientfind = context.Patients.Where(x => x.PatientID == id).FirstOrDefault();


            operationDTO.Techniques = context.Techniques.ToList();
            operationDTO.currencies = context.Currencies.ToList();
            operationDTO.Personals = context.Personals.ToList();
            operationDTO.Patient = patientfind;

            return View(operationDTO);

        }

        [HttpPost]
        public IActionResult OperationAdd(Operation operation)
        {
            _operationDAL.Create(operation);
            return RedirectToAction("PatientDetails", "Patient", new { id = operation.PatientID });
        }


        //   [HttpGet]
        //   public IActionResult OperationDetails(int id)
        //   {
        //       operationDTO.Operation = context.Operations.Where(x => x.OperationID == id).Include(x => x.Patient).FirstOrDefault();

        //       operationDTO.reminders = context.ReminderDates.ToList();


        //       operationDTO.patientOperationImgs = context.patientOperationImgs.Where(x => x.OperationID == id).ToList();

        //       operationDTO.patientOperationImgsGruplar = context.patientOperationImgs.GroupBy(x => x.ReminderDateID)
        //                                     .Select(x => x.ToList())
        //                                     .ToList();



        //       var gruplar = context.patientOperationImgs
        //.Join(context.ReminderDates,
        //      img => img.ReminderDateID,
        //      reminder => reminder.ReminderDateID,
        //      (img, reminder) => new { img, reminder })
        //.Where(x => x.img.OperationID == id) // İşlem kimliğine göre filtreleme
        //.GroupBy(x => x.img.ReminderDateID)
        //.Select(group => new
        //{
        //    ReminderDateID = group.Key,
        //    ReminderDateName = group.FirstOrDefault().reminder != null ? group.FirstOrDefault().reminder.RemindDayCountName : null,
        //    PatientOperationImgs = group.Select(x => x.img).ToList()
        //})
        //.ToList();

        //       //var gruplar = context.patientOperationImgs
        //       //    .GroupBy(x => x.ReminderDateID)
        //       //    .Select(group => new
        //       //    {
        //       //        ReminderDateID = group.Key,
        //       //        PatientOperationImgs = group.ToList(),
        //       //        Reminder = context.ReminderDates.FirstOrDefault(r => r.ReminderDateID == group.Key)
        //       //    })
        //       //    .ToList();



        //       #region The Other Method


        //       //ViewData["Gruplar"] = gruplar;  ---> Burada view data ile model olmadan veri gönderdik.  büyük veri taşımak için model kadar işlevli değil

        //       //var operationDTO = context.patientOperationImgs.GroupBy(x => x.ReminderDateID)
        //       //                   .Select(x => new OperationDTO
        //       //                   {
        //       //                       PatientOperationIDDTO=x.Key,
        //       //                       patientOperationImgs=x.ToList()
        //       //                   })
        //       //                   .ToList();




        //       //var gruplarDTO = veriListesi.GroupBy(x => x.ReminderDateID)
        //       //                .Select(grup => new ReminderGroupDTO
        //       //                {
        //       //                    ReminderDateID = grup.Key,
        //       //                    Veriler = grup.ToList()
        //       //                })
        //       //                .ToList();


        //       //var POImgGroup = context.patientOperationImgs.GroupBy(x => x.ReminderDateID).ToListAsync();

        //       #endregion


        //       return View(operationDTO);
        //   }






        public IActionResult OperationDetails(int id)
        {
            var gruplar = context.patientOperationImgs
                .Join(context.ReminderDates,
                    img => img.ReminderDateID,
                    reminder => reminder.ReminderDateID,
                    (img, reminder) => new { img, reminder })
                .Where(x => x.img.OperationID == id)
                .GroupBy(x => x.img.ReminderDateID)
                .Select(group => new OperationDetailsViewModel
                {
                    ReminderDateID = group.Key,
                    ReminderDateName = group.FirstOrDefault().reminder != null ? group.FirstOrDefault().reminder.RemindDayCountName : null,
                    PatientOperationImgs = group.Select(x => x.img).ToList(),

                })
                .ToList();


            DetailsViewModel.OperationIDVM = id;
            DetailsViewModel.Patient = context.Operations.Include(i => i.Patient)
                                                         .FirstOrDefault(i => i.OperationID == id).Patient;
            DetailsViewModel.reminderDatelist = context.ReminderDates.ToList();
            ViewBag.operationid = id;


            return View(Tuple.Create(gruplar, DetailsViewModel));
        }





        //[HttpPost]
        //public IActionResult OperationDetails(PatientOperationImg PO, IFormFile formFile)
        //{
        //    var existing = context.patientOperationImgs.FirstOrDefault(x => x.OperationID == PO.OperationID && x.ReminderDateID == PO.ReminderDateID);
        //    if (existing != null)
        //    {

        //        if (formFile != null)
        //        {
        //            var extent = Path.GetExtension(formFile.FileName);
        //            var randomName = $"{Guid.NewGuid()}{extent}";
        //            var directory = Path.Combine(_webHostEnvironment.WebRootPath, "folder");
        //            var path = Path.Combine(directory, randomName);

        //            // Eğer klasör yoksa oluştur
        //            if (!Directory.Exists(directory))
        //            {
        //                Directory.CreateDirectory(directory);
        //            }

        //            try
        //            {
        //                using (var stream = new FileStream(path, FileMode.Create))
        //                {
        //                    formFile.CopyTo(stream);
        //                }
        //                // Dosya başarıyla kopyalandıysa buraya ulaşır
        //            }
        //            catch (Exception ex)
        //            {
        //                // Dosya kopyalama sırasında bir hata oluştu
        //                // Hata mesajını loglamak veya uygun bir şekilde işlemek için burada yapılması gerekenleri yapın
        //                // Örneğin: return BadRequest(ex.Message);
        //                return BadRequest(ex.Message);
        //            }

        //            var webPath = Path.Combine("/folder", randomName);
        //            existing.POImg = webPath;
        //        }

        //    }
        //    return RedirectToAction("PatientDetails", "Patient"/*, new { id = 2 }*/);
        //}



        [HttpGet]
        public IActionResult PatientOperationImageAdd(int id)
        {
            //var operationselect=context.Operations.Where(x=>x.OperationID== id);

            DetailsViewModel.Operation = context.Operations.Include(x => x.Patient).Where(x => x.OperationID == id).FirstOrDefault();
            DetailsViewModel.reminderDatelist = context.ReminderDates.ToList();
         


            return View(DetailsViewModel);
        }



    

        [HttpPost]
        public IActionResult PatientOperationImageAdd(PatientOperationImg patientOperationImg, List<IFormFile> formFiles)
        {
            var operationid = patientOperationImg.OperationID;
            var reminderDateid = patientOperationImg.ReminderDateID;
            var existingFiles = context.patientOperationImgs.Where(p => p.OperationID == operationid && p.ReminderDateID == reminderDateid).ToList();
            int totalfiles = existingFiles.Count + formFiles.Count;
            if (formFiles.Count > 5 || totalfiles > 5)
            {
                // 5'ten fazla dosya varsa hata döndür
                TempData["Message"] = "maxfile";
                //return BadRequest("En fazla 5 dosya yükleyebilirsiniz.");
                return RedirectToAction("PatientOperationImageAdd", new { id = operationid });
            }

            foreach (var formFile in formFiles)
            {
                var extent = Path.GetExtension(formFile.FileName);
                var randomName = $"{Guid.NewGuid()}{extent}";
                var directory = Path.Combine(_webHostEnvironment.WebRootPath, "folder");
                var path = Path.Combine(directory, randomName);

                // Klasörü oluştur
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                try
                {
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        formFile.CopyTo(stream);
                    }
                    // Dosya başarıyla kopyalandıysa buraya ulaşır

                    // Dosya yolu oluştur
                    var webPath = Path.Combine("/folder", randomName);
                    
                   
                    // PatientOperationImg nesnesine dosya yolunu ekleyin
                    // ve her bir dosya için ayrı bir PatientOperationImg nesnesi oluşturun
                    var patientOperationImgClone = new PatientOperationImg
                    {
                        POImg = webPath,
                        ReminderDateID = patientOperationImg.ReminderDateID,
                        OperationID = patientOperationImg.OperationID
                    };

                    _patientOperationImgDAL.Create(patientOperationImgClone);
                    

                    // Burada patientOperationImgClone nesnesini veritabanına kaydedebilirsiniz
                    // Örneğin: _dbContext.PatientOperationImgs.Add(patientOperationImgClone);
                    // _dbContext.SaveChanges();

                }
                catch (Exception ex)
                {
                    // Dosya kopyalama sırasında bir hata oluştu
                    // Hata mesajını loglamak veya uygun bir şekilde işlemek için burada yapılması gerekenleri yapın
                    // Örneğin: return BadRequest(ex.Message);
                    return BadRequest(ex.Message);
                }
            }

            return RedirectToAction("OperationDetails",new {id= operationid});
        }

    }
}
