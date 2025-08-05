using Microsoft.AspNetCore.Mvc;

namespace LearningApplicantWeb.Controllers
{
    public class LogoutController : Controller
    {
        public IActionResult Index()
        {
            // Clear the session to log out the user
            HttpContext.Session.Clear();
            return RedirectToAction("Index","Login");
        }
    }
}
