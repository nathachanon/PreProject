using System;
using System.Collections.Generic;

namespace MASdemo.Context
{
    public partial class Dorm
    {
        public Dorm()
        {
            Room = new HashSet<Room>();
            Roomtype = new HashSet<Roomtype>();
            SetFloorRoom = new HashSet<SetFloorRoom>();
        }

        public int? Did { get; set; }
        public int? Oid { get; set; }
        public string Dname { get; set; }
        public int? SetWaterUnit { get; set; }
        public int? SetElecUnit { get; set; }
        public string Picture { get; set; }
        public string AddNo { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string District { get; set; }
        public string SubDistrict { get; set; }
        public string Province { get; set; }

        public virtual Owner U { get; set; }
        public virtual ICollection<Room> Room { get; set; }
        public virtual ICollection<Roomtype> Roomtype { get; set; }
        public virtual ICollection<SetFloorRoom> SetFloorRoom { get; set; }
    }
}
