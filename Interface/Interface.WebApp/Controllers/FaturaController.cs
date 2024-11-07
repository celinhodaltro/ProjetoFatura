using Microsoft.AspNetCore.Mvc;

namespace Interface.WebApp.Controllers
{
    public class FaturaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
