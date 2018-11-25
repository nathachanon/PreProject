using System;
using System.Collections.Generic;
using System.Data.SqlClient; 
using System.Linq;
using System.Threading.Tasks;
using MASdemo.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;

namespace MASdemo.Controllers
{
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]

    public class HomeController : Controller
    {
        public IActionResult Index()
        { 
            OracleConnection oraclecon = new OracleConnection("Pooling=false;User Id=masoracle;Password=1234;Data Source=localhost:1521/xe;");
            List<Promotion> promotions = new List<Promotion>();
            List<Announce> announces = new List<Announce>(); 
            try
            {
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
                string fails = "Fail : " + fail;
            } 
 
            try 
            { 
                oraclecon.Open(); 
                string query2 = "SELECT \"Announce_id\",\"Message\",TO_CHAR(\"Datetime\", 'DD MON YYYY') as \"Date\" FROM \"Announce\" ORDER BY \"Announce_id\" "; 
                OracleCommand oraclecom2 = new OracleCommand(query2); 
                oraclecom2.Connection = oraclecon; 
                OracleDataReader oracleReader2 = oraclecom2.ExecuteReader(); 
                if (oracleReader2.HasRows) 
                { 
                    while (oracleReader2.Read()) 
                    { 
                        announces.Add(new Announce() 
                        { 
                            Announce_id = oracleReader2.GetInt32(oracleReader2.GetOrdinal("Announce_id")), 
                            Message = oracleReader2["Message"].ToString(), 
                            Date = oracleReader2["Date"].ToString() 
                        }); 
                    } 
                } 
                oraclecon.Close(); 
            } 
            catch (Exception fail) 
            { 
                string result = "Fail" + fail; 
            }
            ViewBag.Announce = announces;
            ViewBag.Promotion = promotions;
            return View();
        }

        public IActionResult CheckOwner()
        {
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
        public IActionResult CheckDorm()
        {
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
        public IActionResult CheckRoom()
        {
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
        public IActionResult CheckCal()
        {
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
}