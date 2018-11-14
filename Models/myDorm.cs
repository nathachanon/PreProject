using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MASdemo.Models
{
    public class myDorm
    {
        public int? Did { get; set; }
        public string Name { get; set; }
        public int? setWater { get; set; }
        public int? setElec { get; set; }
        public string picture { get; set; }
        public string Add_no { get; set; }
        public string Street { get; set; }
        public string sub_District { get; set; }
        public string District { get; set; }
        public string Province { get; set; }
        public string Zip_code { get; set; }
    }
    public class myDorm2
    {
        public int Did { get; set; }
    }

    public class myRenter
    {
        public int RenId { get; set; }
        public int Rid { get; set; }
        public string RenName { get; set; }
        public string RenSurename { get; set; }
        public string RenTel { get; set; }
        public int? RenAge { get; set; }
        public string RenSsnPicture { get; set; }
        public int? StartWaterMeter { get; set; }
        public int? StartElecMeter { get; set; }
        public string RenAgreement { get; set; }

        public string DateTime { get; set; }

        public string DateMonth { get; set; }

    }
}
