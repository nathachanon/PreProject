using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MASdemo.Context
{
    public partial class Renter
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
        public DateTime DateTime { get; set; }
        public virtual Room R { get; set; }
    }
}
