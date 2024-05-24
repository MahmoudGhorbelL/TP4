using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using TP3.Models;
using TP3.Models.Repositories;

namespace TP3.Controllers
{
    [Authorize]

    public class StudentController : Controller
    {
        readonly IStudentRepository StudentRepository;
        readonly ISchoolRepository SchoolRepository;

        // GET: StudentController
        public StudentController(IStudentRepository StudentRepository, ISchoolRepository SchoolRepository)
        {
            this.StudentRepository = StudentRepository;
            this.SchoolRepository = SchoolRepository;
        }
        //[AllowAnonymous]

        public ActionResult Index()
        {
            ViewBag.SchoolID = new SelectList(SchoolRepository.GetAll(), "SchoolID", "SchoolName");
            var Students = StudentRepository.GetAll();
            return View(Students);
        }

        // GET: StudentController/Details/5
        public ActionResult Details(int id)
        {
            var student = StudentRepository.GetById(id);
            return View(student);
        }

        // GET: StudentController/Create
        public ActionResult Create()
        {
            ViewBag.SchoolID = new SelectList(SchoolRepository.GetAll(),"SchoolID","SchoolName");
            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student s)
        {
            ViewBag.SchoolID = new SelectList(StudentRepository.GetAll(), "SchoolID", "SchoolName");
            try
            {
                StudentRepository.Add(s);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Edit/5
        public ActionResult Edit(int id)
        {
            var student = StudentRepository.GetById(id);
            ViewBag.SchoolID = new SelectList(SchoolRepository.GetAll(), "SchoolID", "SchoolName", student.SchoolID);
            return View(student);
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Student s)
        {
            ViewBag.SchoolID = new SelectList(SchoolRepository.GetAll(), "SchoolID", "SchoolName");
            try
            {
                StudentRepository.Edit(s);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Delete/5
        public ActionResult Delete(int id)
        {
            var student = StudentRepository.GetById(id);
            return View(student);
        }

        // POST: StudentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var student = StudentRepository.GetById(id);
                StudentRepository.Delete(student);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Search(string name, int? schoolid)
        {
            var result = StudentRepository.GetAll();
            if (!string.IsNullOrEmpty(name))
                result = StudentRepository.FindByName(name);
            else
            if (schoolid != null)
                result = StudentRepository.GetStudentsBySchoolID(schoolid);
            ViewBag.SchoolID = new SelectList(SchoolRepository.GetAll(), "SchoolID", "SchoolName");
            return View("Index", result);
        }

    }
}
