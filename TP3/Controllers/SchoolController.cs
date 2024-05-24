using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TP3.Models.Repositories;
using TP3.Models;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.AspNetCore.Authorization;

namespace TP3.Controllers
{
    
    [Authorize]

    public class SchoolController : Controller
    {
        readonly ISchoolRepository SchoolRepository;
        private readonly StudentContext context;

        // GET: SchoolController
        public SchoolController(ISchoolRepository ProdRepository, StudentContext dbContext)
        {
            SchoolRepository = ProdRepository;
            context = dbContext;
        }
        //[AllowAnonymous]
        public ActionResult Index()
        {
            var Schools = SchoolRepository.GetAll();
            return View(Schools);
        }

        // GET: SchoolController/Details/5
        public ActionResult Details(int id)
        {
            var school = SchoolRepository.GetById(id);
            return View(school);
        }

        // GET: SchoolController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SchoolController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(School s)
        {
            try {  
                SchoolRepository.Add(s);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
            
        }

        // GET: SchoolController/Edit/5
        public ActionResult Edit(int Id)
        {
            return View(SchoolRepository.GetById(Id));
        }

        // POST: SchoolController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(School updatedSchool)
        {
            try
            {
                SchoolRepository.Edit(updatedSchool);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SchoolController/Delete/5
        public ActionResult Delete(int Id)
        {
            return View(SchoolRepository.GetById(Id));
        }

        // POST: SchoolController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(School s)
        {
            try
            {
                SchoolRepository.Delete(s);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
