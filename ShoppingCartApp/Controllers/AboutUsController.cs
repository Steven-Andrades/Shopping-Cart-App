using Microsoft.AspNetCore.Mvc;

namespace ShoppingCartApp.Controllers
{

    public class AboutUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
