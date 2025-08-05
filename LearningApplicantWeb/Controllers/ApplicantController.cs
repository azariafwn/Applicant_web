using LearningApplicantWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace LearningApplicantWeb.Controllers
{
    
    public class ApplicantController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                string session = HttpContext.Session.GetString("UserId") ?? "";
                if (string.IsNullOrEmpty(session)) return RedirectToAction("Index", "Login");

                var model = new Models.ApplicantVM.Index();
                return View(model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpGet]
        public IActionResult Create()
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
        public IActionResult Create(Models.ApplicantVM.Create input)
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

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var response = new ResponseBase();
            try
            {
                var model = new Models.ApplicantVM.Edit(id);
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
        public IActionResult Edit(Models.ApplicantVM.Edit input)
        {
            var response = new ResponseBase();
            try
            {
                string session = HttpContext.Session.GetString("UserId") ?? "ADMIN";
                Models.ApplicantVM.Method.Update(input, session);

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

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var response = new ResponseBase();
            try
            {
                var model = new Models.ApplicantVM.Delete(id);
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
        public IActionResult Delete(Models.ApplicantVM.Delete input)
        {
            var response = new ResponseBase();
            try
            {
                string session = HttpContext.Session.GetString("UserId") ?? "ADMIN";
                Models.ApplicantVM.Method.Remove(input, session);

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

        [HttpGet]
        public IActionResult SetStatus(int id)
        {
            var response = new ResponseBase();
            try
            {
                var model = new Models.ApplicantVM.SetStatus(id);
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
        public IActionResult SetStatus(Models.ApplicantVM.SetStatus input)
        {
            var response = new ResponseBase();
            try
            {
                string session = HttpContext.Session.GetString("UserId") ?? "ADMIN";
                Models.ApplicantVM.Method.UpdateStatusApprove(input, session);

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
