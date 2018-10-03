using System;
using System.Collections.Generic;

namespace MASdemo.Context
{
    public partial class SetFloorRoom
    {
        public int Sid { get; set; }
        public int Did { get; set; }
        public int Floor { get; set; }
        public int Room { get; set; }

        public virtual Dorm D { get; set; }
    }
}
