using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MASdemo.Models
{
    public class Income
    {
        public string Month { get; set; }
        public string Year { get; set; }
        public string Amount { get; set; }

    }

    public class IncomeAll
    {
        public int Did { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string Amount { get; set; }
    }

    public class did
    {
        public int Did { get; set; }
        public string Name { get; set; }
    }


    public class isDid
    {
        public int Did { get; set; }
    }

    public class Extrapolate
    {
        public int Did { get; set; }
        public string DName { get; set; }
        public int Extrapolates { get; set; }
    }

    public class Excel
    {
        public int Row { get; set; }
        public int RoomNumber { get; set; }
        public string Type { get; set; }
        public int Price { get; set; }
        public int Water_meter { get; set; }
        public int Elec_meter { get; set; }
        public int Total_water_unit { get; set; }
        public int Total_elec_unit { get; set; }
        public int Total_water_amount { get; set; }
        public int Total_elec_amount { get; set; }
        public int Total_amount { get; set; }
        public string Date { get; set; }
    }

    public class RoomUpdate
    {
        public string Type { get; set; }
        public int Count { get; set; }
    }

    public class Unit
    {
        public int Water { get; set; }
        public int Elec { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
    }

    public class Last3Month
    {
        public string Month { get; set; }
        public string Year { get; set; }
        public int Income { get; set; }
    }
}