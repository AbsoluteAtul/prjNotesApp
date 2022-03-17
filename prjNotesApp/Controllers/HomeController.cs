using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using prjNotesApp.Models;

namespace prjNotesApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Notes";
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Contact";
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "About Us";
            return View();
        }

    }
}