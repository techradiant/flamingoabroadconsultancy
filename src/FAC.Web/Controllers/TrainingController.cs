using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FAC.Web.Controllers
{
    
    public class TrainingController : Controller
    {
        // GET: Training
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IELTS()
        {
            ViewBag.CurrentTraining = "IELTS";
            return View();
        }
        public ActionResult PTE()
        {
            ViewBag.CurrentTraining = "PTE";
            return View();
        }
        public ActionResult TOEFL()
        {
            ViewBag.CurrentTraining = "TOEFL";
            return View();
        }
        public ActionResult EnglishSpoken()
        {
            ViewBag.CurrentTraining = "EnglishSpoken";
            return View();
        }
    }
}