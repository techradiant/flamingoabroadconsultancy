﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FAC.Web.Controllers
{
    public class ServicesController : Controller
    {
        // GET: Services
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult UniversityCourseSelection()
        {
            return View();
        }

        public ActionResult AdmissionGuidance()
        {
            return View();
        }
        public ActionResult VisaAssistance()
        {
            return View();
        }
        public ActionResult CareerCounseling()
        {
            return View();
        }

    }
}