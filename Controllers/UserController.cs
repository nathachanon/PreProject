using System.Linq;
using MASdemo.Context;
using MASdemo.Models;
using MASdemo.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace MASdemo.Controllers
{
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]

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

        public IActionResult ChangePassword()
        {
            if (HttpContext.Session.GetInt32("Oid") == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                ViewBag.myName = HttpContext.Session.GetString("Name");
                return View();
            }
        }

        public IActionResult ChangePassword2(string password, string newpassword)
        {
            if (HttpContext.Session.GetInt32("Oid") == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                string result = "Fail";
                string passDb = "";
                string ConnectionStringMysql = "server=localhost;database=masdatabase;user=root;pwd=;sslmode=none";
                MySqlConnection mysqlcon = new MySqlConnection(ConnectionStringMysql);
                mysqlcon.Open();
                string query = "SELECT Password FROM owner WHERE Oid = " + HttpContext.Session.GetInt32("Oid") + "";
                MySqlCommand com = new MySqlCommand(query);
                com.Connection = mysqlcon;
                MySqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    passDb = reader["Password"].ToString();
                }
                mysqlcon.Close();
                string enpassword = Encryption.EncryptedPass(password);
                if (enpassword != passDb)
                {
                    return Json(result);
                }
                else
                {
                    string ennewpassword = Encryption.EncryptedPass(newpassword);
                    string query2 = "UPDATE `owner` SET `Password`='" + ennewpassword + "' WHERE Oid = " + HttpContext.Session.GetInt32("Oid") + "";
                    mysqlcon.Open();
                    MySqlCommand com2 = new MySqlCommand(query2);
                    com2.Connection = mysqlcon;
                    MySqlDataReader reader2 = com2.ExecuteReader();
                    mysqlcon.Close();
                    result = "Ok";
                    return Json(result);
                }
            }

        }
    }
}