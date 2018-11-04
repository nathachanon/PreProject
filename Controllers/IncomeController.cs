using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MASdemo.Context;
using MASdemo.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;


namespace MASdemo.Controllers
{
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class IncomeController : Controller
    {
        public IActionResult RevenueDorm(int did)
        {
            if (HttpContext.Session.GetInt32("Oid") == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                int count = 0;
                var context = new masdatabaseContext();
                IQueryable<Dorm> sdorm = from q in context.Dorm where q.Oid == HttpContext.Session.GetInt32("Oid") select q;
                foreach (var t in sdorm)
                {
                    count = 1;
                }
                if (count == 0)
                {
                    return RedirectToAction("AddDorm", "Manage");
                }
                else if (count == 1)
                {
                    var myOid = HttpContext.Session.GetInt32("Oid");
                    ViewBag.myName = HttpContext.Session.GetString("Name");
                    ViewBag.did = did;
                    var monthfm = "";
                    string ConnectionStringMysql = "server=localhost;database=masdatabase;user=root;pwd=;sslmode=none";
                    MySqlConnection mysqlcon = new MySqlConnection(ConnectionStringMysql);
                    mysqlcon.Open();
                    string query = "SELECT SUM(c.Tatal_amount) as Amount, DATE_FORMAT(c.Date,'%M') as Month, DATE_FORMAT(c.Date,'%Y') as Year, d.DName as Name FROM owner o INNER JOIN dorm d ON d.OID = o.Oid INNER JOIN room r ON r.DID = d.DID INNER JOIN cal_info_room c ON c.RID = r.RID " +
                        "WHERE o.Oid = " + myOid + " AND d.DID = " + did + " GROUP BY c.Date";
                    MySqlCommand com = new MySqlCommand(query);
                    com.Connection = mysqlcon;
                    List<Income> incomes = new List<Income>();
                    MySqlDataReader reader = com.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (reader["Month"].ToString() == "January")
                            {
                                monthfm = "1";
                            }
                            else if (reader["Month"].ToString() == "February")
                            {
                                monthfm = "2";
                            }
                            else if (reader["Month"].ToString() == "March")
                            {
                                monthfm = "3";
                            }
                            else if (reader["Month"].ToString() == "April")
                            {
                                monthfm = "4";
                            }
                            else if (reader["Month"].ToString() == "May")
                            {
                                monthfm = "5";
                            }
                            else if (reader["Month"].ToString() == "June")
                            {
                                monthfm = "6";
                            }
                            else if (reader["Month"].ToString() == "July")
                            {
                                monthfm = "7";
                            }
                            else if (reader["Month"].ToString() == "August")
                            {
                                monthfm = "8";
                            }
                            else if (reader["Month"].ToString() == "September")
                            {
                                monthfm = "9";
                            }
                            else if (reader["Month"].ToString() == "October")
                            {
                                monthfm = "10";
                            }
                            else if (reader["Month"].ToString() == "November")
                            {
                                monthfm = "11";
                            }
                            else if (reader["Month"].ToString() == "December")
                            {
                                monthfm = "12";
                            }

                            incomes.Add(new Income()
                            {
                                Month = monthfm,
                                Year = reader["Year"].ToString(),
                                Amount = reader["Amount"].ToString() + ","
                            });
                            ViewBag.Dorm_Name = reader["Name"].ToString();
                            ViewBag.GetIncome = incomes;
                        }
                    }
                    else
                    {
                        return RedirectToAction("Main", "Manage");
                    }
                    mysqlcon.Close();
                }
                return View();
            }
        }

        public IActionResult Revenue()
        {
            if (HttpContext.Session.GetInt32("Oid") == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                int count = 0;
                var context = new masdatabaseContext();
                IQueryable<Dorm> sdorm = from q in context.Dorm where q.Oid == HttpContext.Session.GetInt32("Oid") select q;
                foreach (var t in sdorm)
                {
                    count = 1;
                }
                if (count == 0)
                {
                    return RedirectToAction("AddDorm", "Manage");
                }
                ViewBag.myName = HttpContext.Session.GetString("Name");
                return View();
            }
        }


    }
}