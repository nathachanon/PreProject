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
                            ViewBag.DormName = reader["Name"];
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
                ViewBag.myName = HttpContext.Session.GetString("Name");
                var monthfm = "";
                int count = 0;
                var myOid = HttpContext.Session.GetInt32("Oid");
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
                else
                {
                    string ConnectionStringMysql = "server=localhost;database=masdatabase;user=root;pwd=;sslmode=none";
                    MySqlConnection mysqlcon = new MySqlConnection(ConnectionStringMysql);
                    mysqlcon.Open();
                    string query = "SELECT d.DID as DID , SUM(c.Tatal_amount) as Amount, DATE_FORMAT(c.Date,'%M') as Month, DATE_FORMAT(c.Date,'%Y') as Year, d.DName as Name FROM owner o INNER JOIN dorm d ON d.OID = o.Oid INNER JOIN room r ON r.DID = d.DID INNER JOIN cal_info_room c ON c.RID = r.RID " +
                        "WHERE o.Oid = " + myOid + " GROUP BY d.DName,c.Date";
                    MySqlCommand com = new MySqlCommand(query);
                    com.Connection = mysqlcon;
                    List<IncomeAll> incomeall = new List<IncomeAll>();
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

                            incomeall.Add(new IncomeAll()
                            {
                                Month = monthfm,
                                Did = reader.GetInt32(reader.GetOrdinal("DID")),
                                Year = reader["Year"].ToString(),
                                Amount = reader["Amount"].ToString() + ","
                            });
                            ViewBag.incomeall = incomeall;
                        }
                    }
                    else
                    {
                        return RedirectToAction("Main", "Manage");
                    }
                    mysqlcon.Close();
                    mysqlcon.Open();
                    var countdid = 0;
                    var countdid2 = 0;
                    var thisdid = 0;
                    string query1 = "SELECT did , DName FROM dorm WHERE OID = " + myOid + " ";
                    MySqlCommand com1 = new MySqlCommand(query1);
                    com1.Connection = mysqlcon;
                    List<did> dids = new List<did>();
                    MySqlDataReader reader1 = com1.ExecuteReader();
                    if (reader1.HasRows)
                    {
                        while (reader1.Read())
                        {
                            dids.Add(new did()
                            {
                                Did = reader1.GetInt32(reader1.GetOrdinal("did")),
                                Name = reader1["DName"].ToString() + ","
                            });
                            ViewBag.did = dids;
                        }
                    }
                    mysqlcon.Close();
                    foreach (var d in dids)
                    {
                        mysqlcon.Open();
                        string query2 = "SELECT COUNT(DID) as Count , DID FROM (SELECT d.DID as DID , SUM(c.Tatal_amount) as Amount, DATE_FORMAT(c.Date,'%M') as Month, DATE_FORMAT(c.Date,'%Y') as Year, d.DName as Name FROM owner o INNER JOIN dorm d ON d.OID = o.Oid INNER JOIN room r ON r.DID = d.DID INNER JOIN cal_info_room c ON c.RID = r.RID WHERE o.Oid = " + myOid + " AND d.DID = " + d.Did + " GROUP BY d.DName,c.Date) Mytable";
                        MySqlCommand com2 = new MySqlCommand(query2);
                        com2.Connection = mysqlcon;
                        MySqlDataReader reader2 = com2.ExecuteReader();
                        if (reader2.HasRows)
                        {
                            while (reader2.Read())
                            {
                                countdid = reader2.GetInt32(reader2.GetOrdinal("Count"));
                                if (countdid >= countdid2)
                                {
                                    countdid2 = countdid;
                                    thisdid = reader2.GetInt32(reader2.GetOrdinal("DID"));
                                }
                            }
                        }
                        mysqlcon.Close();
                    }
                    ViewBag.ThisDid = thisdid;
                    return View();
                }
            }
        }

        public IActionResult ExtrapolateDorm(int did)
        {
            if (HttpContext.Session.GetInt32("Oid") == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                ViewBag.myName = HttpContext.Session.GetString("Name");
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
                    var datacount = 0;
                    string ConnectionStringMysql = "server=localhost;database=masdatabase;user=root;pwd=;sslmode=none";
                    MySqlConnection mysqlcon = new MySqlConnection(ConnectionStringMysql);
                    mysqlcon.Open();
                    string query = "SELECT COUNT(Amount) as Count FROM (" +
                        "SELECT SUM(c.Tatal_amount) as Amount FROM owner o " +
                        "INNER JOIN dorm d ON d.OID = o.Oid " +
                        "INNER JOIN room r ON r.DID = d.DID " +
                        "INNER JOIN cal_info_room c ON c.RID = r.RID " +
                        "WHERE o.Oid = " + myOid + " AND d.DID = " + did + " " +
                        "GROUP BY c.Date " +
                        "ORDER BY c.Date DESC " +
                        "LIMIT 3 " +
                        ") Extrapolate";
                    MySqlCommand com = new MySqlCommand(query);
                    com.Connection = mysqlcon;
                    List<Income> incomes = new List<Income>();
                    MySqlDataReader reader = com.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            datacount = reader.GetInt32(reader.GetOrdinal("Count"));
                        }
                    }
                    mysqlcon.Close();
                    if (datacount >= 3)
                    {
                        mysqlcon.Open();
                        string query2 = "SELECT CAST(AVG(Amount) AS DECIMAL(10)) as Extrapolate , d.DName as Name FROM (SELECT SUM(c.Tatal_amount) as Amount FROM owner o INNER JOIN dorm d ON d.OID = o.Oid INNER JOIN room r ON r.DID = d.DID INNER JOIN cal_info_room c ON c.RID = r.RID WHERE o.Oid = " + myOid + " AND d.DID = " + did + " GROUP BY c.Date ORDER BY c.Date DESC LIMIT 3) Extrapolate INNER JOIN dorm d WHERE d.DID = " + did + " ";
                        MySqlCommand com2 = new MySqlCommand(query2);
                        com2.Connection = mysqlcon;
                        MySqlDataReader reader2 = com2.ExecuteReader();
                        if (reader2.HasRows)
                        {
                            while (reader2.Read())
                            {
                                ViewBag.ExtrapolateDorm = reader2.GetInt32(reader2.GetOrdinal("Extrapolate"));
                                ViewBag.Dorm_Name = reader2["Name"];
                            }
                        }
                        mysqlcon.Close();
                    }
                    else
                    {
                        return RedirectToAction("Main", "Manage");
                    }
                }
                return View();
            }
        }

        public IActionResult Extrapolate()
        {
            if (HttpContext.Session.GetInt32("Oid") == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                var myoid = HttpContext.Session.GetInt32("Oid");
                int count = 0;
                int amountall = 0;
                var context = new masdatabaseContext();
                List<isDid> isDids = new List<isDid>();
                List<Extrapolate> Extrapolate1 = new List<Extrapolate>();
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
                    ViewBag.myName = HttpContext.Session.GetString("Name");
                    var myOid = HttpContext.Session.GetInt32("Oid");
                    var datacount = 0;
                    string ConnectionStringMysql = "server=localhost;database=masdatabase;user=root;pwd=;sslmode=none";
                    MySqlConnection mysqlcon = new MySqlConnection(ConnectionStringMysql);
                    mysqlcon.Open();
                    string query = "SELECT DID FROM dorm WHERE OID = " + myoid + " ";
                    MySqlCommand com = new MySqlCommand(query);
                    com.Connection = mysqlcon;
                    MySqlDataReader reader = com.ExecuteReader();
                    while (reader.Read())
                    {
                        isDids.Add(new isDid()
                        {
                            Did = reader.GetInt32(reader.GetOrdinal("DID"))
                        });
                    }
                    mysqlcon.Close();

                    foreach (var d in isDids)
                    {
                        mysqlcon.Open();
                        string query2 = "SELECT COUNT(Amount) as Count FROM (" +
                            "SELECT SUM(c.Tatal_amount) as Amount FROM owner o " +
                            "INNER JOIN dorm d ON d.OID = o.Oid " +
                            "INNER JOIN room r ON r.DID = d.DID " +
                            "INNER JOIN cal_info_room c ON c.RID = r.RID " +
                            "WHERE o.Oid = " + myOid + " AND d.DID = " + d.Did + " " +
                            "GROUP BY c.Date " +
                            "ORDER BY c.Date DESC " +
                            "LIMIT 3 " +
                            ") Extrapolate";
                        MySqlCommand com2 = new MySqlCommand(query2);
                        com2.Connection = mysqlcon;
                        List<Income> incomes = new List<Income>();
                        MySqlDataReader reader2 = com2.ExecuteReader();
                        if (reader2.HasRows)
                        {
                            while (reader2.Read())
                            {
                                datacount = reader2.GetInt32(reader2.GetOrdinal("Count"));
                            }
                        }
                        mysqlcon.Close();
                        if (datacount >= 3)
                        {
                            mysqlcon.Open();
                            string query3 = "SELECT CAST(AVG(Amount) AS DECIMAL(10)) as Extrapolate , d.DName as Name , d.DID as DID FROM (SELECT SUM(c.Tatal_amount) as Amount FROM owner o INNER JOIN dorm d ON d.OID = o.Oid INNER JOIN room r ON r.DID = d.DID INNER JOIN cal_info_room c ON c.RID = r.RID WHERE o.Oid = " + myOid + " AND d.DID = " + d.Did + " GROUP BY c.Date ORDER BY c.Date DESC LIMIT 3) Extrapolate INNER JOIN dorm d WHERE d.DID = " + d.Did + " ";
                            MySqlCommand com3 = new MySqlCommand(query3);
                            com3.Connection = mysqlcon;
                            MySqlDataReader reader3 = com3.ExecuteReader();
                            if (reader3.HasRows)
                            {
                                while (reader3.Read())
                                {
                                    Extrapolate1.Add(new Extrapolate()
                                    {
                                        Did = reader3.GetInt32(reader3.GetOrdinal("DID")),
                                        DName = reader3["Name"].ToString(),
                                        Extrapolates = reader3.GetInt32(reader3.GetOrdinal("Extrapolate"))
                                    });
                                    amountall += reader3.GetInt32(reader3.GetOrdinal("Extrapolate"));

                                }
                            }
                            mysqlcon.Close();
                        }
                    }
                }
                ViewBag.AmountAll = amountall;
                ViewBag.ExtrapolateAll = Extrapolate1;
                return View();
            }
        }

        public IActionResult IncomeAll(int oid)
        {
            if (HttpContext.Session.GetInt32("Oid") == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                int amountall = 0;
                string ConnectionStringMysql = "server=localhost;database=masdatabase;user=root;pwd=;sslmode=none";
                MySqlConnection mysqlcon = new MySqlConnection(ConnectionStringMysql);
                mysqlcon.Open();
                string query = "SELECT DID FROM dorm WHERE OID = " + oid + " ";
                MySqlCommand com = new MySqlCommand(query);
                com.Connection = mysqlcon;
                MySqlDataReader reader = com.ExecuteReader();
                List<isDid> isDids = new List<isDid>();
                while (reader.Read())
                {
                    isDids.Add(new isDid()
                    {
                        Did = reader.GetInt32(reader.GetOrdinal("DID"))
                    });
                }
                mysqlcon.Close();
                foreach (var d in isDids)
                {
                    mysqlcon.Open();
                    string query2 = "SELECT SUM(c.Tatal_amount) as Amount FROM owner o INNER JOIN dorm d ON d.OID = o.Oid INNER JOIN room r ON r.DID = d.DID INNER JOIN cal_info_room c ON c.RID = r.RID " +
                        "WHERE o.Oid = " + oid + " AND d.DID = " + d.Did + " GROUP BY c.Date ORDER BY c.Date DESC LIMIT 1";
                    MySqlCommand com2 = new MySqlCommand(query2);
                    com2.Connection = mysqlcon;
                    MySqlDataReader reader2 = com2.ExecuteReader();
                    if (reader2.HasRows)
                    {
                        while (reader2.Read())
                        {
                            amountall += reader2.GetInt32(reader2.GetOrdinal("Amount"));
                        }
                    }
                    mysqlcon.Close();
                }
                return Json(amountall);
            }
        }

        public IActionResult GetExt(int oid)
        {
            if (HttpContext.Session.GetInt32("Oid") == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                int count = 0;
                int amountall = 0;
                var context = new masdatabaseContext();
                List<isDid> isDids = new List<isDid>();
                List<Extrapolate> Extrapolate1 = new List<Extrapolate>();
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
                    ViewBag.myName = HttpContext.Session.GetString("Name");
                    var datacount = 0;
                    string ConnectionStringMysql = "server=localhost;database=masdatabase;user=root;pwd=;sslmode=none";
                    MySqlConnection mysqlcon = new MySqlConnection(ConnectionStringMysql);
                    mysqlcon.Open();
                    string query = "SELECT DID FROM dorm WHERE OID = " + oid + " ";
                    MySqlCommand com = new MySqlCommand(query);
                    com.Connection = mysqlcon;
                    MySqlDataReader reader = com.ExecuteReader();
                    while (reader.Read())
                    {
                        isDids.Add(new isDid()
                        {
                            Did = reader.GetInt32(reader.GetOrdinal("DID"))
                        });
                    }
                    mysqlcon.Close();

                    foreach (var d in isDids)
                    {
                        mysqlcon.Open();
                        string query2 = "SELECT COUNT(Amount) as Count FROM (" +
                            "SELECT SUM(c.Tatal_amount) as Amount FROM owner o " +
                            "INNER JOIN dorm d ON d.OID = o.Oid " +
                            "INNER JOIN room r ON r.DID = d.DID " +
                            "INNER JOIN cal_info_room c ON c.RID = r.RID " +
                            "WHERE o.Oid = " + oid + " AND d.DID = " + d.Did + " " +
                            "GROUP BY c.Date " +
                            "ORDER BY c.Date DESC " +
                            "LIMIT 3 " +
                            ") Extrapolate";
                        MySqlCommand com2 = new MySqlCommand(query2);
                        com2.Connection = mysqlcon;
                        List<Income> incomes = new List<Income>();
                        MySqlDataReader reader2 = com2.ExecuteReader();
                        if (reader2.HasRows)
                        {
                            while (reader2.Read())
                            {
                                datacount = reader2.GetInt32(reader2.GetOrdinal("Count"));
                            }
                        }
                        mysqlcon.Close();
                        if (datacount >= 3)
                        {
                            mysqlcon.Open();
                            string query3 = "SELECT CAST(AVG(Amount) AS DECIMAL(10)) as Extrapolate , d.DName as Name , d.DID as DID FROM (SELECT SUM(c.Tatal_amount) as Amount FROM owner o INNER JOIN dorm d ON d.OID = o.Oid INNER JOIN room r ON r.DID = d.DID INNER JOIN cal_info_room c ON c.RID = r.RID WHERE o.Oid = " + oid + " AND d.DID = " + d.Did + " GROUP BY c.Date ORDER BY c.Date DESC LIMIT 3) Extrapolate INNER JOIN dorm d WHERE d.DID = " + d.Did + " ";
                            MySqlCommand com3 = new MySqlCommand(query3);
                            com3.Connection = mysqlcon;
                            MySqlDataReader reader3 = com3.ExecuteReader();
                            if (reader3.HasRows)
                            {
                                while (reader3.Read())
                                {
                                    amountall += reader3.GetInt32(reader3.GetOrdinal("Extrapolate"));
                                }
                            }
                            mysqlcon.Close();
                        }
                    }
                }
                return Json(amountall);
            }
        }
    }
}