using Microsoft.AspNetCore.Mvc;
using StudentRecordManagementSystem.Models.Interfaces;

namespace StudentRecordManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();                  
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _userService.Authenticate(username, password);
            if (user != null)
            {
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetString("Role", user.Role);

                if (user.StudentRollNumber.HasValue)
                {
                    HttpContext.Session.SetInt32("StudentRollNumber", user.StudentRollNumber.Value);
                }

                if (user.Role == "Invigilator")
                    return RedirectToAction("Index", "Student");
                else
                    return RedirectToAction("Details", "Student", new { rollNumber = user.StudentRollNumber });
            }
            ViewBag.Message = $"Invalid username or password. (Submitted username: '{username}', password: '{password}')";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
