using Microsoft.AspNetCore.Mvc;

namespace LearningApplicantWeb.Controllers
{
    public class LogoutController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index","Login");
        }
    }
}
