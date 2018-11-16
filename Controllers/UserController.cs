using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading;
using MASdemo.Context;
using MASdemo.Models;
using MASdemo.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols;
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

        public IActionResult Report()
        {
            if (HttpContext.Session.GetInt32("Oid") == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                ViewBag.myName = HttpContext.Session.GetString("Name");
                ViewBag.Email = HttpContext.Session.GetString("Email");
            }
            return View();
        }

        public IActionResult GetReport(string Email)
        {
            if (HttpContext.Session.GetInt32("Oid") == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                ViewBag.myName = HttpContext.Session.GetString("Name");
                ViewBag.Email = HttpContext.Session.GetString("Email");
                List<Report> reports = new List<Report>();
                string result = "";
                try
                {
                    SqlConnection sqlcon = new SqlConnection("Data Source=(LocalDb)\\MSSQLLocalDb;Initial Catalog=MasSql;Integrated Security=True");
                    sqlcon.Open();
                    string query1 = "SELECT RIGHT('000' + CAST([Report_id] AS varchar(5)) , 4) as Report_id, message , status , datetime FROM Report WHERE Owner_email = '" + Email + "' ";
                    SqlCommand sqlcom1 = new SqlCommand(query1);
                    sqlcom1.Connection = sqlcon;
                    SqlDataReader sqlReader1 = sqlcom1.ExecuteReader();
                    if (sqlReader1.HasRows)
                    {
                        while (sqlReader1.Read())
                        {
                            reports.Add(new Report()
                            {
                                Report_id = "R" + sqlReader1["Report_id"].ToString(),
                                Report_message = sqlReader1["message"].ToString(),
                                Report_datetime = sqlReader1["datetime"].ToString(),
                                Report_status = sqlReader1.GetInt32(sqlReader1.GetOrdinal("status"))
                            });
                        }
                    }
                    else
                    {

                    }
                    sqlcon.Close();
                }
                catch (Exception fail)
                {
                    result = "Fail" + fail;
                }
                return Json(reports);
            }
        }

        public IActionResult sendReport(string message)
        {
            string result = "Fail";
            if (HttpContext.Session.GetInt32("Oid") == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                try
                {
                    string mydate = ToChristianDateString(DateTime.Today);
                    SqlConnection sqlcon = new SqlConnection("Data Source=(LocalDb)\\MSSQLLocalDb;Initial Catalog=MasSql;Integrated Security=True");
                    sqlcon.Open();
                    string query1 = "INSERT INTO Report (message, status, datetime, Owner_email) VALUES('" + message + "', 1, '" + mydate + " " + string.Format("{0:HH:mm:ss tt}", DateTime.Now) + "', '" + HttpContext.Session.GetString("Email") + "')";
                    SqlCommand sqlcom1 = new SqlCommand(query1);
                    sqlcom1.Connection = sqlcon;
                    SqlDataReader sqlReader1 = sqlcom1.ExecuteReader();
                    result = "OK";
                    sqlcon.Close();
                }
                catch (Exception fail)
                {
                    result = "Fail" + fail;
                    result = "Fail";
                }
            }
            return Json(result);
        }

        public string ToChristianDateString(DateTime dateTime)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            return dateTime.ToString("yyyy-MM-dd");

        }
    }
}