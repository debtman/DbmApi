using Microsoft.AspNetCore.Mvc;

namespace DbmApi.Controllers
{
    public class DebtorsListController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
