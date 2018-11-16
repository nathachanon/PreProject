using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MASdemo.Models
{
    public class UserViewModel
    {

    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "email")]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "password")]
        public string password { get; set; }

    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        [Display(Name = "tel")]
        public string tel { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "password")]
        public string password { get; set; }

        [Required]
        [Display(Name = "name")]
        public string name { get; set; }

        [Required]
        [Display(Name = "surname")]
        public string surname { get; set; }

    }

    public class Report 
    { 
        public string Report_id { get; set; } 
        public string Report_message { get; set; } 
        public int Report_status { get; set; } 
        public string Report_datetime { get; set; } 
    }

    public class ReportList 
    { 
        public string Report_id2 { get; set; } 
        public int Report_id { get; set; } 
        public string Report_message { get; set; } 
        public string Report_datetime { get; set; } 
        public string Report_Owner { get; set; } 
    }

    public class WorkAll 
    { 
        public string Work_id { get; set; } 
        public int Report_id { get; set; } 
        public string Report_message { get; set; } 
        public string Report_datetime { get; set; } 
        public string Report_Owner { get; set; } 
    }
}
