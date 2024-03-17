using Microsoft.AspNetCore.Mvc;

namespace Phonebook.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
