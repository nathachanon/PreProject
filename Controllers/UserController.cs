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
        MySqlConnection mysqlconnect = new MySqlConnection("Server = localhost; User Id = root; Password=; Database=masdatabase; SslMode=none; CharacterSet=utf8;");

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
                string query = "select * from user where email = '" + lvm.email + "' and password ='" + lvm.password + "'";
                MySqlCommand comm = new MySqlCommand(query);
                comm.Connection = mysqlconnect;
                MySqlDataReader reader = comm.ExecuteReader();
                if (reader.Read())
                {
                    HttpContext.Session.SetInt32("Uid", reader.GetInt32(reader.GetOrdinal("uid")));
                    HttpContext.Session.SetString("Name", reader["name"].ToString());
                    HttpContext.Session.SetString("Surname", reader["surname"].ToString());
                    HttpContext.Session.SetString("Email", reader["email"].ToString());
                    HttpContext.Session.SetString("Tel", reader["tel"].ToString());
                    HttpContext.Session.SetString("Log", "1");
                    return RedirectToAction("Main", "Manage");

                }
                else
                {
                    HttpContext.Session.SetString("Log", "0");
                    MysqlConnection(0);
                    return RedirectToAction("Login", "User");
                }
            }
            MysqlConnection(0);
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
            string query = "INSERT INTO `user`(`email`, `password`, `name`, `surname`, `tel`) VALUES ('" + rvm.email + "','" + rvm.password + "','" + rvm.name + "','" + rvm.surname + "','" + rvm.tel + "')";
            MySqlCommand comm = new MySqlCommand(query);
            comm.Connection = mysqlconnect;
            MySqlDataReader reader = comm.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    return View("Login", "User");
                }
            }
            else
            {
            }
            MysqlConnection(0);

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