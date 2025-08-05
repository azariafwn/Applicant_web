using LearningApplicantWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace LearningApplicantWeb.Controllers
{
    
    public class RoleController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                string session = HttpContext.Session.GetString("UserId") ?? "";
                if (string.IsNullOrEmpty(session)) return RedirectToAction("Index", "Login");

                var model = new Models.RoleVM.Index();
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
                var model = new Models.RoleVM.Create();
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
        public IActionResult Create(Models.RoleVM.Create input)
        {
            var response = new ResponseBase();
            try
            {
                string session = HttpContext.Session.GetString("UserId") ?? "ADMIN";
                Models.RoleVM.Method.Insert(input);

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
                var model = new Models.RoleVM.Edit(id);
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
        public IActionResult Edit(Models.RoleVM.Edit input)
        {
            var response = new ResponseBase();
            try
            {
                string session = HttpContext.Session.GetString("UserId") ?? "ADMIN";
                Models.RoleVM.Method.Update(input);

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
                var model = new Models.RoleVM.Delete(id);
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
        public IActionResult Delete(Models.RoleVM.Delete input)
        {
            var response = new ResponseBase();
            try
            {
                string session = HttpContext.Session.GetString("UserId") ?? "ADMIN";
                Models.RoleVM.Method.Remove(input);

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
