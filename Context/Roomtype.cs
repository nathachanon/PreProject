using System;
using System.Collections.Generic;

namespace MASdemo.Context
{
    public partial class Roomtype
    {
        public Roomtype()
        {
            Room = new HashSet<Room>();
        }

        public int Tid { get; set; }
        public int Did { get; set; }
        public int Type { get; set; }
        public int Price { get; set; }

        public virtual Dorm D { get; set; }
        public virtual ICollection<Room> Room { get; set; }
    }
}
