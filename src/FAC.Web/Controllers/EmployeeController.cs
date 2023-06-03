using FAC.Web.ViewModels;
using FAC.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FAC.Web.Models;
using System.Data.Entity;

namespace FAC.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public EmployeeController()
        {
            this._dbContext = new ApplicationDbContext();
        }
        // GET: Employee  
        public ActionResult Index()
        {
            var employeeList = this._dbContext.Employees.Include(x => x.Department).ToList();
            return View(employeeList);
        }

        public ActionResult AddEmployees()
        {
            var employeeViewModel = new EmployeeViewModel()
            {
                Department = this._dbContext.Departments.ToList(),
                Employee = new Employee()
            };
            return View("EmployeeForm", employeeViewModel);
        }
        [HttpPost]
        public ActionResult AddEmployees(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                var existingEmployee = this._dbContext.Employees.FirstOrDefault(x => x.EmployeeId == employee.EmployeeId);
                if (existingEmployee == null)
                {
                    existingEmployee = new Employee()
                    {
                        EmployeeId = employee.EmployeeId,
                        EmployeeName = employee.EmployeeName,
                        City = employee.City,

                    };
                }
                var employeeViewModel = new EmployeeViewModel()
                {
                    Department = this._dbContext.Departments.ToList(),
                    Employee = existingEmployee
                };
                return View("EmployeeForm", employeeViewModel);
            }

            if (employee.EmployeeId == 0)
                this._dbContext.Employees.Add(employee);

            else
            {
                var employeesDb = this._dbContext.Employees.FirstOrDefault(x => x.EmployeeId == employee.EmployeeId);
                employeesDb.EmployeeName = employee.EmployeeName;
                employeesDb.EmployeeDesignation = employee.EmployeeDesignation;
                employeesDb.EmployeeAddress = employee.EmployeeAddress;
                employeesDb.EmployeePhone = employee.EmployeePhone;
                employeesDb.EmployeeGender = employee.EmployeeGender;
                employeesDb.City = employee.City;

                employeesDb.CompanyName = employee.CompanyName;
                employeesDb.PinCode = employee.PinCode;
                employeesDb.EmployeeDescription = employee.EmployeeDescription;
                employeesDb.DepartmentId = employee.DepartmentId;
                employeesDb.ProfilePhotoUrl = employee.ProfilePhotoUrl;
                employeesDb.IncludeInTeamList = employee.IncludeInTeamList;
                employeesDb.TeamListViewOrder = employee.TeamListViewOrder;
                employeesDb.TwitterUrl = employee.TwitterUrl;
                employeesDb.FacebookUrl = employee.FacebookUrl;
                employeesDb.InstagramUrl = employee.InstagramUrl;
            }

            this._dbContext.SaveChanges();
            return RedirectToAction("Index", "Employee");
        }

        public ActionResult Edit(int id)
        {
            var employees = this._dbContext.Employees.FirstOrDefault(x => x.EmployeeId == id);
            var department = this._dbContext.Departments.ToList();

            var viewModel = new EmployeeViewModel()
            {
                Department = department,
                Employee = employees
            };
            return View("EmployeeForm", viewModel);
        }

        [HttpPost]
        public ActionResult Save(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                var existingEmployee = this._dbContext.Employees.FirstOrDefault(x => x.EmployeeId == employee.EmployeeId);
                if (existingEmployee == null)
                {
                    existingEmployee = new Employee()
                    {
                        EmployeeId = employee.EmployeeId,
                        EmployeeName= employee.EmployeeName,
                        City = employee.City,

                    };
                }
                var employeeViewModel = new EmployeeViewModel()
                {
                    Department = this._dbContext.Departments.ToList(),
                    Employee = existingEmployee
                };
                return View("EmployeeForm", employeeViewModel);
            }

            if (employee.EmployeeId == 0)
                this._dbContext.Employees.Add(employee);

            else
            {
                var employeesDb = this._dbContext.Employees.FirstOrDefault(x => x.EmployeeId == employee.EmployeeId);
                employeesDb.EmployeeName = employee.EmployeeName;
                employeesDb.EmployeeDesignation = employee.EmployeeDesignation;
                employeesDb.EmployeeAddress = employee.EmployeeAddress;
                employeesDb.EmployeePhone = employee.EmployeePhone;
                employeesDb.EmployeeGender = employee.EmployeeGender;
                employeesDb.City = employee.City;

                employeesDb.CompanyName = employee.CompanyName;
                employeesDb.PinCode = employee.PinCode;
                employeesDb.EmployeeDescription = employee.EmployeeDescription;
                employeesDb.DepartmentId = employee.DepartmentId;
                employeesDb.ProfilePhotoUrl = employee.ProfilePhotoUrl;
                employeesDb.IncludeInTeamList = employee.IncludeInTeamList;
                employeesDb.TeamListViewOrder = employee.TeamListViewOrder;
                employeesDb.TwitterUrl = employee.TwitterUrl;
                employeesDb.FacebookUrl = employee.FacebookUrl;
                employeesDb.InstagramUrl = employee.InstagramUrl;
            }

            this._dbContext.SaveChanges();
            return RedirectToAction("Index", "Employee");
        }

        public ActionResult Delete(int id)
        {
            var employeeDb = this._dbContext.Employees.FirstOrDefault(x => x.EmployeeId == id);
            this._dbContext.Employees.Remove(employeeDb);
            this._dbContext.SaveChanges();

            return RedirectToAction("Index", "Employee");
        }

    }
}