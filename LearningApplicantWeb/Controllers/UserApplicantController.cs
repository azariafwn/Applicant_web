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
                var model = new Models.UserApplicantVM.Index();
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
        public IActionResult Index(Models.UserApplicantVM.Index input)
        {
            var response = new ResponseBase();
            try
            {
                var insert = Models.UserApplicantVM.Method.Insert(input);

                response.Status = true;
                response.Message = "Data Berhasil Ditambahkan";
                response.Data = new { registerCode = insert };
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
