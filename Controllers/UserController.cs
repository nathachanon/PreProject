using System.Linq;
using MASdemo.Context;
using MASdemo.Models;
using MASdemo.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MASdemo.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel user)
        {

            var context = new masdatabaseContext();
            string result = "Fail";
            string enpass = Encryption.EncryptedPass(user.password);
            var DataItem = context.Owner.Where(x => x.Email == user.email && x.Password == enpass).SingleOrDefault();
            if (DataItem != null)
            {
                HttpContext.Session.SetInt32("Oid", DataItem.Oid);
                HttpContext.Session.SetString("Name", DataItem.Name);
                HttpContext.Session.SetString("Email", DataItem.Email);
                HttpContext.Session.SetString("Surname", DataItem.Surname);
                HttpContext.Session.SetString("Tel", DataItem.Tel);
                HttpContext.Session.SetString("Log", "1");
                result = "Success";
            }
            return Json(result);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel auser)
        {
            string result = "Success";
            var context = new masdatabaseContext();
            var DataItem = context.Owner.Where(x => x.Email == auser.email).SingleOrDefault();
            if (DataItem != null)
            {
                result = "Fail";
            }
            else
            {
                string enpass = Encryption.EncryptedPass(auser.password);
                var adduser = new Owner { Email = auser.email, Password = enpass, Name = auser.name, Surname = auser.surname, Tel = auser.tel };
                context.Add(adduser);
                context.SaveChanges();
            }

            return Json(result);
        }
        public IActionResult Logout()
        {
            HttpContext.Session.SetString("Log", "0");
            ViewBag.myLog = HttpContext.Session.GetString("Log");
            HttpContext.Session.Clear();
            if (ViewBag.myLog == null)
            {
                HttpContext.Session.SetString("Log", "0");
                ViewBag.myLog = HttpContext.Session.GetString("Log");
            }

            return RedirectToAction("Login", "User");
        }
    }
}