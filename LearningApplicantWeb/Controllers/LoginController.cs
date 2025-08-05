using LearningApplicantWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace LearningApplicantWeb.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            try
            {
                var model = new Models.LoginVM.Index();
                return View(model);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult Index(Models.LoginVM.Index input)
        {
            var response = new ResponseBase();
            try
            {
                Models.LoginVM.Method.Authenticate(input);
                HttpContext.Session.SetString("UserId", input.Username);
                return RedirectToAction("Index","Applicant");
            }
            catch (Exception ex)
            {
                TempData["LoginError"] = ex.Message;
                return View(input);
            }
        }
    }
}
