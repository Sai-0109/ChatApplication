using Microsoft.AspNetCore.Mvc;

namespace ChatApplication.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
