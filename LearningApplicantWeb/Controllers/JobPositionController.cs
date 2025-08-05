using LearningApplicantWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace LearningApplicantWeb.Controllers
{
    
    public class JobPositionController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                string session = HttpContext.Session.GetString("UserId") ?? "";
                if (string.IsNullOrEmpty(session)) return RedirectToAction("Index", "Login");

                var model = new Models.JobPositionVM.Index();
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
                var model = new Models.JobPositionVM.Create();
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
        public IActionResult Create(Models.JobPositionVM.Create input)
        {
            var response = new ResponseBase();
            try
            {
                string session = HttpContext.Session.GetString("UserId") ?? "ADMIN";
                Models.JobPositionVM.Method.Insert(input, session);

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
                var model = new Models.JobPositionVM.Edit(id);
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
        public IActionResult Edit(Models.JobPositionVM.Edit input)
        {
            var response = new ResponseBase();
            try
            {
                string session = HttpContext.Session.GetString("UserId") ?? "ADMIN";
                Models.JobPositionVM.Method.Update(input, session);

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
                var model = new Models.JobPositionVM.Delete(id);
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
        public IActionResult Delete(Models.JobPositionVM.Delete input)
        {
            var response = new ResponseBase();
            try
            {
                string session = HttpContext.Session.GetString("UserId") ?? "ADMIN";
                Models.JobPositionVM.Method.Remove(input, session);

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
