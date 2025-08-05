using LearningApplicantWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace LearningApplicantWeb.Controllers
{
    public class UsersController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                var model = new Models.UserVM.Index();
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
                var model = new Models.UserVM.Create();
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
        public IActionResult Create(Models.UserVM.Create input)
        {
            var response = new ResponseBase();
            try
            {
                string session = HttpContext.Session.GetString("UserId") ?? "ADMIN";
                Models.UserVM.Method.Insert(input);

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
        public IActionResult Edit(string username)
        {
            var response = new ResponseBase();
            try
            {
                var model = new Models.UserVM.Edit(username);
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
        public IActionResult Edit(Models.UserVM.Edit input)
        {
            var response = new ResponseBase();
            try
            {
                string session = HttpContext.Session.GetString("UserId") ?? "ADMIN";
                Models.UserVM.Method.Update(input, session);

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
        public IActionResult Delete(string username)
        {
            var response = new ResponseBase();
            try
            {
                var model = new Models.UserVM.Delete(username);
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
        public IActionResult Delete(Models.UserVM.Delete input)
        {
            var response = new ResponseBase();
            try
            {
                string session = HttpContext.Session.GetString("UserId") ?? "ADMIN";
                Models.UserVM.Method.Remove(input);

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
