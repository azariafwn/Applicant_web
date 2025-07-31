using Microsoft.AspNetCore.Mvc;

namespace LearningApplicantWeb.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
