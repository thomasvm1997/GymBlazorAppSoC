using Microsoft.AspNetCore.Mvc;

namespace Pri.Ee.Api.Controllers
{
    public class GoalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
