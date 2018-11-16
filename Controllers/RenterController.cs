using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MASdemo.Context;
using MASdemo.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System.Globalization;

namespace MASdemo.Controllers

{
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]

    public class RenterController : Controller
    {
        private readonly IHostingEnvironment he;
        public RenterController(IHostingEnvironment e)
        {
            he = e;
        }
        public IActionResult ShowRoom(int did)
        {
            if (HttpContext.Session.GetInt32("Oid") == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                ViewBag.myName = HttpContext.Session.GetString("Name");
                ViewBag.did = did;
                int mydid = did;
                var context = new masdatabaseContext();
                if (mydid > 0)
                {
                    var dorm = context.Dorm.Where(x => x.Did == mydid).SingleOrDefault();
                    if (dorm != null)
                    {
                        ViewBag.dorm_name = dorm.Dname;
                    }
                    var countfloor = context.SetFloorRoom.Where(x => x.Did == mydid).Count();
                    ViewBag.allFloor = countfloor;

                    var floors = from a in context.SetFloorRoom where a.Did == mydid select a;
                    List<SetFloorRoom> sfr = new List<SetFloorRoom>();
                    foreach (var n in floors)
                    {
                        if (n != null)
                        {
                            sfr.Add(new SetFloorRoom()
                            {
                                Floor = n.Floor,
                                Room = n.Room
                            });
                            ViewBag.sfr = sfr;
                        }
                    }

                    var roomq = from q in context.Room where q.Did == mydid select q;
                    List<Room> rooms = new List<Room>();
                    foreach (var q in roomq)
                    {
                        if (q != null)
                        {
                            rooms.Add(new Room()
                            {
                                RoomNumber = q.RoomNumber,
                                Tid = q.Tid,
                                Status = q.Status,
                                Rid = q.Rid
                            });
                            ViewBag.rooms = rooms;
                        }
                    }

                    var renter = from x in context.Renter where x.Rid.ToString().Contains(mydid.ToString()) select x;
                    List<myRenter> renters = new List<myRenter>();
                    foreach (var x in renter)
                    {
                        renters.Add(new myRenter()
                        {
                            Rid = x.Rid,
                            RenId = x.RenId,
                            RenName = x.RenName,
                            RenSurename = x.RenSurename,
                            RenAge = x.RenAge,
                            RenTel = x.RenTel,
                            StartWaterMeter = x.StartWaterMeter,
                            StartElecMeter = x.StartElecMeter,
                            RenSsnPicture = x.RenSsnPicture,
                            RenAgreement = x.RenAgreement,
                            DateTime = x.DateTime.ToString("dd MMMM yyyy"),
                            DateMonth = x.DateTime.ToString("MM-yyyy")
                        });
                        ViewBag.renters = renters;
                    }
                    var countrt = context.Roomtype.Count(me => me.Did == mydid || me.Tid == 0);
                    ViewBag.rt_count = countrt;
                    var rts = context.Roomtype.Where(my => my.Did == mydid || my.Tid == 0);
                    List<Roomtype> roomtypes = new List<Roomtype>();
                    foreach (var rta in rts)
                    {
                        roomtypes.Add(new Roomtype()
                        {
                            Tid = rta.Tid,
                            Type = rta.Type,
                            Price = rta.Price
                        });
                        ViewBag.roomtypes = roomtypes;
                    }
                }
            }
            return View();
        }

        public IActionResult EditRenter(myRenter renter, int did, int tid, IFormFile ssn, IFormFile agreement)
        {
            if (HttpContext.Session.GetInt32("Oid") == null)
            {
                return RedirectToAction("Login", "User");
            }
            {
                var ssnName = "";
                var agreementName = "";
                if (ssn != null)
                {
                    var uploads = Path.Combine(he.WebRootPath, "uploads\\img_ssn");
                    ssnName = Guid.NewGuid().ToString().Substring(0, 10) + Path.GetExtension(ssn.FileName);
                    ssn.CopyTo(new FileStream(Path.Combine(uploads, ssnName), FileMode.Create));
                }
                if (agreement != null)
                {
                    var uploads = Path.Combine(he.WebRootPath, "uploads\\agreement");
                    agreementName = Guid.NewGuid().ToString().Substring(0, 10) + Path.GetExtension(agreement.FileName);
                    agreement.CopyTo(new FileStream(Path.Combine(uploads, agreementName), FileMode.Create));
                }
                int mydid = did;
                var context = new masdatabaseContext();
                if (ssn == null & agreement == null)
                {
                    var editroomtype = context.Room.First(b => b.Rid == renter.Rid);
                    editroomtype.Tid = tid;
                    context.SaveChanges();
                    var editrenter = context.Renter.First(a => a.RenId == renter.RenId);
                    editrenter.RenName = renter.RenName;
                    editrenter.RenSurename = renter.RenSurename;
                    editrenter.RenAge = renter.RenAge;
                    editrenter.RenTel = renter.RenTel;
                    editrenter.StartWaterMeter = renter.StartWaterMeter;
                    editrenter.StartElecMeter = renter.StartElecMeter;
                    context.SaveChanges();
                    TempData["EditSuccessful"] = "<script>swal({type: 'success', title: 'แก้ไขข้อมูลผู้เช่าสำเร็จ', showConfirmButton: false,  timer: 1500,backdrop: 'rgba(0,0, 26,0.8)' })</script>";
                }
                else if (ssn == null & agreement != null)
                {
                    var editroomtype = context.Room.First(b => b.Rid == renter.Rid);
                    editroomtype.Tid = tid;
                    context.SaveChanges();
                    var editrenter = context.Renter.First(a => a.RenId == renter.RenId);
                    editrenter.RenName = renter.RenName;
                    editrenter.RenSurename = renter.RenSurename;
                    editrenter.RenAge = renter.RenAge;
                    editrenter.RenTel = renter.RenTel;
                    editrenter.StartWaterMeter = renter.StartWaterMeter;
                    editrenter.StartElecMeter = renter.StartElecMeter;
                    editrenter.RenAgreement = agreementName;
                    context.SaveChanges();
                    TempData["EditSuccessful"] = "<script>swal({type: 'success', title: 'แก้ไขข้อมูลผู้เช่าสำเร็จ', showConfirmButton: false,  timer: 1500,backdrop: 'rgba(0,0, 26,0.8)' })</script>";

                }
                else if (ssn != null & agreement == null)
                {
                    var editroomtype = context.Room.First(b => b.Rid == renter.Rid);
                    editroomtype.Tid = tid;
                    context.SaveChanges();
                    var editrenter = context.Renter.First(a => a.RenId == renter.RenId);
                    editrenter.RenName = renter.RenName;
                    editrenter.RenSurename = renter.RenSurename;
                    editrenter.RenAge = renter.RenAge;
                    editrenter.RenTel = renter.RenTel;
                    editrenter.StartWaterMeter = renter.StartWaterMeter;
                    editrenter.StartElecMeter = renter.StartElecMeter;
                    editrenter.RenSsnPicture = ssnName;
                    context.SaveChanges();
                    TempData["EditSuccessful"] = "<script>swal({type: 'success', title: 'แก้ไขข้อมูลผู้เช่าสำเร็จ', showConfirmButton: false,  timer: 1500,backdrop: 'rgba(0,0, 26,0.8)' })</script>";

                }
                else if (ssn != null & agreement != null)
                {
                    var editroomtype = context.Room.First(b => b.Rid == renter.Rid);
                    editroomtype.Tid = tid;
                    context.SaveChanges();
                    var editrenter = context.Renter.First(a => a.RenId == renter.RenId);
                    editrenter.RenName = renter.RenName;
                    editrenter.RenSurename = renter.RenSurename;
                    editrenter.RenAge = renter.RenAge;
                    editrenter.RenTel = renter.RenTel;
                    editrenter.StartWaterMeter = renter.StartWaterMeter;
                    editrenter.StartElecMeter = renter.StartElecMeter;
                    editrenter.RenSsnPicture = ssnName;
                    editrenter.RenAgreement = agreementName;
                    context.SaveChanges();
                    TempData["EditSuccessful"] = "<script>swal({type: 'success', title: 'แก้ไขข้อมูลผู้เช่าสำเร็จ', showConfirmButton: false,  timer: 1500,backdrop: 'rgba(0,0, 26,0.8)' })</script>";

                }
                return RedirectToAction("ShowRoom", "Renter", new { did = mydid });
            }
        }

        [HttpPost]
        public IActionResult set0Status(int status, int did)
        {
            string result = "Fail";
            CultureInfo ci = CultureInfo.InstalledUICulture;
            if (HttpContext.Session.GetInt32("Oid") == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                var context = new masdatabaseContext();
                if (status != 0)
                {
                    var editstatus = context.Room.First(a => a.Rid == status);
                    editstatus.Tid = 0;
                    editstatus.Status = "0";
                    context.SaveChanges();
                    var editrenter = context.Renter.First(a => a.Rid == status);
                    editrenter.RenName = null;
                    editrenter.RenSurename = null;
                    editrenter.RenTel = null;
                    editrenter.RenAge = null;
                    editrenter.RenSsnPicture = null;
                    editrenter.StartElecMeter = null;
                    editrenter.StartWaterMeter = null;
                    editrenter.RenAgreement = null;
                    if (ci.ThreeLetterWindowsLanguageName == "THA")
                    {
                        editrenter.DateTime = Convert.ToDateTime("0544-01-01");
                    }
                    else
                    {
                        editrenter.DateTime = Convert.ToDateTime("0001-01-01");
                    }
                    context.SaveChanges();
                    result = "Update OK";
                }
            }
            return Json(result);
        }

        [HttpPost]
        public IActionResult set1Status(int status)
        {
            string result = "Fail";
            if (HttpContext.Session.GetInt32("Oid") == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                var context = new masdatabaseContext();
                if (status != 0)
                {
                    var editstatus = context.Room.First(a => a.Rid == status);
                    editstatus.Status = "1";
                    context.SaveChanges();
                    result = "Update OK";
                }
            }
            return Json(result);
        }
        public IActionResult getRenter(int oid)
        {
            if (HttpContext.Session.GetInt32("Oid") == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                int renterroom = 0;
                string ConnectionStringMysql = "server=localhost;database=masdatabase;user=root;pwd=;sslmode=none";
                MySqlConnection mysqlcon = new MySqlConnection(ConnectionStringMysql);
                mysqlcon.Open();
                string query = "SELECT COUNT(r.RID) FROM owner o INNER JOIN dorm d ON d.OID = o.Oid INNER JOIN room r ON r.DID = d.DID WHERE o.Oid = '" + oid + "' AND r.Status = 1";
                MySqlCommand com = new MySqlCommand(query);
                com.Connection = mysqlcon;
                MySqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    renterroom = reader.GetInt32(reader.GetOrdinal("COUNT(r.RID)"));
                }
                mysqlcon.Close();
                return Json(renterroom);
            }
        }

        public IActionResult getRenter0(int oid)
        {
            if (HttpContext.Session.GetInt32("Oid") == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                int renterroom = 0;
                string ConnectionStringMysql = "server=localhost;database=masdatabase;user=root;pwd=;sslmode=none";
                MySqlConnection mysqlcon = new MySqlConnection(ConnectionStringMysql);
                mysqlcon.Open();
                string query = "SELECT COUNT(r.RID) FROM owner o INNER JOIN dorm d ON d.OID = o.Oid INNER JOIN room r ON r.DID = d.DID WHERE o.Oid = '" + oid + "' AND r.Status = 0";
                MySqlCommand com = new MySqlCommand(query);
                com.Connection = mysqlcon;
                MySqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    renterroom = reader.GetInt32(reader.GetOrdinal("COUNT(r.RID)"));
                }
                mysqlcon.Close();
                return Json(renterroom);
            }
        }

        public IActionResult Meter(int did)
        {
            if (HttpContext.Session.GetInt32("Oid") == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                HttpContext.Session.SetInt32("isDid", did);
                int countdid = 0;
                ViewBag.myName = HttpContext.Session.GetString("Name");
                string ConnectionStringMysql = "server=localhost;database=masdatabase;user=root;pwd=;sslmode=none";
                MySqlConnection mysqlcon = new MySqlConnection(ConnectionStringMysql);
                mysqlcon.Open();
                string query = "SELECT COUNT(*) , o.Oid , d.DID FROM owner o INNER JOIN dorm d ON d.OID = o.Oid WHERE o.Oid = " + HttpContext.Session.GetInt32("Oid") + " AND d.DID = " + did + "";
                MySqlCommand com = new MySqlCommand(query);
                com.Connection = mysqlcon;
                MySqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    countdid = reader.GetInt32(reader.GetOrdinal("COUNT(*)"));
                }
                if (countdid == 1)
                {
                    ViewBag.DormId = did;
                }
                else
                {
                    return RedirectToAction("Main", "Manage");
                }
                mysqlcon.Close();
                return View();
            }
        }

        public IActionResult getRenterMeter(string datemonth_old, string datemonth, int roomid)
        {
            var result = new { Result = "Fail", Water = 0, Elec = 0 };
            if (datemonth != null & datemonth_old != null & roomid != 0)
            {
                string ConnectionStringMysql = "server=localhost;database=masdatabase;user=root;pwd=;sslmode=none";
                MySqlConnection mysqlcon = new MySqlConnection(ConnectionStringMysql);
                mysqlcon.Open();
                string f1 = "SELECT Count(*) FROM owner o INNER JOIN dorm d ON d.OID = o.Oid " +
                    "INNER JOIN room r ON r.DID = d.DID INNER JOIN cal_info_room c ON c.RID = r.RID" +
                    " WHERE o.OID = " + HttpContext.Session.GetInt32("Oid") + " " +
                    "AND d.DID = " + HttpContext.Session.GetInt32("isDid") + " " +
                    "AND r.RoomNumber = " + roomid + " " +
                    "AND r.Status = 1 " +
                    "AND c.Date = '" + datemonth + "-01' ";
                MySqlCommand com0 = new MySqlCommand(f1);
                com0.Connection = mysqlcon;
                MySqlDataReader reader0 = com0.ExecuteReader();
                int monthcount = 0;
                while (reader0.Read())
                {
                    monthcount = reader0.GetInt32(reader0.GetOrdinal("Count(*)"));
                }
                mysqlcon.Close();
                if (monthcount == 0)
                {
                    mysqlcon.Open();
                    string query = "SELECT d.DID, r.RoomNumber, c.Water_meter,c.Elec_meter FROM owner o INNER JOIN dorm d ON d.OID = o.Oid INNER JOIN room r ON r.DID = d.DID INNER JOIN cal_info_room c ON c.RID = r.RID WHERE o.OID = " + HttpContext.Session.GetInt32("Oid") + " AND d.DID = " + HttpContext.Session.GetInt32("isDid") + " AND r.RoomNumber = " + roomid + " AND c.Date = '" + datemonth_old + "-01' ";
                    MySqlCommand com = new MySqlCommand(query);
                    com.Connection = mysqlcon;
                    MySqlDataReader reader = com.ExecuteReader();
                    while (reader.Read())
                    {
                        result = new { Result = "Successed", Water = reader.GetInt32(reader.GetOrdinal("Water_meter")), Elec = reader.GetInt32(reader.GetOrdinal("Elec_meter")) };
                    }
                    mysqlcon.Close();

                    mysqlcon.Open();
                    string query2 = "SELECT Count(*) FROM owner o INNER JOIN dorm d ON d.OID = o.Oid INNER JOIN room r ON r.DID = d.DID INNER JOIN cal_info_room c ON c.RID = r.RID WHERE o.OID = " + HttpContext.Session.GetInt32("Oid") + " AND d.DID = " + HttpContext.Session.GetInt32("isDid") + " AND r.RoomNumber = " + roomid + " AND c.Date = '" + datemonth_old + "-01' ";
                    MySqlCommand com2 = new MySqlCommand(query2);
                    com2.Connection = mysqlcon;
                    MySqlDataReader reader2 = com2.ExecuteReader();
                    int mycount = 0;
                    while (reader2.Read())
                    {
                        mycount = reader2.GetInt32(reader2.GetOrdinal("Count(*)"));
                    }
                    mysqlcon.Close();
                    if (mycount == 0)
                    {
                        mysqlcon.Open();
                        string query3 = "SELECT d.DID, r.RoomNumber,rt.Start_Water_Meter,rt.Start_Elec_Meter FROM owner o INNER JOIN dorm d ON d.OID = o.Oid INNER JOIN room r ON r.DID = d.DID INNER JOIN renter rt ON rt.RID = r.RID WHERE o.OID = " + HttpContext.Session.GetInt32("Oid") + " AND d.DID = " + HttpContext.Session.GetInt32("isDid") + " AND r.RoomNumber = " + roomid + " ";
                        MySqlCommand com3 = new MySqlCommand(query3);
                        com3.Connection = mysqlcon;
                        MySqlDataReader reader3 = com3.ExecuteReader();
                        while (reader3.Read())
                        {
                            result = new { Result = "Successed", Water = reader3.GetInt32(reader3.GetOrdinal("Start_Water_Meter")), Elec = reader3.GetInt32(reader3.GetOrdinal("Start_Elec_Meter")) };
                        }
                        mysqlcon.Close();
                    }
                }
                else
                {
                    result = new { Result = "Fail", Water = 0, Elec = 0 };
                }
            }
            else if (datemonth != null & datemonth_old != null & roomid == 0)
            {

            }
            return Json(result);
        }

        public IActionResult getRenterMeter2()
        {
            string ConnectionStringMysql = "server=localhost;database=masdatabase;user=root;pwd=;sslmode=none";
            MySqlConnection mysqlcon = new MySqlConnection(ConnectionStringMysql);
            mysqlcon.Open();
            string query = "SELECT o.Oid ,d.DID, r.RoomNumber, r.Status FROM owner o INNER JOIN dorm d ON d.OID = o.Oid INNER JOIN room r ON r.DID = d.DID WHERE o.Oid = " + HttpContext.Session.GetInt32("Oid") + " AND d.DID = " + HttpContext.Session.GetInt32("isDid") + " AND r.Status = 1";
            MySqlCommand com = new MySqlCommand(query);
            com.Connection = mysqlcon;
            List<RoomId> roomIds = new List<RoomId>();
            MySqlDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                roomIds.Add(new RoomId()
                {
                    Roomid = reader.GetInt32(reader.GetOrdinal("RoomNumber"))
                });
            }
            mysqlcon.Close();
            return Json(roomIds);
        }

        public IActionResult AddCal(int roomid, int meterwater, int meterelec, string datemonth, int oldwater, int oldelec)
        {
            string ConnectionStringMysql = "server=localhost;database=masdatabase;user=root;pwd=;sslmode=none";
            MySqlConnection mysqlcon = new MySqlConnection(ConnectionStringMysql);
            string result = "Fail";
            int waterprice = 0;
            int elecprice = 0;
            int roomprice = 0;
            int myrid = 0;
            string getDorm = "select d.Set_water_unit, d.Set_elec_unit FROM dorm d INNER JOIN owner o ON o.Oid = d.OID WHERE d.DID = " + HttpContext.Session.GetInt32("isDid") + " and o.Oid = " + HttpContext.Session.GetInt32("Oid") + " ";
            mysqlcon.Open();
            MySqlCommand com = new MySqlCommand(getDorm);
            com.Connection = mysqlcon;
            MySqlDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                waterprice = reader.GetInt32(reader.GetOrdinal("Set_water_unit"));
                elecprice = reader.GetInt32(reader.GetOrdinal("Set_elec_unit"));
            }
            mysqlcon.Close();

            string getRoomtype = "SELECT r.RID,t.Price FROM owner o INNER JOIN dorm d ON d.OID = o.Oid INNER JOIN room r ON r.DID = d.DID INNER JOIN roomtype t ON t.TID = r.TID WHERE o.OID = " + HttpContext.Session.GetInt32("Oid") + " AND d.DID = " + HttpContext.Session.GetInt32("isDid") + " AND r.RoomNumber = " + roomid + " ";
            mysqlcon.Open();
            MySqlCommand com2 = new MySqlCommand(getRoomtype);
            com2.Connection = mysqlcon;
            MySqlDataReader reader2 = com2.ExecuteReader();
            while (reader2.Read())
            {
                myrid = reader2.GetInt32(reader2.GetOrdinal("RID"));
                roomprice = reader2.GetInt32(reader2.GetOrdinal("Price"));
            }
            mysqlcon.Close();

            if (roomprice > 0)
            {
                var totalwater = meterwater - oldwater;
                var totalelec = meterelec - oldelec;
                var amountwater = totalwater * waterprice;
                var amountelec = totalelec * elecprice;
                var amount = amountwater + amountelec + roomprice;
                string query1 = "INSERT INTO `cal_info_room` (`RID`, `Water_meter`, `Elec_meter`, `Total_water_unit`, `Total_elec_unit`, `Total_water_amount`, `Total_elec_amount`, `Tatal_amount`, `Date`) " +
                    "VALUES ('" + myrid + "', '" + meterwater + "', '" + meterelec + "', '" + totalwater + "', '" + totalelec + "', '" + amountwater + "', '" + amountelec + "', '" + amount + "', '" + datemonth + "-01');";
                MySqlCommand com3 = new MySqlCommand(query1, mysqlcon);
                mysqlcon.Open();
                com3.ExecuteNonQuery();
                mysqlcon.Close();
                result = "OK";
            }
            else
            {
                result = "RoomTypeError";
            }
            return Json(result);
        }

        public IActionResult checkStatus(int status)
        {
            string result = "Fail";
            if (HttpContext.Session.GetInt32("Oid") == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                var context = new masdatabaseContext();
                if (status != 0)
                {
                    var checkroom = context.Room.Where(x => x.Rid == status).SingleOrDefault();
                    if (checkroom != null)
                    {
                        result = checkroom.Status;
                    }
                }
            }
            return Json(result);
        }

        public IActionResult RenterCheckMonth(int roomid)
        {
            string result = "Fail";
            string ConnectionStringMysql = "server=localhost;database=masdatabase;user=root;pwd=;sslmode=none";
            MySqlConnection mysqlcon = new MySqlConnection(ConnectionStringMysql);
            mysqlcon.Open();
            string startDate = "SELECT DATE_FORMAT(rt.Ren_StartDate,'%Y-%m') as StartDate FROM owner o INNER JOIN dorm d ON d.OID = o.Oid INNER JOIN room r ON r.DID = d.DID INNER JOIN renter rt ON rt.RID = r.RID " +
                "WHERE o.OID = " + HttpContext.Session.GetInt32("Oid") + " " +
                "AND d.DID = " + HttpContext.Session.GetInt32("isDid") + " " +
                "AND r.RoomNumber = " + roomid + " ";
            MySqlCommand com0 = new MySqlCommand(startDate);
            com0.Connection = mysqlcon;
            MySqlDataReader reader0 = com0.ExecuteReader();
            while (reader0.Read())
            {
                result = reader0["StartDate"].ToString();
            }

            return Json(result);
        }

        public IActionResult RenterEditDate(int RenId, string Date)
        {
            string result = "Fail";
            string ConnectionStringMysql = "server=localhost;database=masdatabase;user=root;pwd=;sslmode=none";
            MySqlConnection mysqlcon = new MySqlConnection(ConnectionStringMysql);

            mysqlcon.Open();
            string startDate = "UPDATE renter SET Ren_StartDate='" + Date + "-01' WHERE Ren_Id = " + RenId + " ";
            MySqlCommand com1 = new MySqlCommand(startDate);
            com1.Connection = mysqlcon;
            MySqlDataReader reader1 = com1.ExecuteReader();
            mysqlcon.Close();

            result = "Update";
            return Json(result);
        }

        public IActionResult RenterCheckbefore(string datemonth_old, int roomid)
        {
            string datemonthold = datemonth_old + "-01";
            string before = "No";
            string datemonth = "";
            DateTime dt_old = Convert.ToDateTime(datemonth_old);
            string ConnectionStringMysql = "server=localhost;database=masdatabase;user=root;pwd=;sslmode=none";
            MySqlConnection mysqlcon = new MySqlConnection(ConnectionStringMysql);
            mysqlcon.Open();
            string startDate = "SELECT DATE_FORMAT(rt.Ren_StartDate,'%Y-%m-%d') as StartDate FROM owner o INNER JOIN dorm d ON d.OID = o.Oid INNER JOIN room r ON r.DID = d.DID INNER JOIN renter rt ON rt.RID = r.RID " +
                "WHERE o.OID = " + HttpContext.Session.GetInt32("Oid") + " " +
                "AND d.DID = " + HttpContext.Session.GetInt32("isDid") + " " +
                "AND r.RoomNumber = " + roomid + " ";
            MySqlCommand com0 = new MySqlCommand(startDate);
            com0.Connection = mysqlcon;
            MySqlDataReader reader0 = com0.ExecuteReader();
            while (reader0.Read())
            {
                datemonth = reader0["StartDate"].ToString();
            }
            DateTime dt = Convert.ToDateTime(datemonth);
            mysqlcon.Close();
            if (dt_old < dt)
            {
                before = "NoDATA";
            }
            else
            {
                mysqlcon.Open();
                string f1 = "SELECT Count(*) FROM owner o INNER JOIN dorm d ON d.OID = o.Oid " +
                    "INNER JOIN room r ON r.DID = d.DID INNER JOIN cal_info_room c ON c.RID = r.RID" +
                    " WHERE o.OID = " + HttpContext.Session.GetInt32("Oid") + " " +
                    "AND d.DID = " + HttpContext.Session.GetInt32("isDid") + " " +
                    "AND r.RoomNumber = " + roomid + " " +
                    "AND r.Status = 1 " +
                    "AND c.Date = '" + datemonth_old + "-01' ";
                MySqlCommand com1 = new MySqlCommand(f1);
                com1.Connection = mysqlcon;
                MySqlDataReader reader1 = com1.ExecuteReader();
                int monthcount = 0;
                while (reader1.Read())
                {
                    monthcount = reader1.GetInt32(reader1.GetOrdinal("Count(*)"));
                }
                mysqlcon.Close();
                if (monthcount == 0)
                {
                    before = "No";
                }
                else
                {
                    before = "Yes";
                }
            }
            return Json(before);
        }
    }
}