using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MASdemo.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace MASdemo.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string email,string password)
        {
            string myEmail = email;
            string myPass = password;

            string strConnString = "Server=localhost; User Id=root; Password=; Database=masdb; SslMode=none";
            MySqlConnection mysql = new MySqlConnection(strConnString);
            mysql.Open();
            string query = "select * from account where email = '"+myEmail+"' and password ='"+myPass+"'";
            MySqlCommand comm = new MySqlCommand(query);
            comm.Connection = mysql;
            MySqlDataReader reader = comm.ExecuteReader();
            if(reader.HasRows)
            {
                while (reader.Read())
                {
                    ViewBag.myName = reader["name"].ToString();
                    ViewBag.mySurname = reader["surname"].ToString();
                    ViewBag.myTel = reader["tel"].ToString();
                }
                ViewBag.status = 1;
            }
            else
            {
                ViewBag.status = 0;
            }
            mysql.Close();
            return View("Index");
        }
    }
}