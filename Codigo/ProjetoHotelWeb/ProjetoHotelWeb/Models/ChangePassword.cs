using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectHotelWeb.Models
{
    public class ChangePassword
    {

        [Required]
        public string OldPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }
        
        [Required]
        [Compare("NewPassword", ErrorMessage="As senhas não conferem")]
        public string ConfirmPassword { get; set; }
    }
}