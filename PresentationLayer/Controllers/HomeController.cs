using BusinessLayer.Abstract;
using DataAccessLayer.Concrate;
using EntityLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using PresentationLayer.Models;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace PresentationLayer.Controllers
{
    public class HomeController : Controller
    {

        Context context = new Context();


        private IPersonalService _personalservice;

        public HomeController(IPersonalService personalservice)
        {
            _personalservice = personalservice;
        }

        public async Task<IActionResult> Index()
        {
            List<Personal> getlist= await _personalservice.AsyncGetAll();
            

            return View(getlist);
        }

        public IActionResult Details()
        {
            return View();
        }

        
    }
}