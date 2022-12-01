using Microsoft.AspNetCore.Mvc;

namespace TARge21Shop.Controllers
{
    public class SpaceshipsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
