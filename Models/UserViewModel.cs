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
}
