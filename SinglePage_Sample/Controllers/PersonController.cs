using Microsoft.AspNetCore.Mvc;

namespace SinglePage_Sample.Controllers
{
    public class PersonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
