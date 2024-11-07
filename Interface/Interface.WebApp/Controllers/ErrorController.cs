using Microsoft.AspNetCore.Mvc;
using Server.Entities.Model;

public class ErrorController : Controller
{
    public IActionResult Index(ErrorModel errorModel)
    {
        return View(errorModel);
    }
}