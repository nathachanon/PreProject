using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace MASdemo.Controllers
{
    public class AccountController : Controller
    {
        public string SessionName = "_Name", SessionSurname = "_Surname", SessionTel = "_Tel", SessionEmail = "_Email", SessionLog = "0";
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string email, string password)
        {
            string myEmail = email;
            string myPass = password;

            string strConnString = "Server=sql12.freesqldatabase.com; User Id=sql12257039; Password=c5wFd5f1up; Database=sql12257039; SslMode=none";
            MySqlConnection mysql = new MySqlConnection(strConnString);
            mysql.Open();
            string query = "select * from account where email = '" + myEmail + "' and password ='" + myPass + "'";
            MySqlCommand comm = new MySqlCommand(query);
            comm.Connection = mysql;
            MySqlDataReader reader = comm.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string myname = reader["name"].ToString();
                    string mysurname = reader["surname"].ToString();
                    string myemail = reader["email"].ToString();
                    string mytel = reader["tel"].ToString();

                    HttpContext.Session.SetString(SessionName, myname);
                    HttpContext.Session.SetString(SessionSurname, mysurname);
                    HttpContext.Session.SetString(SessionEmail, myemail);
                    HttpContext.Session.SetString(SessionTel, mytel);

                    ViewBag.myName = HttpContext.Session.GetString(SessionName);
                    ViewBag.mySurname = HttpContext.Session.GetString(SessionSurname);
                    ViewBag.myTel = HttpContext.Session.GetString(SessionTel);
                    ViewBag.myEmail = HttpContext.Session.GetString(SessionEmail);
                }
                HttpContext.Session.SetString(SessionLog, "1");
                ViewBag.myLog = HttpContext.Session.GetString(SessionLog);
            }
            else
            {
                HttpContext.Session.SetString(SessionLog, "0");
                ViewBag.myLog = HttpContext.Session.GetString(SessionLog);
                if (ViewBag.myLog == null)
                {
                    ViewBag.myLog = "0";
                }
            }
            mysql.Close();

            return View("Index");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            ViewBag.myName = HttpContext.Session.GetString(SessionName);
            ViewBag.mySurname = HttpContext.Session.GetString(SessionSurname);
            ViewBag.myTel = HttpContext.Session.GetString(SessionTel);
            ViewBag.myEmail = HttpContext.Session.GetString(SessionEmail);
            ViewBag.myLog = HttpContext.Session.GetString(SessionLog);
            if (ViewBag.myLog == null)
            {
                ViewBag.myLog = "0";
            }
            return View("Index");
        }
    }
}