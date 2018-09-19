using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MASdemo.Controllers
{
    public class ManageController : Controller
    {
        public string SessionName = "_Name", SessionSurname = "_Surname", SessionTel = "_Tel", SessionEmail = "_Email", SessionLog = "0", SessionReg = "0";
        public IActionResult Index()
        {
            ViewBag.Register = HttpContext.Session.GetString(SessionReg);
            ViewBag.myLog = HttpContext.Session.GetString(SessionLog);
            ViewBag.myName = HttpContext.Session.GetString(SessionName);
            ViewBag.mySurname = HttpContext.Session.GetString(SessionSurname);
            ViewBag.myTel = HttpContext.Session.GetString(SessionTel);
            ViewBag.myEmail = HttpContext.Session.GetString(SessionEmail);
            if(HttpContext.Session.GetString(SessionEmail) == null)
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }
    }
}