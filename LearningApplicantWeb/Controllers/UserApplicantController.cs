using LearningApplicantWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace LearningApplicantWeb.Controllers
{
    public class UserApplicantController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var response = new ResponseBase();
            try
            {
                var model = new Models.ApplicantVM.Create();
                return PartialView(model);
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                return Json(response);
            }
        }
        [HttpPost]
        public IActionResult Index(Models.ApplicantVM.Create input)
        {
            var response = new ResponseBase();
            try
            {
                string session = HttpContext.Session.GetString("UserId") ?? "ADMIN";
                Models.ApplicantVM.Method.Insert(input, session);

                response.Status = true;
                response.Message = "Data Berhasil Ditambahkan";
                return Json(response);
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
                return Json(response);
            }
        }

    }
}
