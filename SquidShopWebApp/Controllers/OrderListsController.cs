using Microsoft.AspNetCore.Mvc;

namespace SquidShopWebApp.Controllers
{
    public class OrderListsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
