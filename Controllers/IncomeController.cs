using System.Collections.Generic;
using System.IO;
using System.Linq;
using MASdemo.Context;
using MASdemo.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using OfficeOpenXml;

namespace MASdemo.Controllers
{
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class IncomeController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public IncomeController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
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
                    ViewBag.Profile = HttpContext.Session.GetString("Picture");
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
                    mysqlcon.Open();
                    string queryall = "SELECT  DATE_FORMAT(c.Date, \"%M\") as Month,DATE_FORMAT(c.Date, \"%Y\") as Year,SUM(c.Tatal_amount) as Amount FROM owner o " +
                        "INNER JOIN dorm d ON d.OID = o.Oid " +
                        "INNER JOIN room r ON r.DID = d.DID " +
                        "INNER JOIN cal_info_room c ON c.RID = r.RID " +
                        "WHERE o.Oid = " + HttpContext.Session.GetInt32("Oid") + " AND d.DID = " + did + " " +
                        "GROUP BY c.Date ORDER BY c.Date DESC LIMIT 3";
                    MySqlCommand comall = new MySqlCommand(queryall);
                    comall.Connection = mysqlcon;
                    MySqlDataReader readerall = comall.ExecuteReader();
                    List<Last3Month> last3Months = new List<Last3Month>();
                    if (readerall.HasRows)
                    {
                        while (readerall.Read())
                        {
                            if (readerall["Month"].ToString() == "January")
                            {
                                monthfm = "มกราคม";
                            }
                            else if (readerall["Month"].ToString() == "February")
                            {
                                monthfm = "กุมภาพันธ์";
                            }
                            else if (readerall["Month"].ToString() == "March")
                            {
                                monthfm = "มีนาคม";
                            }
                            else if (readerall["Month"].ToString() == "April")
                            {
                                monthfm = "เมษายน";
                            }
                            else if (readerall["Month"].ToString() == "May")
                            {
                                monthfm = "พฤษภาคม";
                            }
                            else if (readerall["Month"].ToString() == "June")
                            {
                                monthfm = "มิถุนายน";
                            }
                            else if (readerall["Month"].ToString() == "July")
                            {
                                monthfm = "กรกฎาคม";
                            }
                            else if (readerall["Month"].ToString() == "August")
                            {
                                monthfm = "สิงหาคม";
                            }
                            else if (readerall["Month"].ToString() == "September")
                            {
                                monthfm = "กันยายน";
                            }
                            else if (readerall["Month"].ToString() == "October")
                            {
                                monthfm = "ตุลาคม";
                            }
                            else if (readerall["Month"].ToString() == "November")
                            {
                                monthfm = "พฤศจิกายน";
                            }
                            else if (readerall["Month"].ToString() == "December")
                            {
                                monthfm = "ธันวาคม";
                            }
                            last3Months.Add(new Last3Month()
                            {
                                Month = monthfm,
                                Year = readerall["Year"].ToString(),
                                Income = readerall.GetInt32(readerall.GetOrdinal("Amount"))
                            });
                        }
                    }
                    mysqlcon.Close();
                    ViewBag.Last3month = last3Months;
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
                ViewBag.Profile = HttpContext.Session.GetString("Picture");
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
                    mysqlcon.Open();
                    string queryall = "SELECT  DATE_FORMAT(c.Date, \"%M\") as Month,DATE_FORMAT(c.Date, \"%Y\") as Year,SUM(c.Tatal_amount) as Amount FROM owner o " +
                        "INNER JOIN dorm d ON d.OID = o.Oid " +
                        "INNER JOIN room r ON r.DID = d.DID " +
                        "INNER JOIN cal_info_room c ON c.RID = r.RID " +
                        "WHERE o.Oid = " + HttpContext.Session.GetInt32("Oid") + " " +
                        "GROUP BY c.Date ORDER BY c.Date DESC LIMIT 3";
                    MySqlCommand comall = new MySqlCommand(queryall);
                    comall.Connection = mysqlcon;
                    MySqlDataReader readerall = comall.ExecuteReader();
                    List<Last3Month> last3Months = new List<Last3Month>();
                    if (readerall.HasRows)
                    {
                        while (readerall.Read())
                        {
                            if (readerall["Month"].ToString() == "January")
                            {
                                monthfm = "มกราคม";
                            }
                            else if (readerall["Month"].ToString() == "February")
                            {
                                monthfm = "กุมภาพันธ์";
                            }
                            else if (readerall["Month"].ToString() == "March")
                            {
                                monthfm = "มีนาคม";
                            }
                            else if (readerall["Month"].ToString() == "April")
                            {
                                monthfm = "เมษายน";
                            }
                            else if (readerall["Month"].ToString() == "May")
                            {
                                monthfm = "พฤษภาคม";
                            }
                            else if (readerall["Month"].ToString() == "June")
                            {
                                monthfm = "มิถุนายน";
                            }
                            else if (readerall["Month"].ToString() == "July")
                            {
                                monthfm = "กรกฎาคม";
                            }
                            else if (readerall["Month"].ToString() == "August")
                            {
                                monthfm = "สิงหาคม";
                            }
                            else if (readerall["Month"].ToString() == "September")
                            {
                                monthfm = "กันยายน";
                            }
                            else if (readerall["Month"].ToString() == "October")
                            {
                                monthfm = "ตุลาคม";
                            }
                            else if (readerall["Month"].ToString() == "November")
                            {
                                monthfm = "พฤศจิกายน";
                            }
                            else if (readerall["Month"].ToString() == "December")
                            {
                                monthfm = "ธันวาคม";
                            }
                            last3Months.Add(new Last3Month()
                            {
                                Month = monthfm,
                                Year = readerall["Year"].ToString(),
                                Income = readerall.GetInt32(readerall.GetOrdinal("Amount"))
                            });
                        }
                    }
                    mysqlcon.Close();
                    ViewBag.Last3month = last3Months;
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
                ViewBag.Profile = HttpContext.Session.GetString("Picture");
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
                    List<Unit> units = new List<Unit>();
                    List<RoomUpdate> roomUpdates = new List<RoomUpdate>();
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
                        mysqlcon.Open();
                        string query0 = "SELECT rt.Type , COUNT(*) as Count FROM dorm d " +
                                        "INNER JOIN room r on d.DID = r.DID " +
                                        "INNER JOIN roomtype rt on r.TID = rt.TID " +
                                        "WHERE d.DID = " + did + " AND r.Status = 1 " +
                                        "GROUP BY Type";
                        MySqlCommand com0 = new MySqlCommand(query0);
                        com0.Connection = mysqlcon;
                        int rentercount = 0;
                        MySqlDataReader reader0 = com0.ExecuteReader();
                        if (reader0.HasRows)
                        {
                            while (reader0.Read())
                            {
                                roomUpdates.Add(new RoomUpdate()
                                {
                                    Type = reader0["Type"].ToString(),
                                    Count = reader0.GetInt32(reader0.GetOrdinal("Count"))
                                });
                                rentercount += reader0.GetInt32(reader0.GetOrdinal("Count"));
                            }
                        }
                        mysqlcon.Close();
                        mysqlcon.Open();
                        string query3 = "SELECT  DATE_FORMAT(c.Date, \"%M\") as Month,DATE_FORMAT(c.Date, \"%Y\") as Year,SUM(c.Total_water_unit) as Water,SUM(c.Total_elec_unit) as Elec FROM owner o " +
                            "INNER JOIN dorm d ON d.OID = o.Oid " +
                            "INNER JOIN room r ON r.DID = d.DID " +
                            "INNER JOIN cal_info_room c ON c.RID = r.RID " +
                            "WHERE o.Oid = " + HttpContext.Session.GetInt32("Oid") + " AND d.DID = " + did + " " +
                            "GROUP BY c.Date ORDER BY c.Date DESC LIMIT 3";
                        MySqlCommand com3 = new MySqlCommand(query3);
                        com3.Connection = mysqlcon;
                        MySqlDataReader reader3 = com3.ExecuteReader();
                        var monthfm = "";
                        if (reader3.HasRows)
                        {
                            while (reader3.Read())
                            {
                                if (reader3["Month"].ToString() == "January")
                                {
                                    monthfm = "1";
                                }
                                else if (reader3["Month"].ToString() == "February")
                                {
                                    monthfm = "2";
                                }
                                else if (reader3["Month"].ToString() == "March")
                                {
                                    monthfm = "3";
                                }
                                else if (reader3["Month"].ToString() == "April")
                                {
                                    monthfm = "4";
                                }
                                else if (reader3["Month"].ToString() == "May")
                                {
                                    monthfm = "5";
                                }
                                else if (reader3["Month"].ToString() == "June")
                                {
                                    monthfm = "6";
                                }
                                else if (reader3["Month"].ToString() == "July")
                                {
                                    monthfm = "7";
                                }
                                else if (reader3["Month"].ToString() == "August")
                                {
                                    monthfm = "8";
                                }
                                else if (reader3["Month"].ToString() == "September")
                                {
                                    monthfm = "9";
                                }
                                else if (reader3["Month"].ToString() == "October")
                                {
                                    monthfm = "10";
                                }
                                else if (reader3["Month"].ToString() == "November")
                                {
                                    monthfm = "11";
                                }
                                else if (reader3["Month"].ToString() == "December")
                                {
                                    monthfm = "12";
                                }
                                units.Add(new Unit()
                                {
                                    Water = reader3.GetInt32(reader3.GetOrdinal("Water")),
                                    Elec = reader3.GetInt32(reader3.GetOrdinal("Elec")),
                                    Month = monthfm,
                                    Year = reader3["Year"].ToString()
                                });
                            }
                        }
                        mysqlcon.Close();
                        ViewBag.Unit = units;
                        ViewBag.RenterCount = rentercount;
                        ViewBag.RoomUpdate = roomUpdates;
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
                    ViewBag.Profile = HttpContext.Session.GetString("Picture");
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
                    ViewBag.Profile = HttpContext.Session.GetString("Picture");
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

