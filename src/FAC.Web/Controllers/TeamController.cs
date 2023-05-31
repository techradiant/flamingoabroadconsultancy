using FAC.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FAC.Web.Controllers
{
    public class TeamController : Controller
    {
        ApplicationDbContext _context;
        public TeamController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Team
        public ActionResult Index()
        {
          
            IEnumerable<Employee> teamMembers = _context.Employees.Where(emp=> emp.IncludeInTeamList).OrderBy(emp=> emp.TeamListViewOrder);
            
            return View(teamMembers);            
        }

        public ActionResult TeamDetails()
        {
            return View();
        }
    }
}