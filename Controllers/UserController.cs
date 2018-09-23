using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MASdemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace MASdemo.Controllers
{
    public class UserController : Controller
    {
        public string SessionName = "_Name", SessionSurname = "_Surname", SessionTel = "_Tel", SessionEmail = "_Email", SessionLog = "0", SessionReg = "0";
        MySqlConnection mysqlconnect = new MySqlConnection("Server = sql12.freesqldatabase.com; User Id = sql12257039; Password=c5wFd5f1up; Database=sql12257039; SslMode=none");

        public void MysqlConnection(int myconfig)
        {
            if (myconfig == 1)
            {
                mysqlconnect.Open();
            }
            else if (myconfig == 0)
            {
                mysqlconnect.Close();
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel lvm)
        {
            if (ModelState.IsValid)
            {
                MysqlConnection(1);
                string query = "select * from account where email = '" + lvm.email + "' and password ='" + lvm.password + "'";
                MySqlCommand comm = new MySqlCommand(query);
                comm.Connection = mysqlconnect;
                MySqlDataReader reader = comm.ExecuteReader();
                if (reader.Read())
                {

                    HttpContext.Session.SetString(SessionName, reader["name"].ToString());
                    HttpContext.Session.SetString(SessionSurname, reader["surname"].ToString());
                    HttpContext.Session.SetString(SessionEmail, reader["email"].ToString());
                    HttpContext.Session.SetString(SessionTel, reader["tel"].ToString());
                    HttpContext.Session.SetString(SessionLog, "1");
                    return RedirectToAction("ManageDorm", "Manage");

                }
                else
                {
                    TempData["msg"] = "Invalid credentials !";
                    HttpContext.Session.SetString(SessionLog, "0");
                    MysqlConnection(0);
                    return RedirectToAction("Login", "User");
                }
            }
            return View("Login", "User");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel rvm)
        {

            MysqlConnection(1);
            string query = "INSERT INTO `account`(`email`, `password`, `name`, `surname`, `tel`) VALUES ('" + rvm.email + "','" + rvm.password + "','" + rvm.name + "','" + rvm.surname + "','" + rvm.tel + "')";
            MySqlCommand comm = new MySqlCommand(query);
            comm.Connection = mysqlconnect;
            MySqlDataReader reader = comm.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    HttpContext.Session.SetString(SessionReg, "1");
                    return View("Login", "User");
                }
            }
            else
            {
                HttpContext.Session.SetString(SessionReg, "0");
            }
            MysqlConnection(0);

            return View("Login");
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
            return View("Login");
        }
    }
}