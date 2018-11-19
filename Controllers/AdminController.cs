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
            if (result == "Login OK")
            {
                var myactivities = "เข้าสู่ระบบ";
                string mydate = ToChristianDateString(DateTime.Today);
                sqlcon.Open();
                string query2 = "INSERT INTO admin_log (activities, datetime , Admin_id) VALUES('" + myactivities + "', '" + mydate + " " + string.Format("{0:HH:mm:ss tt}", DateTime.Now) + "' , '" + HttpContext.Session.GetInt32("AdminID") + "' )";
                SqlCommand sqlcom2 = new SqlCommand(query2);
                sqlcom2.Connection = sqlcon;
                SqlDataReader sqlReader2 = sqlcom2.ExecuteReader();
                result = "OK";
                sqlcon.Close();
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
                var myactivities = "ออกจากระบบ";
                string mydate = ToChristianDateString(DateTime.Today);
                SqlConnection sqlcon = new SqlConnection("Data Source=(LocalDb)\\MSSQLLocalDb;Initial Catalog=MasSql;Integrated Security=True");
                sqlcon.Open();
                string query2 = "INSERT INTO admin_log (activities, datetime , Admin_id) VALUES('" + myactivities + "', '" + mydate + " " + string.Format("{0:HH:mm:ss tt}", DateTime.Now) + "' , '" + HttpContext.Session.GetInt32("AdminID") + "' )";
                SqlCommand sqlcom2 = new SqlCommand(query2);
                sqlcom2.Connection = sqlcon;
                SqlDataReader sqlReader2 = sqlcom2.ExecuteReader();
                sqlcon.Close();

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
            string result = "Fail";
            List<ReportList> reportlists = new List<ReportList>();
            try
            {
                SqlConnection sqlcon = new SqlConnection("Data Source=(LocalDb)\\MSSQLLocalDb;Initial Catalog=MasSql;Integrated Security=True");
                sqlcon.Open();
                string query1 = "SELECT RIGHT('000' + CAST([Report_id] AS varchar(5)) , 4) as Report_id2 , * FROM Report WHERE status = 1";
                SqlCommand sqlcom1 = new SqlCommand(query1);
                sqlcom1.Connection = sqlcon;
                SqlDataReader sqlReader1 = sqlcom1.ExecuteReader();
                if (sqlReader1.HasRows)
                {
                    while (sqlReader1.Read())
                    {
                        reportlists.Add(new ReportList()
                        {
                            Report_id2 = "R" + sqlReader1["Report_id2"].ToString(),
                            Report_id = sqlReader1.GetInt32(sqlReader1.GetOrdinal("Report_id")),
                            Report_message = sqlReader1["message"].ToString(),
                            Report_datetime = sqlReader1["datetime"].ToString(),
                            Report_Owner = sqlReader1["Owner_email"].ToString()
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

        public IActionResult AddReport(int report_id)
        {
            string result = "Fail";
            try
            {
                SqlConnection sqlcon = new SqlConnection("Data Source=(LocalDb)\\MSSQLLocalDb;Initial Catalog=MasSql;Integrated Security=True");
                sqlcon.Open();
                string query1 = "UPDATE Report SET Status=2 WHERE Report_id = " + report_id + " ";
                SqlCommand sqlcom1 = new SqlCommand(query1);
                sqlcom1.Connection = sqlcon;
                SqlDataReader sqlReader1 = sqlcom1.ExecuteReader();
                result = "Add OK";
                sqlcon.Close();
                sqlcon.Open();
                string query2 = "INSERT INTO Work (Admin_id, Report_id) VALUES('" + HttpContext.Session.GetInt32("AdminID") + "', '" + report_id + "') ";
                SqlCommand sqlcom2 = new SqlCommand(query2);
                sqlcom2.Connection = sqlcon;
                SqlDataReader sqlReader2 = sqlcom2.ExecuteReader();
                result = "OK";
                sqlcon.Close();

                var myactivities = "เพิ่มรายงาน report id : " + report_id;
                string mydate = ToChristianDateString(DateTime.Today);
                sqlcon.Open();
                string query3 = "INSERT INTO admin_log (activities, datetime , Admin_id) VALUES('" + myactivities + "', '" + mydate + " " + string.Format("{0:HH:mm:ss tt}", DateTime.Now) + "' , '" + HttpContext.Session.GetInt32("AdminID") + "' )";
                SqlCommand sqlcom3 = new SqlCommand(query3);
                sqlcom3.Connection = sqlcon;
                SqlDataReader sqlReader3 = sqlcom3.ExecuteReader();

                sqlcon.Close();
            }
            catch (Exception fail)
            {
                result = "Fail" + fail;
                result = "Fail";
            }
            return Json(result);
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
                    string query1 = "SELECT RIGHT('000' + CAST([Work_id] AS varchar(5)) , 4) as Work_ids , r.Owner_email , r.report_id , r.status , r.message , r.datetime  FROM [Work] w " +
                        "INNER JOIN Report r on w.Report_id = r.Report_id " +
                        "WHERE r.status = 2 " +
                        "AND w.Admin_id = " + HttpContext.Session.GetInt32("AdminID") + " ";
                    SqlCommand sqlcom1 = new SqlCommand(query1);
                    sqlcom1.Connection = sqlcon;
                    SqlDataReader sqlReader1 = sqlcom1.ExecuteReader();
                    if (sqlReader1.HasRows)
                    {
                        while (sqlReader1.Read())
                        {
                            workalls.Add(new WorkAll()
                            {
                                Work_id = "W" + sqlReader1["Work_ids"].ToString(),
                                Report_id = sqlReader1.GetInt32(sqlReader1.GetOrdinal("report_id")),
                                Report_Owner = sqlReader1["Owner_email"].ToString(),
                                Report_message = sqlReader1["message"].ToString(),
                                Report_datetime = sqlReader1["datetime"].ToString()
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

                var myactivities = "งานเสร็จสิ้น report id : " + report_id;
                string mydate = ToChristianDateString(DateTime.Today);
                sqlcon.Open();
                string query3 = "INSERT INTO admin_log (activities, datetime , Admin_id) VALUES('" + myactivities + "', '" + mydate + " " + string.Format("{0:HH:mm:ss tt}", DateTime.Now) + "' , '" + HttpContext.Session.GetInt32("AdminID") + "' )";
                SqlCommand sqlcom3 = new SqlCommand(query3);
                sqlcom3.Connection = sqlcon;
                SqlDataReader sqlReader3 = sqlcom3.ExecuteReader();

                sqlcon.Close();
            }
            catch (Exception fail)
            {
                result = "Fail" + fail;
                result = "Fail";
            }
            return Json(result);
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
                    string query1 = "insert into \"Promotion\" (\"Promotion_id\",\"Title\",\"Detail\",\"Price\",\"Startdate\", \"Enddate\") values(to_char(sequence_promotion.nextval,'FM00000'),'" + title + "','" + detail + "'," + price + ",TO_DATE('" + startdate + " 23:59:59" + "', 'yyyy-mm-dd HH24:MI:SS'),TO_DATE('" + enddate + " 23:59:59" + "', 'yyyy-mm-dd HH24:MI:SS'))";
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
                    string query1 = "SELECT \"Promotion_id\",\"Title\",\"Detail\",\"Price\",TO_CHAR(\"Startdate\", 'dd-MM-yyyy hh:mm:ss') as \"Startdate\" , TO_CHAR(\"Enddate\", 'dd-MM-yyyy hh:mm:ss') as \"Enddate\" FROM \"Promotion\" ORDER BY \"Promotion_id\" ";
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
    }
}