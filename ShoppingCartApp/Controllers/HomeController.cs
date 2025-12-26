using Microsoft.AspNetCore.Mvc;
using ShoppingCartApp.Models;
using System.Diagnostics;

namespace ShoppingCartApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            string authenticated = HttpContext.Session.GetString("Authenticated") ?? "false";
            if (authenticated.Equals("false"))
            {
                return RedirectToAction("Login","Authentication");
            }

            return View();
        }

        public IActionResult Product()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}