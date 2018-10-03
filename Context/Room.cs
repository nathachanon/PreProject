using System;
using System.Collections.Generic;

namespace MASdemo.Context
{
    public partial class Room
    {
        public Room()
        {
            CalInfoRoom = new HashSet<CalInfoRoom>();
            Renter = new HashSet<Renter>();
        }

        public int Rid { get; set; }
        public int Did { get; set; }
        public int Tid { get; set; }
        public int RoomNumber { get; set; }
        public string Status { get; set; }

        public virtual Dorm D { get; set; }
        public virtual Roomtype T { get; set; }
        public virtual ICollection<CalInfoRoom> CalInfoRoom { get; set; }
        public virtual ICollection<Renter> Renter { get; set; }
    }
}
