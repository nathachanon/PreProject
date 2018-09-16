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
        public string SessionName = "_Name", SessionSurname = "_Surname", SessionTel = "_Tel", SessionEmail = "_Email", SessionLog = "0", SessionReg = "0";
        public IActionResult Index()
        {
            ViewBag.Register = HttpContext.Session.GetString(SessionReg);
            ViewBag.myLog = HttpContext.Session.GetString(SessionLog);
            ViewBag.myName = HttpContext.Session.GetString(SessionName);
            ViewBag.mySurname = HttpContext.Session.GetString(SessionSurname);
            ViewBag.myTel = HttpContext.Session.GetString(SessionTel);
            ViewBag.myEmail = HttpContext.Session.GetString(SessionEmail);
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
                    HttpContext.Session.SetString(SessionLog, "1");
                    ViewBag.myLog = HttpContext.Session.GetString(SessionLog);
                }
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
            HttpContext.Session.SetString(SessionLog, "0");
            ViewBag.myLog = HttpContext.Session.GetString(SessionLog);
            HttpContext.Session.Clear();
            if (ViewBag.myLog == null)
            {
                HttpContext.Session.SetString(SessionLog, "0");
                ViewBag.myLog = HttpContext.Session.GetString(SessionLog);
            }
            return View("Index");
        }

        public IActionResult Register()
        {
            ViewBag.active = "Register";
            ViewBag.Register = HttpContext.Session.GetString(SessionReg);
            return View();
        }

        [HttpPost]
        public IActionResult Register(string email, string password, string name, string surname, string tel)
        {
            string myEmail = email;
            string myPass = password;
            string myName = name;
            string mySurname = surname;
            string myTel = tel;

            string strConnString = "Server=sql12.freesqldatabase.com; User Id=sql12257039; Password=c5wFd5f1up; Database=sql12257039; SslMode=none";
            MySqlConnection mysql = new MySqlConnection(strConnString);
            mysql.Open();
            string query = "INSERT INTO `account`(`email`, `password`, `name`, `surname`, `tel`) VALUES ('"+myEmail+ "','" + myPass + "','" + myName + "','" + mySurname + "','" + myTel + "')";
            MySqlCommand comm = new MySqlCommand(query);
            comm.Connection = mysql;
            MySqlDataReader reader = comm.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    HttpContext.Session.SetString(SessionReg, "1");
                    ViewBag.Register = HttpContext.Session.GetString(SessionReg);
                }
            }
            else
            {
                HttpContext.Session.SetString(SessionReg, "0");
                ViewBag.Register = HttpContext.Session.GetString(SessionReg);
            }
            mysql.Close();

            return View("Index");
        }
    }
}