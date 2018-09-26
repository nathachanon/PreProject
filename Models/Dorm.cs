﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MASdemo.Models
{
    public class Dorm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Room { get; set; }
        public int Floor { get; set; }
        public int setRates { get; set; }
        public int setWater { get; set; }
        public int setElec { get; set; }
        public int calRoom { get; set; }
        public string picture { get; set; }
        public string Add_no { get; set; }
        public string Street { get; set; }
        public string sub_District { get; set; }
        public string District { get; set; }
        public string Province { get; set; }
        public string Zip_code { get; set; }
    }
}
