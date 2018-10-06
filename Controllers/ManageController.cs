using MASdemo.Context;
using MASdemo.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace MASdemo.Controllers
{
    public class ManageController : Controller
    {
        private readonly IHostingEnvironment he;
        public ManageController(IHostingEnvironment e)
        {
            he = e;
        }

        [HttpGet]
        public IActionResult Main()
        {
            ViewBag.myUid = HttpContext.Session.GetInt32("Oid");
            ViewBag.myLog = HttpContext.Session.GetString("Log");
            ViewBag.myName = HttpContext.Session.GetString("Name");
            ViewBag.mySurname = HttpContext.Session.GetString("Surname");
            ViewBag.myTel = HttpContext.Session.GetString("Tel");
            ViewBag.myEmail = HttpContext.Session.GetString("Email");
            if (HttpContext.Session.GetInt32("Oid") == null)
            {
                return RedirectToAction("Login", "User");
            }
            else if (HttpContext.Session.GetInt32("Oid") != null & HttpContext.Session.GetInt32("Oid") != 0)
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
                List<myDorm> dorms = new List<myDorm>();
                foreach (var t in sdorm)
                {
                    dorms.Add(new myDorm()
                    {
                        Did = t.Did,
                        Name = t.Dname,
                        picture = t.Picture,
                        setWater = t.SetWaterUnit,
                        setElec = t.SetElecUnit,
                        Add_no = t.AddNo,
                        Street = t.Street,
                        sub_District = t.SubDistrict,
                        District = t.District,
                        Province = t.Province,
                        Zip_code = t.ZipCode
                    });
                    ViewBag.dorms = dorms;
                }
            }
            return View();
        }





        [HttpGet]
        public IActionResult AddDorm()
        {
            ViewBag.myName = HttpContext.Session.GetString("Name");
            if (HttpContext.Session.GetInt32("Oid") == null)
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }




        [HttpGet]
        public IActionResult ManageDorm()
        {
            ViewBag.myName = HttpContext.Session.GetString("Name");
            if (HttpContext.Session.GetInt32("Oid") == null)
            {
                return RedirectToAction("Login", "User");
            }
            else if (HttpContext.Session.GetInt32("Oid") != null & HttpContext.Session.GetInt32("Oid") != 0)
            {
                var context = new masdatabaseContext();
                int count = 0;
                IQueryable<Dorm> sdorm = from q in context.Dorm where q.Oid == HttpContext.Session.GetInt32("Oid") select q;
                foreach (var t in sdorm)
                {
                    count = 1;
                }
                if (count == 0)
                {
                    return RedirectToAction("AddDorm", "Manage");
                }
                List<myDorm> dorms = new List<myDorm>();
                foreach (var t in sdorm)
                {
                    dorms.Add(new myDorm()
                    {
                        Did = t.Did,
                        Name = t.Dname,
                        picture = t.Picture,
                        setWater = t.SetWaterUnit,
                        setElec = t.SetElecUnit,
                        Add_no = t.AddNo,
                        Street = t.Street,
                        sub_District = t.SubDistrict,
                        District = t.District,
                        Province = t.Province,
                        Zip_code = t.ZipCode
                    });
                    ViewBag.dorms = dorms;
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult AddDorm(AddDorm ad, IFormFile picture)
        {
            if (HttpContext.Session.GetInt32("Oid") == null)
            {
                return RedirectToAction("Login", "User");
            }
            var fileName = "";
            if (picture != null)
            {
                var uploads = Path.Combine(he.WebRootPath, "uploads\\img_mansion");
                fileName = Guid.NewGuid().ToString().Substring(0, 10) + Path.GetExtension(picture.FileName);
                picture.CopyTo(new FileStream(Path.Combine(uploads, fileName), FileMode.Create));
            }
            var context = new masdatabaseContext();
            int? didcount = 0;
            int? realDid = 0;
            int? mydid = 0;
            IQueryable<Dorm> sdorm = from Dorm in context.Dorm where Dorm.Oid == HttpContext.Session.GetInt32("Oid") select Dorm;
            foreach (var Dorm in sdorm)
            {
                realDid = Dorm.Did;
                didcount = 1;
            }
            if (didcount == 1)
            {
                mydid = realDid + 1;
            }
            else
            {
                mydid = (1000 * HttpContext.Session.GetInt32("Oid")) + 1;
            }
            var addDorm = new Dorm
            {
                Did = mydid,
                Oid = HttpContext.Session.GetInt32("Oid"),
                Dname = ad.d_name,
                SetElecUnit = ad.setelec,
                SetWaterUnit = ad.setwater,
                AddNo = ad.add_no,
                Street = ad.street,
                ZipCode = ad.zip_code,
                District = ad.district,
                SubDistrict = ad.sub_district,
                Province = ad.province,
                Picture = fileName
            };
            context.Add(addDorm);
            context.SaveChanges();

            int floor = ad.floor;
            int did = mydid.Value;
            int floor1 = ad.floor1;
            int floor2 = ad.floor2;
            int floor3 = ad.floor3;
            int floor4 = ad.floor4;
            int floor5 = ad.floor5;
            int floor6 = ad.floor6;
            int floor7 = ad.floor7;
            int floor8 = ad.floor8;
            int floor9 = ad.floor9;
            int floor10 = ad.floor10;
            int room;


            if (floor == 1)
            {
                for (var i = 1; i <= floor1; i++)
                {
                    int rid = (100000 * did) + ((floor * 100) + i);

                    var addroom = new Room { Rid = rid, Did = did, RoomNumber = ((floor * 100) + i), Tid = 0 };
                    context.Add(addroom);
                    context.SaveChanges();

                    int renid = (did * 1000) + (floor * 10) + i;

                    var addrenter = new Renter { RenId = renid, Rid = rid };
                    context.Add(addrenter);
                    context.SaveChanges();
                }
                var setfloorroom = new SetFloorRoom
                {
                    Sid = (did * 10000) + (floor * 100) + floor1,
                    Did = did,
                    Floor = floor,
                    Room = floor1
                };
                context.Add(setfloorroom);
                context.SaveChanges();
            }
            else if (floor == 2)
            {
                for (var i = 1; i <= floor; i++)
                {
                    if (i == 1)
                    {
                        room = floor1;
                        for (var j = 1; j <= room; j++)
                        {
                            int rid = (100000 * did) + ((i * 100) + j);

                            var addroom = new Room { Rid = rid, Did = did, RoomNumber = ((i * 100) + j), Tid = 0 };
                            context.Add(addroom);
                            context.SaveChanges();

                            int renid = (did * 1000) + (i * 10) + j;

                            var addrenter = new Renter { RenId = renid, Rid = rid };
                            context.Add(addrenter);
                            context.SaveChanges();
                        }
                        var setfloorroom = new SetFloorRoom
                        {
                            Sid = (did * 10000) + (i * 100) + room,
                            Did = did,
                            Floor = i,
                            Room = room
                        };
                        context.Add(setfloorroom);
                        context.SaveChanges();
                    }
                    else if (i == 2)
                    {
                        room = floor2;
                        for (var j = 1; j <= room; j++)
                        {
                            int rid = (100000 * did) + ((i * 100) + j);

                            var addroom = new Room { Rid = rid, Did = did, RoomNumber = ((i * 100) + j), Tid = 0 };
                            context.Add(addroom);
                            context.SaveChanges();

                            int renid = (did * 1000) + (i * 10) + j;

                            var addrenter = new Renter { RenId = renid, Rid = rid };
                            context.Add(addrenter);
                            context.SaveChanges();
                        }
                        var setfloorroom = new SetFloorRoom
                        {
                            Sid = (did * 10000) + (i * 100) + room,
                            Did = did,
                            Floor = i,
                            Room = room
                        };
                        context.Add(setfloorroom);
                        context.SaveChanges();
                    }
                }
            }
            else if (floor == 3)
            {
                for (var i = 1; i <= floor; i++)
                {
                    if (i == 1)
                    {
                        room = floor1;
                        for (var j = 1; j <= room; j++)
                        {
                            int rid = (100000 * did) + ((i * 100) + j);

                            var addroom = new Room { Rid = rid, Did = did, RoomNumber = ((i * 100) + j), Tid = 0 };
                            context.Add(addroom);
                            context.SaveChanges();

                            int renid = (did * 1000) + (i * 10) + j;

                            var addrenter = new Renter { RenId = renid, Rid = rid };
                            context.Add(addrenter);
                            context.SaveChanges();
                        }
                        var setfloorroom = new SetFloorRoom
                        {
                            Sid = (did * 10000) + (i * 100) + room,
                            Did = did,
                            Floor = i,
                            Room = room
                        };
                        context.Add(setfloorroom);
                        context.SaveChanges();
                    }
                    else if (i == 2)
                    {
                        room = floor2;
                        for (var j = 1; j <= room; j++)
                        {
                            int rid = (100000 * did) + ((i * 100) + j);

                            var addroom = new Room { Rid = rid, Did = did, RoomNumber = ((i * 100) + j), Tid = 0 };
                            context.Add(addroom);
                            context.SaveChanges();

                            int renid = (did * 1000) + (i * 10) + j;

                            var addrenter = new Renter { RenId = renid, Rid = rid };
                            context.Add(addrenter);
                            context.SaveChanges();
                        }
                        var setfloorroom = new SetFloorRoom
                        {
                            Sid = (did * 10000) + (i * 100) + room,
                            Did = did,
                            Floor = i,
                            Room = room
                        };
                        context.Add(setfloorroom);
                        context.SaveChanges();
                    }
                    else if (i == 3)
                    {
                        room = floor3;
                        for (var j = 1; j <= room; j++)
                        {
                            int rid = (100000 * did) + ((i * 100) + j);

                            var addroom = new Room { Rid = rid, Did = did, RoomNumber = ((i * 100) + j), Tid = 0 };
                            context.Add(addroom);
                            context.SaveChanges();

                            int renid = (did * 1000) + (i * 10) + j;

                            var addrenter = new Renter { RenId = renid, Rid = rid };
                            context.Add(addrenter);
                            context.SaveChanges();
                        }
                        var setfloorroom = new SetFloorRoom
                        {
                            Sid = (did * 10000) + (i * 100) + room,
                            Did = did,
                            Floor = i,
                            Room = room
                        };
                        context.Add(setfloorroom);
                        context.SaveChanges();
                    }
                }
            }
            else if (floor == 4)
            {
                for (var i = 1; i <= floor; i++)
                {
                    if (i == 1)
                    {
                        room = floor1;
                        for (var j = 1; j <= room; j++)
                        {
                            int rid = (100000 * did) + ((i * 100) + j);

                            var addroom = new Room { Rid = rid, Did = did, RoomNumber = ((i * 100) + j), Tid = 0 };
                            context.Add(addroom);
                            context.SaveChanges();

                            int renid = (did * 1000) + (i * 10) + j;

                            var addrenter = new Renter { RenId = renid, Rid = rid };
                            context.Add(addrenter);
                            context.SaveChanges();
                        }
                        var setfloorroom = new SetFloorRoom
                        {
                            Sid = (did * 10000) + (i * 100) + room,
                            Did = did,
                            Floor = i,
                            Room = room
                        };
                        context.Add(setfloorroom);
                        context.SaveChanges();
                    }
                    else if (i == 2)
                    {
                        room = floor2;
                        for (var j = 1; j <= room; j++)
                        {
                            int rid = (100000 * did) + ((i * 100) + j);

                            var addroom = new Room { Rid = rid, Did = did, RoomNumber = ((i * 100) + j), Tid = 0 };
                            context.Add(addroom);
                            context.SaveChanges();

                            int renid = (did * 1000) + (i * 10) + j;

                            var addrenter = new Renter { RenId = renid, Rid = rid };
                            context.Add(addrenter);
                            context.SaveChanges();
                        }
                        var setfloorroom = new SetFloorRoom
                        {
                            Sid = (did * 10000) + (i * 100) + room,
                            Did = did,
                            Floor = i,
                            Room = room
                        };
                        context.Add(setfloorroom);
                        context.SaveChanges();
                    }
                    else if (i == 3)
                    {
                        room = floor3;
                        for (var j = 1; j <= room; j++)
                        {
                            int rid = (100000 * did) + ((i * 100) + j);

                            var addroom = new Room { Rid = rid, Did = did, RoomNumber = ((i * 100) + j), Tid = 0 };
                            context.Add(addroom);
                            context.SaveChanges();

                            int renid = (did * 1000) + (i * 10) + j;

                            var addrenter = new Renter { RenId = renid, Rid = rid };
                            context.Add(addrenter);
                            context.SaveChanges();
                        }
                        var setfloorroom = new SetFloorRoom
                        {
                            Sid = (did * 10000) + (i * 100) + room,
                            Did = did,
                            Floor = i,
                            Room = room
                        };
                        context.Add(setfloorroom);
                        context.SaveChanges();
                    }
                    else if (i == 4)
                    {
                        room = floor4;
                        for (var j = 1; j <= room; j++)
                        {
                            int rid = (100000 * did) + ((i * 100) + j);

                            var addroom = new Room { Rid = rid, Did = did, RoomNumber = ((i * 100) + j), Tid = 0 };
                            context.Add(addroom);
                            context.SaveChanges();

                            int renid = (did * 1000) + (i * 10) + j;

                            var addrenter = new Renter { RenId = renid, Rid = rid };
                            context.Add(addrenter);
                            context.SaveChanges();
                        }
                        var setfloorroom = new SetFloorRoom
                        {
                            Sid = (did * 10000) + (i * 100) + room,
                            Did = did,
                            Floor = i,
                            Room = room
                        };
                        context.Add(setfloorroom);
                        context.SaveChanges();
                    }
                }
            }
            else if (floor == 5)
            {
                for (var i = 1; i <= floor; i++)
                {
                    if (i == 1)
                    {
                        room = floor1;
                        for (var j = 1; j <= room; j++)
                        {
                            int rid = (100000 * did) + ((i * 100) + j);

                            var addroom = new Room { Rid = rid, Did = did, RoomNumber = ((i * 100) + j), Tid = 0 };
                            context.Add(addroom);
                            context.SaveChanges();

                            int renid = (did * 1000) + (i * 10) + j;

                            var addrenter = new Renter { RenId = renid, Rid = rid };
                            context.Add(addrenter);
                            context.SaveChanges();
                        }
                        var setfloorroom = new SetFloorRoom
                        {
                            Sid = (did * 10000) + (i * 100) + room,
                            Did = did,
                            Floor = i,
                            Room = room
                        };
                        context.Add(setfloorroom);
                        context.SaveChanges();
                    }
                    else if (i == 2)
                    {
                        room = floor2;
                        for (var j = 1; j <= room; j++)
                        {
                            int rid = (100000 * did) + ((i * 100) + j);

                            var addroom = new Room { Rid = rid, Did = did, RoomNumber = ((i * 100) + j), Tid = 0 };
                            context.Add(addroom);
                            context.SaveChanges();

                            int renid = (did * 1000) + (i * 10) + j;

                            var addrenter = new Renter { RenId = renid, Rid = rid };
                            context.Add(addrenter);
                            context.SaveChanges();
                        }
                        var setfloorroom = new SetFloorRoom
                        {
                            Sid = (did * 10000) + (i * 100) + room,
                            Did = did,
                            Floor = i,
                            Room = room
                        };
                        context.Add(setfloorroom);
                        context.SaveChanges();
                    }
                    else if (i == 3)
                    {
                        room = floor3;
                        for (var j = 1; j <= room; j++)
                        {
                            int rid = (100000 * did) + ((i * 100) + j);

                            var addroom = new Room { Rid = rid, Did = did, RoomNumber = ((i * 100) + j), Tid = 0 };
                            context.Add(addroom);
                            context.SaveChanges();

                            int renid = (did * 1000) + (i * 10) + j;

                            var addrenter = new Renter { RenId = renid, Rid = rid };
                            context.Add(addrenter);
                            context.SaveChanges();
                        }
                        var setfloorroom = new SetFloorRoom
                        {
                            Sid = (did * 10000) + (i * 100) + room,
                            Did = did,
                            Floor = i,
                            Room = room
                        };
                        context.Add(setfloorroom);
                        context.SaveChanges();
                    }
                    else if (i == 4)
                    {
                        room = floor4;
                        for (var j = 1; j <= room; j++)
                        {
                            int rid = (100000 * did) + ((i * 100) + j);

                            var addroom = new Room { Rid = rid, Did = did, RoomNumber = ((i * 100) + j), Tid = 0 };
                            context.Add(addroom);
                            context.SaveChanges();

                            int renid = (did * 1000) + (i * 10) + j;

                            var addrenter = new Renter { RenId = renid, Rid = rid };
                            context.Add(addrenter);
                            context.SaveChanges();
                        }
                        var setfloorroom = new SetFloorRoom
                        {
                            Sid = (did * 10000) + (i * 100) + room,
                            Did = did,
                            Floor = i,
                            Room = room
                        };
                        context.Add(setfloorroom);
                        context.SaveChanges();
                    }
                    else if (i == 5)
                    {
                        room = floor5;
                        for (var j = 1; j <= room; j++)
                        {
                            int rid = (100000 * did) + ((i * 100) + j);

                            var addroom = new Room { Rid = rid, Did = did, RoomNumber = ((i * 100) + j), Tid = 0 };
                            context.Add(addroom);
                            context.SaveChanges();

                            int renid = (did * 1000) + (i * 10) + j;

                            var addrenter = new Renter { RenId = renid, Rid = rid };
                            context.Add(addrenter);
                            context.SaveChanges();
                        }
                        var setfloorroom = new SetFloorRoom
                        {
                            Sid = (did * 10000) + (i * 100) + room,
                            Did = did,
                            Floor = i,
                            Room = room
                        };
                        context.Add(setfloorroom);
                        context.SaveChanges();
                    }
                }
            }
            else if (floor == 6)
            {
                for (var i = 1; i <= floor; i++)
                {
                    if (i == 1)
                    {
                        room = floor1;
                        for (var j = 1; j <= room; j++)
                        {
                            int rid = (100000 * did) + ((i * 100) + j);

                            var addroom = new Room { Rid = rid, Did = did, RoomNumber = ((i * 100) + j), Tid = 0 };
                            context.Add(addroom);
                            context.SaveChanges();

                            int renid = (did * 1000) + (i * 10) + j;

                            var addrenter = new Renter { RenId = renid, Rid = rid };
                            context.Add(addrenter);
                            context.SaveChanges();
                        }
                        var setfloorroom = new SetFloorRoom
                        {
                            Sid = (did * 10000) + (i * 100) + room,
                            Did = did,
                            Floor = i,
                            Room = room
                        };
                        context.Add(setfloorroom);
                        context.SaveChanges();
                    }
                    else if (i == 2)
                    {
                        room = floor2;
                        for (var j = 1; j <= room; j++)
                        {
                            int rid = (100000 * did) + ((i * 100) + j);

                            var addroom = new Room { Rid = rid, Did = did, RoomNumber = ((i * 100) + j), Tid = 0 };
                            context.Add(addroom);
                            context.SaveChanges();

                            int renid = (did * 1000) + (i * 10) + j;

                            var addrenter = new Renter { RenId = renid, Rid = rid };
                            context.Add(addrenter);
                            context.SaveChanges();
                        }
                        var setfloorroom = new SetFloorRoom
                        {
                            Sid = (did * 10000) + (i * 100) + room,
                            Did = did,
                            Floor = i,
                            Room = room
                        };
                        context.Add(setfloorroom);
                        context.SaveChanges();
                    }
                    else if (i == 3)
                    {
                        room = floor3;
                        for (var j = 1; j <= room; j++)
                        {
                            int rid = (100000 * did) + ((i * 100) + j);

                            var addroom = new Room { Rid = rid, Did = did, RoomNumber = ((i * 100) + j), Tid = 0 };
                            context.Add(addroom);
                            context.SaveChanges();

                            int renid = (did * 1000) + (i * 10) + j;

                            var addrenter = new Renter { RenId = renid, Rid = rid };
                            context.Add(addrenter);
                            context.SaveChanges();
                        }
                        var setfloorroom = new SetFloorRoom
                        {
                            Sid = (did * 10000) + (i * 100) + room,
                            Did = did,
                            Floor = i,
                            Room = room
                        };
                        context.Add(setfloorroom);
                        context.SaveChanges();
                    }
                    else if (i == 4)
                    {
                        room = floor4;
                        for (var j = 1; j <= room; j++)
                        {
                            int rid = (100000 * did) + ((i * 100) + j);

                            var addroom = new Room { Rid = rid, Did = did, RoomNumber = ((i * 100) + j), Tid = 0 };
                            context.Add(addroom);
                            context.SaveChanges();

                            int renid = (did * 1000) + (i * 10) + j;

                            var addrenter = new Renter { RenId = renid, Rid = rid };
                            context.Add(addrenter);
                            context.SaveChanges();
                        }
                        var setfloorroom = new SetFloorRoom
                        {
                            Sid = (did * 10000) + (i * 100) + room,
                            Did = did,
                            Floor = i,
                            Room = room
                        };
                        context.Add(setfloorroom);
                        context.SaveChanges();
                    }
                    else if (i == 5)
                    {
                        room = floor5;
                        for (var j = 1; j <= room; j++)
                        {
                            int rid = (100000 * did) + ((i * 100) + j);

                            var addroom = new Room { Rid = rid, Did = did, RoomNumber = ((i * 100) + j), Tid = 0 };
                            context.Add(addroom);
                            context.SaveChanges();

                            int renid = (did * 1000) + (i * 10) + j;

                            var addrenter = new Renter { RenId = renid, Rid = rid };
                            context.Add(addrenter);
                            context.SaveChanges();
                        }
                        var setfloorroom = new SetFloorRoom
                        {
                            Sid = (did * 10000) + (i * 100) + room,
                            Did = did,
                            Floor = i,
                            Room = room
                        };
                        context.Add(setfloorroom);
                        context.SaveChanges();
                    }
                    else if (i == 6)
                    {
                        room = floor6;
                        for (var j = 1; j <= room; j++)
                        {
                            int rid = (100000 * did) + ((i * 100) + j);

                            var addroom = new Room { Rid = rid, Did = did, RoomNumber = ((i * 100) + j), Tid = 0 };
                            context.Add(addroom);
                            context.SaveChanges();

                            int renid = (did * 1000) + (i * 10) + j;

                            var addrenter = new Renter { RenId = renid, Rid = rid };
                            context.Add(addrenter);
                            context.SaveChanges();
                        }
                        var setfloorroom = new SetFloorRoom
                        {
                            Sid = (did * 10000) + (i * 100) + room,
                            Did = did,
                            Floor = i,
                            Room = room
                        };
                        context.Add(setfloorroom);
                        context.SaveChanges();
                    }
                }
            }
            else if (floor == 7)
            {
                for (var i = 1; i <= floor; i++)
                {
                    if (i == 1)
                    {
                        room = floor1;
                        for (var j = 1; j <= room; j++)
                        {
                            int rid = (100000 * did) + ((i * 100) + j);

                            var addroom = new Room { Rid = rid, Did = did, RoomNumber = ((i * 100) + j), Tid = 0 };
                            context.Add(addroom);
                            context.SaveChanges();

                            int renid = (did * 1000) + (i * 10) + j;

                            var addrenter = new Renter { RenId = renid, Rid = rid };
                            context.Add(addrenter);
                            context.SaveChanges();
                        }
                        var setfloorroom = new SetFloorRoom
                        {
                            Sid = (did * 10000) + (i * 100) + room,
                            Did = did,
                            Floor = i,
                            Room = room
                        };
                        context.Add(setfloorroom);
                        context.SaveChanges();
                    }
                    else if (i == 2)
                    {
                        room = floor2;
                        for (var j = 1; j <= room; j++)
                        {
                            int rid = (100000 * did) + ((i * 100) + j);

                            var addroom = new Room { Rid = rid, Did = did, RoomNumber = ((i * 100) + j), Tid = 0 };
                            context.Add(addroom);
                            context.SaveChanges();

                            int renid = (did * 1000) + (i * 10) + j;

                            var addrenter = new Renter { RenId = renid, Rid = rid };
                            context.Add(addrenter);
                            context.SaveChanges();
                        }
                        var setfloorroom = new SetFloorRoom
                        {
                            Sid = (did * 10000) + (i * 100) + room,
                            Did = did,
                            Floor = i,
                            Room = room
                        };
                        context.Add(setfloorroom);
                        context.SaveChanges();
                    }
                    else if (i == 3)
                    {
                        room = floor3;
                        for (var j = 1; j <= room; j++)
                        {
                            int rid = (100000 * did) + ((i * 100) + j);

                            var addroom = new Room { Rid = rid, Did = did, RoomNumber = ((i * 100) + j), Tid = 0 };
                            context.Add(addroom);
                            context.SaveChanges();

                            int renid = (did * 1000) + (i * 10) + j;

                            var addrenter = new Renter { RenId = renid, Rid = rid };
                            context.Add(addrenter);
                            context.SaveChanges();
                        }
                        var setfloorroom = new SetFloorRoom
                        {
                            Sid = (did * 10000) + (i * 100) + room,
                            Did = did,
                            Floor = i,
                            Room = room
                        };
                        context.Add(setfloorroom);
                        context.SaveChanges();
                    }
                    else if (i == 4)
                    {
                        room = floor4;
                        for (var j = 1; j <= room; j++)
                        {
                            int rid = (100000 * did) + ((i * 100) + j);

                            var addroom = new Room { Rid = rid, Did = did, RoomNumber = ((i * 100) + j), Tid = 0 };
                            context.Add(addroom);
                            context.SaveChanges();

                            int renid = (did * 1000) + (i * 10) + j;

                            var addrenter = new Renter { RenId = renid, Rid = rid };
                            context.Add(addrenter);
                            context.SaveChanges();
                        }
                        var setfloorroom = new SetFloorRoom
                        {
                            Sid = (did * 10000) + (i * 100) + room,
                            Did = did,
                            Floor = i,
                            Room = room
                        };
                        context.Add(setfloorroom);
                        context.SaveChanges();
                    }
                    else if (i == 5)
                    {
                        room = floor5;
                        for (var j = 1; j <= room; j++)
                        {
                            int rid = (100000 * did) + ((i * 100) + j);

                            var addroom = new Room { Rid = rid, Did = did, RoomNumber = ((i * 100) + j), Tid = 0 };
                            context.Add(addroom);
                            context.SaveChanges();

                            int renid = (did * 1000) + (i * 10) + j;

                            var addrenter = new Renter { RenId = renid, Rid = rid };
                            context.Add(addrenter);
                            context.SaveChanges();
                        }
                        var setfloorroom = new SetFloorRoom
                        {
                            Sid = (did * 10000) + (i * 100) + room,
                            Did = did,
                            Floor = i,
                            Room = room
                        };
                        context.Add(setfloorroom);
                        context.SaveChanges();
                    }
                    else if (i == 6)
                    {
                        room = floor6;
                        for (var j = 1; j <= room; j++)
                        {
                            int rid = (100000 * did) + ((i * 100) + j);

                            var addroom = new Room { Rid = rid, Did = did, RoomNumber = ((i * 100) + j), Tid = 0 };
                            context.Add(addroom);
                            context.SaveChanges();

                            int renid = (did * 1000) + (i * 10) + j;

                            var addrenter = new Renter { RenId = renid, Rid = rid };
                            context.Add(addrenter);
                            context.SaveChanges();
                        }
                        var setfloorroom = new SetFloorRoom
                        {
                            Sid = (did * 10000) + (i * 100) + room,
                            Did = did,
                            Floor = i,
                            Room = room
                        };
                        context.Add(setfloorroom);
                        context.SaveChanges();
                    }
                    else if (i == 7)
                    {
                        room = floor7;
                        for (var j = 1; j <= room; j++)
                        {
                            int rid = (100000 * did) + ((i * 100) + j);

                            var addroom = new Room { Rid = rid, Did = did, RoomNumber = ((i * 100) + j), Tid = 0 };
                            context.Add(addroom);
                            context.SaveChanges();

                            int renid = (did * 1000) + (i * 10) + j;

                            var addrenter = new Renter { RenId = renid, Rid = rid };
                            context.Add(addrenter);
                            context.SaveChanges();
                        }
                        var setfloorroom = new SetFloorRoom
                        {
                            Sid = (did * 10000) + (i * 100) + room,
                            Did = did,
                            Floor = i,
                            Room = room
                        };
                        context.Add(setfloorroom);
                        context.SaveChanges();
                    }
                }
            }
            else if (floor == 8)
            {
                for (var i = 1; i <= floor; i++)
                {
                    if (i == 1)
                    {
                        room = floor1;
                        for (var j = 1; j <= room; j++)
                        {
                            int rid = (100000 * did) + ((i * 100) + j);

                            var addroom = new Room { Rid = rid, Did = did, RoomNumber = ((i * 100) + j), Tid = 0 };
                            context.Add(addroom);
                            context.SaveChanges();

                            int renid = (did * 1000) + (i * 10) + j;

                            var addrenter = new Renter { RenId = renid, Rid = rid };
                            context.Add(addrenter);
                            context.SaveChanges();
                        }
                        var setfloorroom = new SetFloorRoom
                        {
                            Sid = (did * 10000) + (i * 100) + room,
                            Did = did,
                            Floor = i,
                            Room = room
                        };
                        context.Add(setfloorroom);
                        context.SaveChanges();
                    }
                    else if (i == 2)
                    {
                        room = floor2;
                        for (var j = 1; j <= room; j++)
                        {
                            int rid = (100000 * did) + ((i * 100) + j);

                            var addroom = new Room { Rid = rid, Did = did, RoomNumber = ((i * 100) + j), Tid = 0 };
                            context.Add(addroom);
                            context.SaveChanges();

                            int renid = (did * 1000) + (i * 10) + j;

                            var addrenter = new Renter { RenId = renid, Rid = rid };
                            context.Add(addrenter);
                            context.SaveChanges();
                        }
                        var setfloorroom = new SetFloorRoom
                        {
                            Sid = (did * 10000) + (i * 100) + room,
                            Did = did,
                            Floor = i,
                            Room = room
                        };
                        context.Add(setfloorroom);
                        context.SaveChanges();
                    }
                    else if (i == 3)
                    {
                        room = floor3;
                        for (var j = 1; j <= room; j++)
                        {
                            int rid = (100000 * did) + ((i * 100) + j);

                            var addroom = new Room { Rid = rid, Did = did, RoomNumber = ((i * 100) + j), Tid = 0 };
                            context.Add(addroom);
                            context.SaveChanges();

                            int renid = (did * 1000) + (i * 10) + j;

                            var addrenter = new Renter { RenId = renid, Rid = rid };
                            context.Add(addrenter);
                            context.SaveChanges();
                        }
                        var setfloorroom = new SetFloorRoom
                        {
                            Sid = (did * 10000) + (i * 100) + room,
                            Did = did,
                            Floor = i,
                            Room = room
                        };
                        context.Add(setfloorroom);
                        context.SaveChanges();
                    }
                    else if (i == 4)
                    {
                        room = floor4;
                        for (var j = 1; j <= room; j++)
                        {
                            int rid = (100000 * did) + ((i * 100) + j);

                            var addroom = new Room { Rid = rid, Did = did, RoomNumber = ((i * 100) + j), Tid = 0 };
                            context.Add(addroom);
                            context.SaveChanges();

                            int renid = (did * 1000) + (i * 10) + j;

                            var addrenter = new Renter { RenId = renid, Rid = rid };
                            context.Add(addrenter);
                            context.SaveChanges();
                        }
                        var setfloorroom = new SetFloorRoom
                        {
                            Sid = (did * 10000) + (i * 100) + room,
                            Did = did,
                            Floor = i,
                            Room = room
                        };
                        context.Add(setfloorroom);
                        context.SaveChanges();
                    }
                    else if (i == 5)
                    {
                        room = floor5;
                        for (var j = 1; j <= room; j++)
                        {
                            int rid = (100000 * did) + ((i * 100) + j);

                            var addroom = new Room { Rid = rid, Did = did, RoomNumber = ((i * 100) + j), Tid = 0 };
                            context.Add(addroom);
                            context.SaveChanges();

                            int renid = (did * 1000) + (i * 10) + j;

                            var addrenter = new Renter { RenId = renid, Rid = rid };
                            context.Add(addrenter);
                            context.SaveChanges();
                        }
                        var setfloorroom = new SetFloorRoom
                        {
                            Sid = (did * 10000) + (i * 100) + room,
                            Did = did,
                            Floor = i,
                            Room = room
                        };
                        context.Add(setfloorroom);
                        context.SaveChanges();
                    }
                    else if (i == 6)
                    {
                        room = floor6;
                        for (var j = 1; j <= room; j++)
                        {
                            int rid = (100000 * did) + ((i * 100) + j);

                            var addroom = new Room { Rid = rid, Did = did, RoomNumber = ((i * 100) + j), Tid = 0 };
                            context.Add(addroom);
                            context.SaveChanges();

                            int renid = (did * 1000) + (i * 10) + j;

                            var addrenter = new Renter { RenId = renid, Rid = rid };
                            context.Add(addrenter);
                            context.SaveChanges();
                        }
                        var setfloorroom = new SetFloorRoom
                        {
                            Sid = (did * 10000) + (i * 100) + room,
                            Did = did,
                            Floor = i,
                            Room = room
                        };
                        context.Add(setfloorroom);
                        context.SaveChanges();
                    }
                    else if (i == 7)
                    {
                        room = floor7;
                        for (var j = 1; j <= room; j++)
                        {
                            int rid = (100000 * did) + ((i * 100) + j);

                            var addroom = new Room { Rid = rid, Did = did, RoomNumber = ((i * 100) + j), Tid = 0 };
                            context.Add(addroom);
                            context.SaveChanges();

                            int renid = (did * 1000) + (i * 10) + j;

                            var addrenter = new Renter { RenId = renid, Rid = rid };
                            context.Add(addrenter);
                            context.SaveChanges();
                        }
                        var setfloorroom = new SetFloorRoom
                        {
                            Sid = (did * 10000) + (i * 100) + room,
                            Did = did,
                            Floor = i,
                            Room = room
                        };
                        context.Add(setfloorroom);
                        context.SaveChanges();
                    }
                    else if (i == 8)
                    {
                        room = floor8;
                        for (var j = 1; j <= room; j++)
                        {
                            int rid = (100000 * did) + ((i * 100) + j);

                            var addroom = new Room { Rid = rid, Did = did, RoomNumber = ((i * 100) + j), Tid = 0 };
                            context.Add(addroom);
                            context.SaveChanges();

                            int renid = (did * 1000) + (i * 10) + j;

                            var addrenter = new Renter { RenId = renid, Rid = rid };
                            context.Add(addrenter);
                            context.SaveChanges();
                        }
                        var setfloorroom = new SetFloorRoom
                        {
                            Sid = (did * 10000) + (i * 100) + room,
                            Did = did,
                            Floor = i,
                            Room = room
                        };
                        context.Add(setfloorroom);
                        context.SaveChanges();
                    }
                }
            }
            else if (floor == 9)
            {
                for (var i = 1; i <= floor; i++)
                {
                    if (i == 1)
                    {
                        room = floor1;
                        for (var j = 1; j <= room; j++)
                        {
                            int rid = (100000 * did) + ((i * 100) + j);

                            var addroom = new Room { Rid = rid, Did = did, RoomNumber = ((i * 100) + j), Tid = 0 };
                            context.Add(addroom);
                            context.SaveChanges();

                            int renid = (did * 1000) + (i * 10) + j;

                            var addrenter = new Renter { RenId = renid, Rid = rid };
                            context.Add(addrenter);
                            context.SaveChanges();
                        }
                        var setfloorroom = new SetFloorRoom
                        {
                            Sid = (did * 10000) + (i * 100) + room,
                            Did = did,
                            Floor = i,
                            Room = room
                        };
                        context.Add(setfloorroom);
                        context.SaveChanges();
                    }
                    else if (i == 2)
                    {
                        room = floor2;
                        for (var j = 1; j <= room; j++)
                        {
                            int rid = (100000 * did) + ((i * 100) + j);

                            var addroom = new Room { Rid = rid, Did = did, RoomNumber = ((i * 100) + j), Tid = 0 };
                            context.Add(addroom);
                            context.SaveChanges();

                            int renid = (did * 1000) + (i * 10) + j;

                            var addrenter = new Renter { RenId = renid, Rid = rid };
                            context.Add(addrenter);
                            context.SaveChanges();
                        }
                        var setfloorroom = new SetFloorRoom
                        {
                            Sid = (did * 10000) + (i * 100) + room,
                            Did = did,
                            Floor = i,
                            Room = room
                        };
                        context.Add(setfloorroom);
                        context.SaveChanges();
                    }
                    else if (i == 3)
                    {
                        room = floor3;
                        for (var j = 1; j <= room; j++)
                        {
                            int rid = (100000 * did) + ((i * 100) + j);

                            var addroom = new Room { Rid = rid, Did = did, RoomNumber = ((i * 100) + j), Tid = 0 };
                            context.Add(addroom);
                            context.SaveChanges();

                            int renid = (did * 1000) + (i * 10) + j;

                            var addrenter = new Renter { RenId = renid, Rid = rid };
                            context.Add(addrenter);
                            context.SaveChanges();
                        }
                        var setfloorroom = new SetFloorRoom
                        {
                            Sid = (did * 10000) + (i * 100) + room,
                            Did = did,
                            Floor = i,
                            Room = room
                        };
                        context.Add(setfloorroom);
                        context.SaveChanges();
                    }
                    else if (i == 4)
                    {
                        room = floor4;
                        for (var j = 1; j <= room; j++)
                        {
                            int rid = (100000 * did) + ((i * 100) + j);

                            var addroom = new Room { Rid = rid, Did = did, RoomNumber = ((i * 100) + j), Tid = 0 };
                            context.Add(addroom);
                            context.SaveChanges();

                            int renid = (did * 1000) + (i * 10) + j;

                            var addrenter = new Renter { RenId = renid, Rid = rid };
                            context.Add(addrenter);
                            context.SaveChanges();
                        }
                        var setfloorroom = new SetFloorRoom
                        {
                            Sid = (did * 10000) + (i * 100) + room,
                            Did = did,
                            Floor = i,
                            Room = room
                        };
                        context.Add(setfloorroom);
                        context.SaveChanges();
                    }
                    else if (i == 5)
                    {
                        room = floor5;
                        for (var j = 1; j <= room; j++)
                        {
                            int rid = (100000 * did) + ((i * 100) + j);

                            var addroom = new Room { Rid = rid, Did = did, RoomNumber = ((i * 100) + j), Tid = 0 };
                            context.Add(addroom);
                            context.SaveChanges();

                            int renid = (did * 1000) + (i * 10) + j;

                            var addrenter = new Renter { RenId = renid, Rid = rid };
                            context.Add(addrenter);
                            context.SaveChanges();
                        }
                        var setfloorroom = new SetFloorRoom
                        {
                            Sid = (did * 10000) + (i * 100) + room,
                            Did = did,
                            Floor = i,
                            Room = room
                        };
                        context.Add(setfloorroom);
                        context.SaveChanges();
                    }
                    else if (i == 6)
                    {
                        room = floor6;
                        for (var j = 1; j <= room; j++)
                        {
                            int rid = (100000 * did) + ((i * 100) + j);

                            var addroom = new Room { Rid = rid, Did = did, RoomNumber = ((i * 100) + j), Tid = 0 };
                            context.Add(addroom);
                            context.SaveChanges();

                            int renid = (did * 1000) + (i * 10) + j;

                            var addrenter = new Renter { RenId = renid, Rid = rid };
                            context.Add(addrenter);
                            context.SaveChanges();
                        }
                        var setfloorroom = new SetFloorRoom
                        {
                            Sid = (did * 10000) + (i * 100) + room,
                            Did = did,
                            Floor = i,
                            Room = room
                        };
                        context.Add(setfloorroom);
                        context.SaveChanges();
                    }
                    else if (i == 7)
                    {
                        room = floor7;
                        for (var j = 1; j <= room; j++)
                        {
                            int rid = (100000 * did) + ((i * 100) + j);

                            var addroom = new Room { Rid = rid, Did = did, RoomNumber = ((i * 100) + j), Tid = 0 };
                            context.Add(addroom);
                            context.SaveChanges();

                            int renid = (did * 1000) + (i * 10) + j;

                            var addrenter = new Renter { RenId = renid, Rid = rid };
                            context.Add(addrenter);
                            context.SaveChanges();
                        }
                        var setfloorroom = new SetFloorRoom
                        {
                            Sid = (did * 10000) + (i * 100) + room,
                            Did = did,
                            Floor = i,
                            Room = room
                        };
                        context.Add(setfloorroom);
                        context.SaveChanges();
                    }
                    else if (i == 8)
                    {
                        room = floor8;
                        for (var j = 1; j <= room; j++)
                        {
                            int rid = (100000 * did) + ((i * 100) + j);

                            var addroom = new Room { Rid = rid, Did = did, RoomNumber = ((i * 100) + j), Tid = 0 };
                            context.Add(addroom);
                            context.SaveChanges();

                            int renid = (did * 1000) + (i * 10) + j;

                            var addrenter = new Renter { RenId = renid, Rid = rid };
                            context.Add(addrenter);
                            context.SaveChanges();
                        }
                        var setfloorroom = new SetFloorRoom
                        {
                            Sid = (did * 10000) + (i * 100) + room,
                            Did = did,
                            Floor = i,
                            Room = room
                        };
                        context.Add(setfloorroom);
                        context.SaveChanges();
                    }
                    else if (i == 9)
                    {
                        room = floor9;
                        for (var j = 1; j <= room; j++)
                        {
                            int rid = (100000 * did) + ((i * 100) + j);

                            var addroom = new Room { Rid = rid, Did = did, RoomNumber = ((i * 100) + j), Tid = 0 };
                            context.Add(addroom);
                            context.SaveChanges();

                            int renid = (did * 1000) + (i * 10) + j;

                            var addrenter = new Renter { RenId = renid, Rid = rid };
                            context.Add(addrenter);
                            context.SaveChanges();
                        }
                        var setfloorroom = new SetFloorRoom
                        {
                            Sid = (did * 10000) + (i * 100) + room,
                            Did = did,
                            Floor = i,
                            Room = room
                        };
                        context.Add(setfloorroom);
                        context.SaveChanges();
                    }
                }
            }
            else if (floor == 10)
            {
                for (var i = 1; i <= floor; i++)
                {
                    if (i == 1)
                    {
                        room = floor1;
                        for (var j = 1; j <= room; j++)
                        {
                            int rid = (100000 * did) + ((i * 100) + j);

                            var addroom = new Room { Rid = rid, Did = did, RoomNumber = ((i * 100) + j), Tid = 0 };
                            context.Add(addroom);
                            context.SaveChanges();

                            int renid = (did * 1000) + (i * 10) + j;

                            var addrenter = new Renter { RenId = renid, Rid = rid };
                            context.Add(addrenter);
                            context.SaveChanges();
                        }
                        var setfloorroom = new SetFloorRoom
                        {
                            Sid = (did * 10000) + (i * 100) + room,
                            Did = did,
                            Floor = i,
                            Room = room
                        };
                        context.Add(setfloorroom);
                        context.SaveChanges();
                    }
                    else if (i == 2)
                    {
                        room = floor2;
                        for (var j = 1; j <= room; j++)
                        {
                            int rid = (100000 * did) + ((i * 100) + j);

                            var addroom = new Room { Rid = rid, Did = did, RoomNumber = ((i * 100) + j), Tid = 0 };
                            context.Add(addroom);
                            context.SaveChanges();

                            int renid = (did * 1000) + (i * 10) + j;

                            var addrenter = new Renter { RenId = renid, Rid = rid };
                            context.Add(addrenter);
                            context.SaveChanges();
                        }
                        var setfloorroom = new SetFloorRoom
                        {
                            Sid = (did * 10000) + (i * 100) + room,
                            Did = did,
                            Floor = i,
                            Room = room
                        };
                        context.Add(setfloorroom);
                        context.SaveChanges();
                    }
                    else if (i == 3)
                    {
                        room = floor3;
                        for (var j = 1; j <= room; j++)
                        {
                            int rid = (100000 * did) + ((i * 100) + j);

                            var addroom = new Room { Rid = rid, Did = did, RoomNumber = ((i * 100) + j), Tid = 0 };
                            context.Add(addroom);
                            context.SaveChanges();

                            int renid = (did * 1000) + (i * 10) + j;

                            var addrenter = new Renter { RenId = renid, Rid = rid };
                            context.Add(addrenter);
                            context.SaveChanges();
                        }
                        var setfloorroom = new SetFloorRoom
                        {
                            Sid = (did * 10000) + (i * 100) + room,
                            Did = did,
                            Floor = i,
                            Room = room
                        };
                        context.Add(setfloorroom);
                        context.SaveChanges();
                    }
                    else if (i == 4)
                    {
                        room = floor4;
                        for (var j = 1; j <= room; j++)
                        {
                            int rid = (100000 * did) + ((i * 100) + j);

                            var addroom = new Room { Rid = rid, Did = did, RoomNumber = ((i * 100) + j), Tid = 0 };
                            context.Add(addroom);
                            context.SaveChanges();

                            int renid = (did * 1000) + (i * 10) + j;

                            var addrenter = new Renter { RenId = renid, Rid = rid };
                            context.Add(addrenter);
                            context.SaveChanges();
                        }
                        var setfloorroom = new SetFloorRoom
                        {
                            Sid = (did * 10000) + (i * 100) + room,
                            Did = did,
                            Floor = i,
                            Room = room
                        };
                        context.Add(setfloorroom);
                        context.SaveChanges();
                    }
                    else if (i == 5)
                    {
                        room = floor5;
                        for (var j = 1; j <= room; j++)
                        {
                            int rid = (100000 * did) + ((i * 100) + j);

                            var addroom = new Room { Rid = rid, Did = did, RoomNumber = ((i * 100) + j), Tid = 0 };
                            context.Add(addroom);
                            context.SaveChanges();

                            int renid = (did * 1000) + (i * 10) + j;

                            var addrenter = new Renter { RenId = renid, Rid = rid };
                            context.Add(addrenter);
                            context.SaveChanges();
                        }
                        var setfloorroom = new SetFloorRoom
                        {
                            Sid = (did * 10000) + (i * 100) + room,
                            Did = did,
                            Floor = i,
                            Room = room
                        };
                        context.Add(setfloorroom);
                        context.SaveChanges();
                    }
                    else if (i == 6)
                    {
                        room = floor6;
                        for (var j = 1; j <= room; j++)
                        {
                            int rid = (100000 * did) + ((i * 100) + j);

                            var addroom = new Room { Rid = rid, Did = did, RoomNumber = ((i * 100) + j), Tid = 0 };
                            context.Add(addroom);
                            context.SaveChanges();

                            int renid = (did * 1000) + (i * 10) + j;

                            var addrenter = new Renter { RenId = renid, Rid = rid };
                            context.Add(addrenter);
                            context.SaveChanges();
                        }
                        var setfloorroom = new SetFloorRoom
                        {
                            Sid = (did * 10000) + (i * 100) + room,
                            Did = did,
                            Floor = i,
                            Room = room
                        };
                        context.Add(setfloorroom);
                        context.SaveChanges();
                    }
                    else if (i == 7)
                    {
                        room = floor7;
                        for (var j = 1; j <= room; j++)
                        {
                            int rid = (100000 * did) + ((i * 100) + j);

                            var addroom = new Room { Rid = rid, Did = did, RoomNumber = ((i * 100) + j), Tid = 0 };
                            context.Add(addroom);
                            context.SaveChanges();

                            int renid = (did * 1000) + (i * 10) + j;

                            var addrenter = new Renter { RenId = renid, Rid = rid };
                            context.Add(addrenter);
                            context.SaveChanges();
                        }
                        var setfloorroom = new SetFloorRoom
                        {
                            Sid = (did * 10000) + (i * 100) + room,
                            Did = did,
                            Floor = i,
                            Room = room
                        };
                        context.Add(setfloorroom);
                        context.SaveChanges();
                    }
                    else if (i == 8)
                    {
                        room = floor8;
                        for (var j = 1; j <= room; j++)
                        {
                            int rid = (100000 * did) + ((i * 100) + j);

                            var addroom = new Room { Rid = rid, Did = did, RoomNumber = ((i * 100) + j), Tid = 0 };
                            context.Add(addroom);
                            context.SaveChanges();

                            int renid = (did * 1000) + (i * 10) + j;

                            var addrenter = new Renter { RenId = renid, Rid = rid };
                            context.Add(addrenter);
                            context.SaveChanges();
                        }
                        var setfloorroom = new SetFloorRoom
                        {
                            Sid = (did * 10000) + (i * 100) + room,
                            Did = did,
                            Floor = i,
                            Room = room
                        };
                        context.Add(setfloorroom);
                        context.SaveChanges();
                    }
                    else if (i == 9)
                    {
                        room = floor9;
                        for (var j = 1; j <= room; j++)
                        {
                            int rid = (100000 * did) + ((i * 100) + j);

                            var addroom = new Room { Rid = rid, Did = did, RoomNumber = ((i * 100) + j), Tid = 0 };
                            context.Add(addroom);
                            context.SaveChanges();

                            int renid = (did * 1000) + (i * 10) + j;

                            var addrenter = new Renter { RenId = renid, Rid = rid };
                            context.Add(addrenter);
                            context.SaveChanges();
                        }
                        var setfloorroom = new SetFloorRoom
                        {
                            Sid = (did * 10000) + (i * 100) + room,
                            Did = did,
                            Floor = i,
                            Room = room
                        };
                        context.Add(setfloorroom);
                        context.SaveChanges();
                    }
                    else if (i == 10)
                    {
                        room = floor10;
                        for (var j = 1; j <= room; j++)
                        {
                            int rid = (100000 * did) + ((i * 100) + j);

                            var addroom = new Room { Rid = rid, Did = did, RoomNumber = ((i * 100) + j), Tid = 0 };
                            context.Add(addroom);
                            context.SaveChanges();

                            int renid = (did * 1000) + (i * 10) + j;

                            var addrenter = new Renter { RenId = renid, Rid = rid };
                            context.Add(addrenter);
                            context.SaveChanges();
                        }
                        var setfloorroom = new SetFloorRoom
                        {
                            Sid = (did * 10000) + (i * 100) + room,
                            Did = did,
                            Floor = i,
                            Room = room
                        };
                        context.Add(setfloorroom);
                        context.SaveChanges();
                    }
                }
            }

            TempData["AddSuccessful"] = "<script>swal({type: 'success', title: 'เพิ่มหอพักสำเร็จ', showConfirmButton: false,  timer: 1500,backdrop: 'rgba(0,0, 26,0.8)' })</script>";
            return RedirectToAction("ManageDorm", "Manage");
        }

        public IActionResult DeleteDorm(int Id_dorm)
        {
            if (HttpContext.Session.GetInt32("Oid") == null)
            {
                return RedirectToAction("Login", "User");
            }
            MySqlConnection mysqlconnect = new MySqlConnection("Server = localhost; User Id = root; Password=; Database=masdatabase; SslMode=none; CharacterSet=utf8;");

            string query = "DELETE FROM `renter` WHERE Ren_Id like @instemail ";
            string query2 = "DELETE FROM `room` WHERE did = '" + Id_dorm + "' ";
            string query3 = "DELETE FROM `set_floor_room` WHERE did = '" + Id_dorm + "' ";
            string query4 = "DELETE FROM `dorm` WHERE did = '" + Id_dorm + "' and oid = " + HttpContext.Session.GetInt32("Oid") + " ";

            mysqlconnect.Open();
            MySqlCommand comm = new MySqlCommand(query);
            comm.Parameters.AddWithValue("@instemail", "%" + Id_dorm + "%");
            comm.Connection = mysqlconnect;
            MySqlDataReader reader = comm.ExecuteReader();
            mysqlconnect.Close();

            mysqlconnect.Open();
            MySqlCommand comm2 = new MySqlCommand(query2);
            comm2.Connection = mysqlconnect;
            MySqlDataReader reader2 = comm2.ExecuteReader();
            mysqlconnect.Close();

            mysqlconnect.Open();
            MySqlCommand comm3 = new MySqlCommand(query3);
            comm3.Connection = mysqlconnect;
            MySqlDataReader reader3 = comm3.ExecuteReader();
            mysqlconnect.Close();

            mysqlconnect.Open();
            MySqlCommand comm4 = new MySqlCommand(query4);
            comm4.Connection = mysqlconnect;
            MySqlDataReader reader4 = comm4.ExecuteReader();
            mysqlconnect.Close();

            return RedirectToAction("ManageDorm", "Manage");
        }

        public IActionResult EditDorm(AddDorm ad, IFormFile picture, int Id_dorm)
        {
            if (HttpContext.Session.GetInt32("Oid") == null)
            {
                return RedirectToAction("Login", "User");
            }
            var context = new masdatabaseContext();

            if (picture != null)
            {
                var fileName = "";
                var uploads = Path.Combine(he.WebRootPath, "uploads\\img_mansion");
                fileName = Guid.NewGuid().ToString().Substring(0, 10) + Path.GetExtension(picture.FileName);
                picture.CopyTo(new FileStream(Path.Combine(uploads, fileName), FileMode.Create));

                if (ad.picture != null)
                {
                    var editDorm = context.Dorm.First(a => a.Did == Id_dorm);
                    editDorm.Dname = ad.d_name;
                    editDorm.SetElecUnit = ad.setelec;
                    editDorm.SetWaterUnit = ad.setwater;
                    editDorm.AddNo = ad.add_no;
                    editDorm.Street = ad.street;
                    editDorm.ZipCode = ad.zip_code;
                    editDorm.District = ad.district;
                    editDorm.SubDistrict = ad.sub_district;
                    editDorm.Province = ad.province;
                    context.SaveChanges();
                    TempData["EditSuccessful"] = "<script>swal({type: 'success', title: 'แก้ไขข้อมูลสำเร็จ', showConfirmButton: false,  timer: 1500,backdrop: 'rgba(0,0, 26,0.8)' })</script>";
                    return RedirectToAction("ManageDorm", "Manage");
                }
                else
                {
                    var editDorm = context.Dorm.First(a => a.Did == Id_dorm);
                    editDorm.Dname = ad.d_name;
                    editDorm.SetElecUnit = ad.setelec;
                    editDorm.SetWaterUnit = ad.setwater;
                    editDorm.AddNo = ad.add_no;
                    editDorm.Street = ad.street;
                    editDorm.ZipCode = ad.zip_code;
                    editDorm.District = ad.district;
                    editDorm.SubDistrict = ad.sub_district;
                    editDorm.Province = ad.province;
                    editDorm.Picture = fileName;
                    context.SaveChanges();
                    TempData["EditSuccessful"] = "<script>swal({type: 'success', title: 'แก้ไขข้อมูลสำเร็จ', showConfirmButton: false,  timer: 1500,backdrop: 'rgba(0,0, 26,0.8)' })</script>";
                    return RedirectToAction("ManageDorm", "Manage");
                }
            }
            else
            {
                if (ad.picture != null)
                {
                    var editDorm = context.Dorm.First(a => a.Did == Id_dorm);
                    editDorm.Dname = ad.d_name;
                    editDorm.SetElecUnit = ad.setelec;
                    editDorm.SetWaterUnit = ad.setwater;
                    editDorm.AddNo = ad.add_no;
                    editDorm.Street = ad.street;
                    editDorm.ZipCode = ad.zip_code;
                    editDorm.District = ad.district;
                    editDorm.SubDistrict = ad.sub_district;
                    editDorm.Province = ad.province;
                    context.SaveChanges();
                    TempData["EditSuccessful"] = "<script>swal({type: 'success', title: 'แก้ไขข้อมูลสำเร็จ', showConfirmButton: false,  timer: 1500,backdrop: 'rgba(0,0, 26,0.8)' })</script>";
                    return RedirectToAction("ManageDorm", "Manage");
                }
                else
                {
                    var editDorm = context.Dorm.First(a => a.Did == Id_dorm);
                    editDorm.Dname = ad.d_name;
                    editDorm.SetElecUnit = ad.setelec;
                    editDorm.SetWaterUnit = ad.setwater;
                    editDorm.AddNo = ad.add_no;
                    editDorm.Street = ad.street;
                    editDorm.ZipCode = ad.zip_code;
                    editDorm.District = ad.district;
                    editDorm.SubDistrict = ad.sub_district;
                    editDorm.Province = ad.province;
                    context.SaveChanges();
                    TempData["EditSuccessful"] = "<script>swal({type: 'success', title: 'แก้ไขข้อมูลสำเร็จ', showConfirmButton: false,  timer: 1500,backdrop: 'rgba(0,0, 26,0.8)' })</script>";
                    return RedirectToAction("ManageDorm", "Manage");
                }
            }
        }
    }
}