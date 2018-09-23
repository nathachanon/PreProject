using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MASdemo.Models
{
    public class Manage
    {
        
    }

    public class AddDorm
    {
        [Required]
        [Display(Name = "d_name")]
        public string d_name { get; set; }

        [Required]
        [Display(Name = "setfloor")]
        public int setfloor { get; set; }

        [Required]
        [Display(Name = "setroom")]
        public int setroom { get; set; }

        [Required]
        [Display(Name = "setrates")]
        public int setrates { get; set; }

        [Display(Name = "setwater")]
        public int setwater { get; set; }

        [Display(Name = "setelec")]
        public int setelec { get; set; }

        [Display(Name = "add_no")]
        public string add_no { get; set; }

        [Display(Name = "street")]
        public string street { get; set; }

        [Display(Name = "zip_code")]
        public string zip_code { get; set; }

        [Display(Name = "district")]
        public string district { get; set; }

        [Display(Name = "sub_district")]
        public string sub_district { get; set; }

        [Display(Name = "province")]
        public string province { get; set; }

        [Display(Name = "picture")]
        public string picture { get; set; }

    }
}
