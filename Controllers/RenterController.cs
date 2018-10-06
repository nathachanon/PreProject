using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MASdemo.Context;
using MASdemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace MASdemo.Controllers
{
    public class RenterController : Controller
    {
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
                    List<Renter> renters = new List<Renter>();
                    foreach (var x in renter)
                    {
                        renters.Add(new Renter()
                        {
                            Rid = x.Rid,
                            RenId = x.RenId,
                            RenName = x.RenName,
                            RenSurename = x.RenSurename,
                            RenAge = x.RenAge,
                            RenTel = x.RenTel
                        });
                        ViewBag.renters = renters;
                    }
                }
            }
            return View();
        }

        public IActionResult EditRenter(Renter renter, int did)
        {
            if (HttpContext.Session.GetInt32("Oid") == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                int mydid = did;
                var context = new masdatabaseContext();
                var editrenter = context.Renter.First(a => a.RenId == renter.RenId);
                editrenter.RenName = renter.RenName;
                editrenter.RenSurename = renter.RenSurename;
                editrenter.RenAge = renter.RenAge;
                editrenter.RenTel = renter.RenTel;
                context.SaveChanges();
                TempData["EditSuccessful"] = "<script>swal({type: 'success', title: 'แก้ไขข้อมูลผู้เช่าสำเร็จ', showConfirmButton: false,  timer: 1500,backdrop: 'rgba(0,0, 26,0.8)' })</script>";
                return RedirectToAction("ShowRoom", "Renter", new { did = mydid });
            }
        }
    }
}