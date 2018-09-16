using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MASdemo.Models;
using Microsoft.AspNetCore.Http;

namespace MASdemo.Controllers
{
    public class HomeController : Controller
    {
        public string SessionName = "_Name", SessionSurname = "_Surname", SessionTel = "_Tel", SessionEmail = "_Email", SessionLog = "0";
        public IActionResult Index()
        {
            ViewBag.Log = TempData["log"];
            ViewBag.active = "Index";
            ViewBag.myName = HttpContext.Session.GetString(SessionName);
            ViewBag.mySurname = HttpContext.Session.GetString(SessionSurname);
            ViewBag.myTel = HttpContext.Session.GetString(SessionTel);
            ViewBag.myEmail = HttpContext.Session.GetString(SessionEmail);
            ViewBag.myLog = HttpContext.Session.GetString(SessionLog);
            if (ViewBag.myLog == null)
            {
                ViewBag.myLog = "0";
            }
            return View();
        }

        public IActionResult About()
        {
            ViewBag.active = "About";
            ViewData["Message"] = "Your application description page.";
            ViewBag.myName = HttpContext.Session.GetString(SessionName);
            ViewBag.mySurname = HttpContext.Session.GetString(SessionSurname);
            ViewBag.myTel = HttpContext.Session.GetString(SessionTel);
            ViewBag.myEmail = HttpContext.Session.GetString(SessionEmail);
            ViewBag.myLog = HttpContext.Session.GetString(SessionLog);
            if (ViewBag.myLog == null)
            {
                ViewBag.myLog = "0";
            }
            return View();
        }

        public IActionResult Contact()
        {
            ViewBag.active = "Contact";
            ViewData["Message"] = "Your contact page.";
            ViewBag.myName = HttpContext.Session.GetString(SessionName);
            ViewBag.mySurname = HttpContext.Session.GetString(SessionSurname);
            ViewBag.myTel = HttpContext.Session.GetString(SessionTel);
            ViewBag.myEmail = HttpContext.Session.GetString(SessionEmail);
            ViewBag.myLog = HttpContext.Session.GetString(SessionLog);
            if (ViewBag.myLog == null)
            {
                ViewBag.myLog = "0";
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
