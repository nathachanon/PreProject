using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Threading;
using MASdemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;

namespace MASdemo.Controllers
{
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]

    public class AdminController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult CheckLogin(string username, string password)
        {
            string result = "Fail";
            SqlConnection sqlcon = new SqlConnection("Data Source=(LocalDb)\\MSSQLLocalDb;Initial Catalog=MasSql;Integrated Security=True");
            try
            {
                sqlcon.Open();
                string query1 = "SELECT * FROM Admin WHERE Username = '" + username + "' AND Password = '" + password + "' AND Status = 1 ";
                SqlCommand sqlcom1 = new SqlCommand(query1);
                sqlcom1.Connection = sqlcon;
                SqlDataReader sqlReader1 = sqlcom1.ExecuteReader();
                if (sqlReader1.HasRows)
                {
                    while (sqlReader1.Read())
                    {
                        HttpContext.Session.SetInt32("AdminID", sqlReader1.GetInt32(sqlReader1.GetOrdinal("Admin_id")));
                        HttpContext.Session.SetString("AdminName", sqlReader1["Name"].ToString());
                        HttpContext.Session.SetString("AdminSurname", sqlReader1["Surname"].ToString());
                        HttpContext.Session.SetString("AdminTel", sqlReader1["Tel"].ToString());
                        HttpContext.Session.SetInt32("AdminStatus", sqlReader1.GetInt32(sqlReader1.GetOrdinal("Status")));
                        HttpContext.Session.SetString("AdminUsername", sqlReader1["Username"].ToString());
                        HttpContext.Session.SetString("AdminPassword", sqlReader1["Password"].ToString());
                        result = "Login OK";
                    }
                }
                else
                {
                    result = "Fail";
                }
                sqlcon.Close();
            }
            catch (Exception fail)
            {
                result = "Fail" + fail;
                result = "Fail";
            }
            return Json(result);
        }

        public IActionResult Administrator()
        {
            if (HttpContext.Session.GetInt32("AdminID") == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                ViewBag.myName = HttpContext.Session.GetString("AdminName");
            }
            return View();
        }

        public IActionResult Logout()
        {
            if (HttpContext.Session.GetInt32("AdminID") == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                HttpContext.Session.SetString("Log", "0");
                ViewBag.myLog = HttpContext.Session.GetString("Log");
                HttpContext.Session.Clear();
                if (ViewBag.myLog == null)
                {
                    HttpContext.Session.SetString("Log", "0");
                    ViewBag.myLog = HttpContext.Session.GetString("Log");
                }
            }
            return RedirectToAction("Login", "Admin");
        }

        public IActionResult CheckReport()
        {
            if (HttpContext.Session.GetInt32("AdminID") == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                ViewBag.myName = HttpContext.Session.GetString("AdminName");
            }
            return View();
        }
        public IActionResult CheckOwner()
        {
            if (HttpContext.Session.GetInt32("AdminID") == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                ViewBag.myName = HttpContext.Session.GetString("AdminName");
                string result = "Fail";
                string ConnectionStringMysql = "server=localhost;database=masdatabase;user=root;pwd=;sslmode=none";
                MySqlConnection mysqlcon = new MySqlConnection(ConnectionStringMysql);
                mysqlcon.Open();
                string query = "SELECT COUNT(*) as Count FROM `owner`";
                MySqlCommand com = new MySqlCommand(query);
                com.Connection = mysqlcon;
                MySqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    result = reader.GetInt32(reader.GetOrdinal("Count")).ToString();
                }
                mysqlcon.Close();
                return Json(result);
            }
        }
        public IActionResult CheckDorm()
        {
            if (HttpContext.Session.GetInt32("AdminID") == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                ViewBag.myName = HttpContext.Session.GetString("AdminName");
                string result = "Fail";
                string ConnectionStringMysql = "server=localhost;database=masdatabase;user=root;pwd=;sslmode=none";
                MySqlConnection mysqlcon = new MySqlConnection(ConnectionStringMysql);
                mysqlcon.Open();
                string query = "SELECT COUNT(*) as Count FROM `dorm`";
                MySqlCommand com = new MySqlCommand(query);
                com.Connection = mysqlcon;
                MySqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    result = reader.GetInt32(reader.GetOrdinal("Count")).ToString();
                }
                mysqlcon.Close();
                return Json(result);
            }
        }
        public IActionResult CheckRoom()
        {
            if (HttpContext.Session.GetInt32("AdminID") == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                ViewBag.myName = HttpContext.Session.GetString("AdminName");
                string result = "Fail";
                string ConnectionStringMysql = "server=localhost;database=masdatabase;user=root;pwd=;sslmode=none";
                MySqlConnection mysqlcon = new MySqlConnection(ConnectionStringMysql);
                mysqlcon.Open();
                string query = "SELECT COUNT(*) as Count FROM `room`";
                MySqlCommand com = new MySqlCommand(query);
                com.Connection = mysqlcon;
                MySqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    result = reader.GetInt32(reader.GetOrdinal("Count")).ToString();
                }
                mysqlcon.Close();
                return Json(result);
            }
        }
        public IActionResult CheckCal()
        {
            if (HttpContext.Session.GetInt32("AdminID") == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                ViewBag.myName = HttpContext.Session.GetString("AdminName");
                string result = "Fail";
                string ConnectionStringMysql = "server=localhost;database=masdatabase;user=root;pwd=;sslmode=none";
                MySqlConnection mysqlcon = new MySqlConnection(ConnectionStringMysql);
                mysqlcon.Open();
                string query = "SELECT COUNT(*) as Count FROM `cal_info_room`";
                MySqlCommand com = new MySqlCommand(query);
                com.Connection = mysqlcon;
                MySqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    result = reader.GetInt32(reader.GetOrdinal("Count")).ToString();
                }
                mysqlcon.Close();
                return Json(result);
            }
        }

        public IActionResult ReportList()
        {
            if (HttpContext.Session.GetInt32("AdminID") == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            { 
                string result = "Fail"; 
                List<ReportList> reportlists = new List<ReportList>(); 
                try 
                { 
                    SqlConnection sqlcon = new SqlConnection("Data Source=(LocalDb)\\MSSQLLocalDb;Initial Catalog=MasSql;Integrated Security=True"); 
                    sqlcon.Open(); 
                    string query2 = "SELECT RIGHT('000' + CAST([Report_id] AS varchar(5)) , 4) as Report_id2 , o.Email as Owner_email , r.message , r.datetime , r.report_id  FROM Report r " + 
                        "INNER JOIN owner o ON r.owner_id = o.oid " + 
                        "WHERE status = 1 "; 
                    SqlCommand sqlcom2 = new SqlCommand(query2); 
                    sqlcom2.Connection = sqlcon; 
                    SqlDataReader sqlReader2 = sqlcom2.ExecuteReader(); 
                    if (sqlReader2.HasRows) 
                    { 
                        while (sqlReader2.Read()) 
                        { 
                            reportlists.Add(new ReportList() 
                            { 
                                Report_id2 = "R" + sqlReader2["Report_id2"].ToString(), 
                                Report_id = sqlReader2.GetInt32(sqlReader2.GetOrdinal("Report_id")), 
                                Report_message = sqlReader2["message"].ToString(), 
                                Report_datetime = sqlReader2["datetime"].ToString(), 
                                Owner_email = sqlReader2["Owner_email"].ToString() 
                            }); 
                        } 
                    } 
                    sqlcon.Close(); 
                } 
                catch (Exception fail) 
                { 
                    result = "Fail" + fail; 
                    result = "Fail"; 
                } 
                return Json(reportlists); 
            }
        }

        public IActionResult AddReport(int report_id)
        {
            string result = "Fail";
            if (HttpContext.Session.GetInt32("AdminID") == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            { 
                try 
                { 
                    SqlConnection sqlcon = new SqlConnection("Data Source=(LocalDb)\\MSSQLLocalDb;Initial Catalog=MasSql;Integrated Security=True"); 
                    sqlcon.Open(); 
                    string query1 = "UPDATE Report SET Status=2,Admin_id=" + HttpContext.Session.GetInt32("AdminID") + " WHERE Report_id = " + report_id + " "; 
                    SqlCommand sqlcom1 = new SqlCommand(query1); 
                    sqlcom1.Connection = sqlcon; 
                    SqlDataReader sqlReader1 = sqlcom1.ExecuteReader(); 
                    result = "Add OK"; 
                    sqlcon.Close(); 
                } 
                catch (Exception fail) 
                { 
                    result = "Fail" + fail; 
                    result = "Fail"; 
                } 
                return Json(result); 
            }
        }

        public string ToChristianDateString(DateTime dateTime)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            return dateTime.ToString("yyyy-MM-dd");

        }

        public IActionResult Work()
        {
            if (HttpContext.Session.GetInt32("AdminID") == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                ViewBag.myName = HttpContext.Session.GetString("AdminName");
            }
            return View();
        }

        public IActionResult CheckWork()
        {
            if (HttpContext.Session.GetInt32("AdminID") == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                List<WorkAll> workalls = new List<WorkAll>();
                string result = "Fail";
                try
                {
                    SqlConnection sqlcon = new SqlConnection("Data Source=(LocalDb)\\MSSQLLocalDb;Initial Catalog=MasSql;Integrated Security=True"); 
                        sqlcon.Open(); 
                        string query2 = " SELECT RIGHT('0000' + CAST([Report_id] AS varchar(5)) , 4) as Report_ids ,Report_id, Admin_id , message , datetime , o.Email as Owner_email FROM [Report] r INNER JOIN owner o ON r.owner_id = o.oid WHERE Admin_id = "+ HttpContext.Session.GetInt32("AdminID") + " AND Status = 2 "; 
                        SqlCommand sqlcom2 = new SqlCommand(query2); 
                        sqlcom2.Connection = sqlcon; 
                        SqlDataReader sqlReader2 = sqlcom2.ExecuteReader(); 
                        if (sqlReader2.HasRows) 
                        { 
                            while (sqlReader2.Read()) 
                            { 
                                workalls.Add(new WorkAll() 
                                { 
 
                                    Report_ids = "R" + sqlReader2["Report_ids"].ToString(), 
                                    Report_id = sqlReader2.GetInt32(sqlReader2.GetOrdinal("Report_id")), 
                                    Owner_email = sqlReader2["Owner_email"].ToString(), 
                                    Report_message = sqlReader2["message"].ToString(), 
                                    Report_datetime = sqlReader2["datetime"].ToString() 
                                }); 
                            } 
                        } 
                        sqlcon.Close();
                }
                catch (Exception fail)
                {
                    result = "Fail" + fail;
                    result = "Fail";
                }
                return Json(workalls);
            }
        }

        public IActionResult Complete(int report_id)
        {
            string result = "Fail";
            if (HttpContext.Session.GetInt32("AdminID") == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            { 
                try 
                { 
                    SqlConnection sqlcon = new SqlConnection("Data Source=(LocalDb)\\MSSQLLocalDb;Initial Catalog=MasSql;Integrated Security=True"); 
                    sqlcon.Open(); 
                    string query1 = "UPDATE Report SET Status=3 WHERE Report_id = " + report_id + " "; 
                    SqlCommand sqlcom1 = new SqlCommand(query1); 
                    sqlcom1.Connection = sqlcon; 
                    SqlDataReader sqlReader1 = sqlcom1.ExecuteReader(); 
                    result = "Add OK"; 
                    sqlcon.Close(); 
                } 
                catch (Exception fail) 
                { 
                    result = "Fail" + fail; 
                    result = "Fail"; 
                } 
                return Json(result); 
            }
        }

        public IActionResult Promotion()
        {
            if (HttpContext.Session.GetInt32("AdminID") == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                ViewBag.myName = HttpContext.Session.GetString("AdminName");
            }
            return View();
        }

        public IActionResult AddPromotion(string title, string detail, int price, string startdate, string enddate)
        {
            if (HttpContext.Session.GetInt32("AdminID") == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                string result = "Fail";
                ViewBag.myName = HttpContext.Session.GetString("AdminName");
                try
                {
                    OracleConnection oraclecon = new OracleConnection("Pooling=false;User Id=masoracle;Password=1234;Data Source=localhost:1521/xe;");
                    oraclecon.Open();
                    string query1 = "insert into \"Promotion\" (\"Promotion_id\",\"Title\",\"Detail\",\"Price\",\"Startdate\", \"Enddate\") values(to_char(sequence_promotion.nextval,'FM00000'),'" + title + "','" + detail + "'," + price + ",TO_DATE('" + startdate + "', 'yyyy-mm-dd'),TO_DATE('" + enddate + "', 'yyyy-mm-dd'))";
                    OracleCommand oraclecom1 = new OracleCommand(query1);
                    oraclecom1.Connection = oraclecon;
                    OracleDataReader oracleReader1 = oraclecom1.ExecuteReader();
                    result = "Insert OK";
                    oraclecon.Close();
                }
                catch (Exception fail)
                {
                    result = "Fail" + fail;
                }

                return Json(result);
            }
        }

        public IActionResult LoadPromotion()
        {
            if (HttpContext.Session.GetInt32("AdminID") == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                string result = "Fail";
                ViewBag.myName = HttpContext.Session.GetString("AdminName");
                List<Promotion> promotions = new List<Promotion>();
                try
                {
                    OracleConnection oraclecon = new OracleConnection("Pooling=false;User Id=masoracle;Password=1234;Data Source=localhost:1521/xe;");
                    oraclecon.Open();
                    string query1 = "SELECT \"Promotion_id\",\"Title\",\"Detail\",\"Price\",TO_CHAR(\"Startdate\", 'DD MON YYYY') as \"Startdate\" , TO_CHAR(\"Enddate\", 'DD MON YYYY') as \"Enddate\" FROM \"Promotion\" ORDER BY \"Promotion_id\" ";
                    OracleCommand oraclecom1 = new OracleCommand(query1);
                    oraclecom1.Connection = oraclecon;
                    OracleDataReader oracleReader1 = oraclecom1.ExecuteReader();
                    if (oracleReader1.HasRows)
                    {
                        while (oracleReader1.Read())
                        {
                            promotions.Add(new Promotion()
                            {
                                Promotion_id = oracleReader1.GetInt32(oracleReader1.GetOrdinal("Promotion_id")),
                                Title = oracleReader1["Title"].ToString(),
                                Detail = oracleReader1["Detail"].ToString(),
                                Price = oracleReader1.GetInt32(oracleReader1.GetOrdinal("Price")),
                                StartDate = oracleReader1["StartDate"].ToString(),
                                EndDate = oracleReader1["EndDate"].ToString()
                            });
                        }
                    }
                    oraclecon.Close();
                }
                catch (Exception fail)
                {
                    result = "Fail" + fail;
                }

                return Json(promotions);
            }
        }

        public IActionResult DeletePromotion(int promotion_id)
        {
            if (HttpContext.Session.GetInt32("AdminID") == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                string result = "Fail";
                ViewBag.myName = HttpContext.Session.GetString("AdminName");
                try
                {
                    OracleConnection oraclecon = new OracleConnection("Pooling=false;User Id=masoracle;Password=1234;Data Source=localhost:1521/xe;");
                    oraclecon.Open();
                    string query1 = "DELETE FROM \"Promotion\" WHERE \"Promotion_id\" = " + promotion_id + " ";
                    OracleCommand oraclecom1 = new OracleCommand(query1);
                    oraclecom1.Connection = oraclecon;
                    OracleDataReader oracleReader1 = oraclecom1.ExecuteReader();
                    result = "Delete";
                    oraclecon.Close();
                }
                catch (Exception fail)
                {
                    result = "Fail" + fail;
                }

                return Json(result);
            }
        }

        public IActionResult Announce()
        {
            if (HttpContext.Session.GetInt32("AdminID") == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                ViewBag.myName = HttpContext.Session.GetString("AdminName");
            }
            return View();
        }

        public IActionResult LoadAnnounce()
        {
            if (HttpContext.Session.GetInt32("AdminID") == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                string result = "Fail";
                ViewBag.myName = HttpContext.Session.GetString("AdminName");
                List<Announce> announces = new List<Announce>();
                try
                {
                    OracleConnection oraclecon = new OracleConnection("Pooling=false;User Id=masoracle;Password=1234;Data Source=localhost:1521/xe;");
                    oraclecon.Open();
                    string query1 = "SELECT \"Announce_id\",\"Message\",TO_CHAR(\"Datetime\", 'DD MON YYYY') as \"Date\" FROM \"Announce\" ORDER BY \"Announce_id\" ";
                    OracleCommand oraclecom1 = new OracleCommand(query1);
                    oraclecom1.Connection = oraclecon;
                    OracleDataReader oracleReader1 = oraclecom1.ExecuteReader();
                    if (oracleReader1.HasRows)
                    {
                        while (oracleReader1.Read())
                        {
                            announces.Add(new Announce()
                            {
                                Announce_id = oracleReader1.GetInt32(oracleReader1.GetOrdinal("Announce_id")),
                                Message = oracleReader1["Message"].ToString(),
                                Date = oracleReader1["Date"].ToString()
                            });
                        }
                    }
                    oraclecon.Close();
                }
                catch (Exception fail)
                {
                    result = "Fail" + fail;
                }
                return Json(announces);
            }
        } 
 
        public IActionResult AddAnnounce(string message)
        {
            if (HttpContext.Session.GetInt32("AdminID") == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                string result = "Fail";
                ViewBag.myName = HttpContext.Session.GetString("AdminName");
                try
                {
                    OracleConnection oraclecon = new OracleConnection("Pooling=false;User Id=masoracle;Password=1234;Data Source=localhost:1521/xe;");
                    oraclecon.Open();
                    string query1 = "insert into \"Announce\" (\"Announce_id\", \"Message\",\"Datetime\") values(to_char(sequence_announce.nextval,'FM00000'),'"+message+"',TO_DATE(SYSDATE, 'dd-mm-yyyy')) ";
                    OracleCommand oraclecom1 = new OracleCommand(query1);
                    oraclecom1.Connection = oraclecon;
                    OracleDataReader oracleReader1 = oraclecom1.ExecuteReader();
                    result = "Insert OK";
                    oraclecon.Close();
                }
                catch (Exception fail)
                {
                    result = "Fail" + fail;
                }

                return Json(result);
            }
        } 
 
        public IActionResult DeleteAnnounce(int announce_id)
        {
            if (HttpContext.Session.GetInt32("AdminID") == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                string result = "Fail";
                ViewBag.myName = HttpContext.Session.GetString("AdminName");
                try
                {
                    OracleConnection oraclecon = new OracleConnection("Pooling=false;User Id=masoracle;Password=1234;Data Source=localhost:1521/xe;");
                    oraclecon.Open();
                    string query1 = "DELETE FROM \"Announce\" WHERE \"Announce_id\" = " + announce_id + " ";
                    OracleCommand oraclecom1 = new OracleCommand(query1);
                    oraclecom1.Connection = oraclecon;
                    OracleDataReader oracleReader1 = oraclecom1.ExecuteReader();
                    result = "Delete";
                    oraclecon.Close();
                }
                catch (Exception fail)
                {
                    result = "Fail" + fail;
                }

                return Json(result);
            }
        }

        public IActionResult UpdateAnnounce(int aid , string message) 
        { 
            if (HttpContext.Session.GetInt32("AdminID") == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            { 
                string result = "Fail"; 
                try 
                { 
                    OracleConnection oraclecon = new OracleConnection("Pooling=false;User Id=masoracle;Password=1234;Data Source=localhost:1521/xe;"); 
                    oraclecon.Open(); 
                    string query1 = " UPDATE \"Announce\" SET \"Message\" = '" + message + "' WHERE \"Announce_id\" = " + aid + " "; 
                    OracleCommand oraclecom1 = new OracleCommand(query1); 
                    oraclecom1.Connection = oraclecon; 
                    oraclecom1.ExecuteReader(); 
                    result = "UPDATE OK"; 
                    oraclecon.Close(); 
                } 
                catch (Exception fail) 
                { 
                    result = "Fail" + fail; 
                } 
                return Json(result); 
            } 
        }

    }
}