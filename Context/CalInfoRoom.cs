using System;
using System.Collections.Generic;

namespace MASdemo.Context
{
    public partial class CalInfoRoom
    {
        public int CalId { get; set; }
        public int Rid { get; set; }
        public int WaterMeter { get; set; }
        public int ElecMeter { get; set; }
        public int TotalWaterUnit { get; set; }
        public int TotalElecUnit { get; set; }
        public int TotalWaterAmount { get; set; }
        public int TotalElecAmount { get; set; }
        public int TatalAmount { get; set; }
        public DateTime DateTime { get; set; }

        public virtual Room R { get; set; }
    }
}
