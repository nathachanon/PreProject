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
}