        public IActionResult ExportExcel(int did, string dname)
        {
            string result = "Fail";
            string sendExport = Export(did, dname);
            result = sendExport;
            return Json(result);
        }

        [HttpGet]
        [Route("Revenue/Export")]
        public string Export(int did, string dname)
        {
            string sWebRootFolder = _hostingEnvironment.WebRootPath;
            string sFileName = @"รายได้ย้อนหลังของหอ_" + dname + ".xlsx";
            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "export\\xlsx");
            string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, "export/xlsx/" + sFileName);
            FileInfo file = new FileInfo(Path.Combine(uploads, sFileName));
            if (file.Exists)
            {
                file.Delete();
                file = new FileInfo(Path.Combine(uploads, sFileName));
            }
            using (ExcelPackage package = new ExcelPackage(file))
            {
                // add a new worksheet to the empty workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Revenue_" + dname + " ");
                //First add the headers
                worksheet.Cells[1, 1].Value = "วัน-เดือน-ปี (ที่ทำการคิด)";
                worksheet.Cells[1, 2].Value = "หมายเลขห้อง";
                worksheet.Cells[1, 3].Value = "ประเภทห้อง";
                worksheet.Cells[1, 4].Value = "ราคาห้อง (บาท)";
                worksheet.Cells[1, 5].Value = "เลขมิเตอร์น้ำ";
                worksheet.Cells[1, 6].Value = "เลขมิเตอร์ไฟ";
                worksheet.Cells[1, 7].Value = "มิเตอร์น้ำที่ใช้ (หน่วย)";
                worksheet.Cells[1, 8].Value = "มิเตอร์ไฟที่ใช้ (หน่วย)";
                worksheet.Cells[1, 9].Value = "ค่าน้ำ (บาท)";
                worksheet.Cells[1, 10].Value = "ค่าไฟ (บาท)";
                worksheet.Cells[1, 11].Value = "รวมเป็นเงิน (บาท)";

                string ConnectionStringMysql = "server=localhost;database=masdatabase;user=root;pwd=;sslmode=none";
                MySqlConnection mysqlcon = new MySqlConnection(ConnectionStringMysql);
                int mycount = 0;
                mysqlcon.Open();
                string query0 = "SELECT COUNT(*) as Count FROM owner o INNER JOIN dorm d ON d.OID = o.Oid INNER JOIN room r ON r.DID = d.DID INNER JOIN cal_info_room c ON c.RID = r.RID INNER JOIN roomtype rt on r.tid = rt.TID WHERE d.DID = " + did + " ";
                MySqlCommand com0 = new MySqlCommand(query0);
                com0.Connection = mysqlcon;
                MySqlDataReader reader0 = com0.ExecuteReader();
                if (reader0.HasRows)
                {
                    while (reader0.Read())
                    {
                        mycount = reader0.GetInt32(reader0.GetOrdinal("Count"));
                        mycount += 2;
                    }
                }
                mysqlcon.Close();

                if (mycount > 0)
                {
                    mysqlcon.Open();
                    string query = "SELECT r.RoomNumber , rt.Type , rt.Price , c.Water_meter , c.Elec_meter , c.Total_water_unit , c.Total_elec_unit ,c.Total_water_amount,c.Total_elec_amount, c.Tatal_amount , DATE_FORMAT(c.Date,'%e/%M/%Y') as Date FROM owner o " +
                                        "INNER JOIN dorm d ON d.OID = o.Oid " +
                                        "INNER JOIN room r ON r.DID = d.DID " +
                                        "INNER JOIN cal_info_room c ON c.RID = r.RID " +
                                        "INNER JOIN roomtype rt on r.tid = rt.TID " +
                                        "WHERE d.DID = " + did + " ORDER BY c.Date DESC";
                    MySqlCommand com = new MySqlCommand(query);
                    com.Connection = mysqlcon;
                    MySqlDataReader reader = com.ExecuteReader();
                    List<Excel> excels = new List<Excel>();
                    int row = 1;
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            row += 1;
                            excels.Add(new Excel()
                            {
                                Row = row,
                                RoomNumber = reader.GetInt32(reader.GetOrdinal("RoomNumber")),
                                Type = reader["Type"].ToString(),
                                Price = reader.GetInt32(reader.GetOrdinal("Price")),
                                Water_meter = reader.GetInt32(reader.GetOrdinal("Water_meter")),
                                Elec_meter = reader.GetInt32(reader.GetOrdinal("Elec_meter")),
                                Total_water_unit = reader.GetInt32(reader.GetOrdinal("Total_water_unit")),
                                Total_elec_unit = reader.GetInt32(reader.GetOrdinal("Total_elec_unit")),
                                Total_water_amount = reader.GetInt32(reader.GetOrdinal("Total_water_amount")),
                                Total_elec_amount = reader.GetInt32(reader.GetOrdinal("Total_elec_amount")),
                                Total_amount = reader.GetInt32(reader.GetOrdinal("Tatal_amount")),

                                Date = reader["Date"].ToString(),
                            });
                        }
                    }
                    foreach (var ex in excels)
                    {
                        worksheet.Cells["A" + ex.Row + " "].Value = ex.Date;
                        worksheet.Cells["B" + ex.Row + " "].Value = ex.RoomNumber;
                        worksheet.Cells["C" + ex.Row + " "].Value = ex.Type;
                        worksheet.Cells["D" + ex.Row + " "].Value = ex.Price;
                        worksheet.Cells["E" + ex.Row + " "].Value = ex.Water_meter;
                        worksheet.Cells["F" + ex.Row + " "].Value = ex.Elec_meter;
                        worksheet.Cells["G" + ex.Row + " "].Value = ex.Total_water_unit;
                        worksheet.Cells["H" + ex.Row + " "].Value = ex.Total_elec_unit;
                        worksheet.Cells["I" + ex.Row + " "].Value = string.Format("{0:#,#.}", ex.Total_water_amount);
                        worksheet.Cells["J" + ex.Row + " "].Value = string.Format("{0:#,#.}", ex.Total_elec_amount);
                        worksheet.Cells["K" + ex.Row + " "].Value = string.Format("{0:#,#.}", ex.Total_amount);
                    }
                    mysqlcon.Close();
                }
                package.Save(); //Save the workbook.
            }
            return URL;
        }
    }
}