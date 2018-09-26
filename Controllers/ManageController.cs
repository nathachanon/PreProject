﻿using MASdemo.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace MASdemo.Controllers
{
    public class ManageController : Controller
    {
        private readonly IHostingEnvironment he;
        MySqlConnection mysqlconnect = new MySqlConnection("Server = localhost; User Id = root; Password=; Database=masdatabase; SslMode=none");


        public ManageController(IHostingEnvironment e)
        {
            he = e;
        }

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
            }
            else if (HttpContext.Session.GetInt32("Uid") != null & HttpContext.Session.GetInt32("Uid") != 0)
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
                            calRoom = reader.GetInt32(reader.GetOrdinal("setroom")) * reader.GetInt32(reader.GetOrdinal("setfloor")),
                            picture = reader["picture"].ToString(),
                            setWater = reader.GetInt32(reader.GetOrdinal("setwater")),
                            setElec = reader.GetInt32(reader.GetOrdinal("setelec")),
                            Add_no = reader["add_no"].ToString(),
                            Street = reader["street"].ToString(),
                            sub_District = reader["sub_district"].ToString(),
                            District = reader["district"].ToString(),
                            Province = reader["province"].ToString(),
                            Zip_code = reader["zip_code"].ToString()
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
        public IActionResult AddDorm(AddDorm ad, IFormFile picture)
        {
            var fileName = "";
            if (picture != null)
            {
                var uploads = Path.Combine(he.WebRootPath, "uploads\\img_mansion");
                fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(picture.FileName);
                picture.CopyTo(new FileStream(Path.Combine(uploads, fileName), FileMode.Create));
            }
            MysqlConnection(1);
            string query = "INSERT INTO `dorm`(`uid`, `d_name`, `setroom`, `setfloor`, `setrates`, `setelec`, " +
                "`setwater`, `add_no`, `street`, `zip_code`, `district`, `sub_district`, `province`, `picture`) " +
                "VALUES ('" + HttpContext.Session.GetInt32("Uid") + "','" + ad.d_name + "','" + ad.setroom + "','" + ad.setfloor + "'," +
                "'" + ad.setrates + "','" + ad.setelec + "','" + ad.setwater + "','" + ad.add_no + "','" + ad.street + "'," +
                "'" + ad.zip_code + "','" + ad.district + "','" + ad.sub_district + "','" + ad.province + "','" + fileName + "')";
            MySqlCommand comm = new MySqlCommand(query);
            comm.Connection = mysqlconnect;
            MySqlDataReader reader = comm.ExecuteReader();
            MysqlConnection(0);
            return RedirectToAction("ManageDorm", "Manage");
        }


        public IActionResult DeleteDorm(int Id_dorm)
        {
            MysqlConnection(1);
            string query = "DELETE FROM `dorm` WHERE `did` = '" + Id_dorm + "' and `uid` = '" + HttpContext.Session.GetInt32("Uid") + "'";
            MySqlCommand comm = new MySqlCommand(query);
            comm.Connection = mysqlconnect;
            MySqlDataReader reader = comm.ExecuteReader();
            MysqlConnection(0);
            return RedirectToAction("ManageDorm", "Manage");
        }

        public IActionResult EditDorm(AddDorm ad, IFormFile picture, int Id_dorm)
        {
            MysqlConnection(1);
            if (picture != null)
            {
                var fileName = "";
                var uploads = Path.Combine(he.WebRootPath, "uploads\\img_mansion");
                fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(picture.FileName);
                picture.CopyTo(new FileStream(Path.Combine(uploads, fileName), FileMode.Create));

                if (ad.picture != null)
                {

                    string query = "UPDATE `dorm` SET `d_name`='" + ad.d_name + "',`setroom`='" + ad.setroom + "',`setfloor`='" + ad.setfloor + "'," +
                    "`setrates`='" + ad.setrates + "',`setelec`='" + ad.setelec + "',`setwater`='" + ad.setwater + "',`add_no`='" + ad.add_no + "'," +
                    "`street`='" + ad.street + "',`zip_code`='" + ad.zip_code + "',`district`='" + ad.district + "',`sub_district`='" + ad.sub_district + "'," +
                    "`province`='" + ad.province + "' WHERE did='" + Id_dorm + "'";
                    MySqlCommand comm = new MySqlCommand(query);
                    comm.Connection = mysqlconnect;
                    MySqlDataReader reader = comm.ExecuteReader();
                    MysqlConnection(0);
                    return RedirectToAction("ManageDorm", "Manage");
                }
                else
                {
                    string query = "UPDATE `dorm` SET `d_name`='" + ad.d_name + "',`setroom`='" + ad.setroom + "',`setfloor`='" + ad.setfloor + "'," +
                    "`setrates`='" + ad.setrates + "',`setelec`='" + ad.setelec + "',`setwater`='" + ad.setwater + "',`add_no`='" + ad.add_no + "'," +
                    "`street`='" + ad.street + "',`zip_code`='" + ad.zip_code + "',`district`='" + ad.district + "',`sub_district`='" + ad.sub_district + "'," +
                    "`province`='" + ad.province + "',`picture`='" + fileName + "'  WHERE did='" + Id_dorm + "'";
                    MySqlCommand comm = new MySqlCommand(query);
                    comm.Connection = mysqlconnect;
                    MySqlDataReader reader = comm.ExecuteReader();
                    MysqlConnection(0);
                    return RedirectToAction("ManageDorm", "Manage");
                }
            }
            else
            {
                if (ad.picture != null)
                {

                    string query = "UPDATE `dorm` SET `d_name`='" + ad.d_name + "',`setroom`='" + ad.setroom + "',`setfloor`='" + ad.setfloor + "'," +
                    "`setrates`='" + ad.setrates + "',`setelec`='" + ad.setelec + "',`setwater`='" + ad.setwater + "',`add_no`='" + ad.add_no + "'," +
                    "`street`='" + ad.street + "',`zip_code`='" + ad.zip_code + "',`district`='" + ad.district + "',`sub_district`='" + ad.sub_district + "'," +
                    "`province`='" + ad.province + "' WHERE did='" + Id_dorm + "'";
                    MySqlCommand comm = new MySqlCommand(query);
                    comm.Connection = mysqlconnect;
                    MySqlDataReader reader = comm.ExecuteReader();
                    MysqlConnection(0);
                    return RedirectToAction("ManageDorm", "Manage");
                }
                else
                {
                    string query = "UPDATE `dorm` SET `d_name`='" + ad.d_name + "',`setroom`='" + ad.setroom + "',`setfloor`='" + ad.setfloor + "'," +
                    "`setrates`='" + ad.setrates + "',`setelec`='" + ad.setelec + "',`setwater`='" + ad.setwater + "',`add_no`='" + ad.add_no + "'," +
                    "`street`='" + ad.street + "',`zip_code`='" + ad.zip_code + "',`district`='" + ad.district + "',`sub_district`='" + ad.sub_district + "'," +
                    "`province`='" + ad.province + "'  WHERE did='" + Id_dorm + "'";
                    MySqlCommand comm = new MySqlCommand(query);
                    comm.Connection = mysqlconnect;
                    MySqlDataReader reader = comm.ExecuteReader();
                    MysqlConnection(0);
                    return RedirectToAction("ManageDorm", "Manage");
                }
            }
        }
    }
}