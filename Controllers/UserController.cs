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
            string loginSuccessful = "เข้าสู่ระบบสำเร็จ !";
            string loginFail = "Email หรือ Password ผิดพลาดกรุณาลองใหม่อีกครั้ง !";
            if (ModelState.IsValid)
            {
                var context = new masdatabaseContext();
                string enpass = Encryption.EncryptedPass(user.password);
                var query = from p in context.Owner where p.Email == user.email & p.Password == enpass select p;
                foreach (var a in query)
                {
                    HttpContext.Session.SetInt32("Oid", a.Oid);
                    HttpContext.Session.SetString("Name", a.Name);
                    HttpContext.Session.SetString("Surname", a.Surname);
                    HttpContext.Session.SetString("Email", a.Email);
                    HttpContext.Session.SetString("Tel", a.Tel);
                    HttpContext.Session.SetString("Log", "1");
                    TempData["loginSuccessful"] = "<script>alert('" + loginSuccessful + "');</script>";
                    return RedirectToAction("Main", "Manage");
                }
            }

            HttpContext.Session.SetString("Log", "0");
            TempData["loginFail"] = "<script>alert('" + loginFail + "');</script>";
            return View("Login", "User");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel auser)
        {
            var context = new masdatabaseContext();
            string enpass = Encryption.EncryptedPass(auser.password);
            var adduser = new Owner { Email = auser.email, Password = enpass, Name = auser.name, Surname = auser.surname, Tel = auser.tel };
            context.Add(adduser);
            context.SaveChanges();
            return View("Login");
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