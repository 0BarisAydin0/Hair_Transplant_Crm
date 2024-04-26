using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    public class DefinitionsController : Controller
    {
        [HttpGet]
        public IActionResult ChronicProblems()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChronicProblems(int id)
        {
            return View();
        }
    }
}
