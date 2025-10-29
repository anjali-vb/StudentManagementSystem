using Microsoft.AspNetCore.Mvc;
using StudentRecordManagementSystem.Models.Interfaces;
using StudentRecordManagementSystem.Models.Entities;
using System.Data;

namespace StudentRecordManagementSystem.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Role") != "Invigilator")
                return RedirectToAction("Login", "Account");

            DataTable dt = _studentService.GetAllStudents();
            return View(dt);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("Role") != "Invigilator")
                return RedirectToAction("Login", "Account");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            if (HttpContext.Session.GetString("Role") != "Invigilator")
                return RedirectToAction("Login", "Account");

            if (!ModelState.IsValid)
            {
                // Collect model state errors and show in view
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                ViewBag.ModelErrors = errors;
                return View(student);
            }

            if (student.RollNumber == 0)
                student.RollNumber = _studentService.GenerateNextRollNumber();

            try
            {
                _studentService.AddStudent(student);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Return error to view so user knows what went wrong
                ViewBag.ErrorMessage = "Unable to create student: " + ex.Message;
                return View(student);
            }
        }

        [HttpGet]
        public IActionResult Edit(int rollNumber)
        {
            if (HttpContext.Session.GetString("Role") != "Invigilator")
                return RedirectToAction("Login", "Account");

            var student = _studentService.GetStudentByRoll(rollNumber);
            if (student == null) return NotFound();
            return View(student);
        }

        [HttpPost]
        public IActionResult Edit(Student student)
        {
            if (HttpContext.Session.GetString("Role") != "Invigilator")
                return RedirectToAction("Login", "Account");

            if (!ModelState.IsValid) return View(student);

            _studentService.UpdateStudent(student);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int rollNumber)
        {
            _studentService.DeleteStudent(rollNumber);
            return RedirectToAction("Index");
        }

    }
}


        