using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FAC.Web.Models;

namespace FAC.Web.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public EmployeesController()
        {
            this._dbContext = new ApplicationDbContext();
        }

        // GET: Employees
        public ActionResult Index()
        {
            var employees = _dbContext.Employees.Include(e => e.Department);
            return View(employees.ToList());
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = _dbContext.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(_dbContext.Departments, "DepartmentId", "DepartmentName");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeId,EmployeeName,EmployeeDesignation,EmployeeAddress,EmployeePhone,EmployeeGender,City,CompanyName,PinCode,EmployeeDescription,DepartmentId,ProfilePhotoUrl,IncludeInTeamList,TeamListViewOrder,TwitterUrl,FacebookUrl,InstagramUrl,ListeningExperience,DesignExperience,LearningExperience,PersonalExperience")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Employees.Add(employee);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(_dbContext.Departments, "DepartmentId", "DepartmentName", employee.DepartmentId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = _dbContext.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(_dbContext.Departments, "DepartmentId", "DepartmentName", employee.DepartmentId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeId,EmployeeName,EmployeeDesignation,EmployeeAddress,EmployeePhone,EmployeeGender,City,CompanyName,PinCode,EmployeeDescription,DepartmentId,ProfilePhotoUrl,IncludeInTeamList,TeamListViewOrder,TwitterUrl,FacebookUrl,InstagramUrl,ListeningExperience,DesignExperience,LearningExperience,PersonalExperience")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Entry(employee).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(_dbContext.Departments, "DepartmentId", "DepartmentName", employee.DepartmentId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = _dbContext.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = _dbContext.Employees.Find(id);
            _dbContext.Employees.Remove(employee);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
