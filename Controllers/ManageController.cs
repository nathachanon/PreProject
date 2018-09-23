using MASdemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;

namespace MASdemo.Controllers
{
    public class ManageController : Controller
    {
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
        public IActionResult Main()
        {
            ViewBag.myUid = HttpContext.Session.GetInt32("Uid");
            ViewBag.myLog = HttpContext.Session.GetString("Log");
            ViewBag.myName = HttpContext.Session.GetString("Name");
            ViewBag.mySurname = HttpContext.Session.GetString("Surname");
            ViewBag.myTel = HttpContext.Session.GetString("Tel");
            ViewBag.myEmail = HttpContext.Session.GetString("Email");
            if (HttpContext.Session.GetInt32("Uid") == null)
            {
                return RedirectToAction("Login", "User");
            }
            else if (HttpContext.Session.GetInt32("Uid") != null & HttpContext.Session.GetInt32("Uid") != 0)
            {
                MysqlConnection(1);
                string query = "select * from dorm where uid = '" + HttpContext.Session.GetInt32("Uid") + "'";
                MySqlCommand comm = new MySqlCommand(query);
                comm.Connection = mysqlconnect;
                MySqlDataReader reader = comm.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        HttpContext.Session.SetInt32("Did", reader.GetInt32(reader.GetOrdinal("did")));
                        HttpContext.Session.SetString("D_name", reader["d_name"].ToString());
                        ViewBag.D_name = HttpContext.Session.GetString("D_name");
                        ViewBag.DidInfo = "พร้อมใช้งานระบบ เลือกหอที่ต้องการจัดการ";
                    }

                }
                else
                {
                    reader.Close();
                    return RedirectToAction("AddDorm", "Manage");
                }
            }
            MysqlConnection(0);
            return View();
        }

        [HttpGet]
        public IActionResult AddDorm()
        {
            ViewBag.myName = HttpContext.Session.GetString("Name");
            if (HttpContext.Session.GetInt32("Uid") == null)
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }

        [HttpGet]
        public IActionResult ManageDorm()
        {
            ViewBag.myName = HttpContext.Session.GetString("Name");
            if (HttpContext.Session.GetInt32("Uid") == null)
            {
                return RedirectToAction("Login", "User");
            }else if(HttpContext.Session.GetInt32("Uid") != null & HttpContext.Session.GetInt32("Uid") != 0)
            {
                MysqlConnection(1);
                string query = "select * from dorm where uid = '" + HttpContext.Session.GetInt32("Uid") + "'";
                MySqlCommand comm = new MySqlCommand(query);
                comm.Connection = mysqlconnect;
                MySqlDataReader reader = comm.ExecuteReader();
                List<Dorm> dorms = new List<Dorm>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        dorms.Add(new Dorm()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("did")),
                            Name = reader["d_name"].ToString(),
                            Room = reader.GetInt32(reader.GetOrdinal("setroom")),
                            Floor = reader.GetInt32(reader.GetOrdinal("setfloor")),
                            setRates = reader.GetInt32(reader.GetOrdinal("setrates")),
                            calRoom = reader.GetInt32(reader.GetOrdinal("setroom")) * reader.GetInt32(reader.GetOrdinal("setfloor"))
                        });
                        HttpContext.Session.SetInt32("Did", reader.GetInt32(reader.GetOrdinal("did")));
                        HttpContext.Session.SetString("D_name", reader["d_name"].ToString());
                        ViewBag.D_name = HttpContext.Session.GetString("D_name");
                        ViewBag.DidInfo = "พร้อมใช้งานระบบ เลือกหอที่ต้องการจัดการ";
                        ViewBag.dorms = dorms;
                    }

                }
                else
                {
                    reader.Close();
                    return RedirectToAction("AddDorm", "Manage");
                }

            }
            MysqlConnection(0);
            return View();
        }

        [HttpPost]
        public IActionResult AddDorm(AddDorm ad)
        {
            MysqlConnection(1);
            string query = "INSERT INTO `dorm`(`uid`, `d_name`, `setroom`, `setfloor`, `setrates`, `setelec`, " +
                "`setwater`, `add_no`, `street`, `zip_code`, `district`, `sub_district`, `province`, `picture`) " +
                "VALUES ('"+ HttpContext.Session.GetInt32("Uid") + "','"+ ad.d_name +"','"+ ad.setroom + "','"+ ad.setfloor +"'," +
                "'"+ ad.setrates + "','" + ad.setelec + "','" + ad.setwater + "','" + ad.add_no + "','" + ad.street + "'," +
                "'" + ad.zip_code + "','" + ad.district + "','" + ad.sub_district + "','" + ad.province + "','" + ad.picture + "')";
            MySqlCommand comm = new MySqlCommand(query);
            comm.Connection = mysqlconnect;
            MySqlDataReader reader = comm.ExecuteReader();
            MysqlConnection(0);
            return RedirectToAction("ManageDorm", "Manage");
        }
    }
}