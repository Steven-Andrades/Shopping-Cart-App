using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingCartApp.Controllers
{
    public class PurchaseHistoryController : Controller
    {
        // GET: PurchaseHistoryController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PurchaseHistoryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
    }
}
