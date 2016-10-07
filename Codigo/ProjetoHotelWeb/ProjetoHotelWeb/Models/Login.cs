using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectHotelWeb.Models
{
    public class Login
    {
        [Required]
        [Display(Name="Nome de usuário")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name="Senha")]
        public string Password { get; set; }
        
        [Display(Name="Lembrar de usuário?")]
        public bool RememberMe { get; set; }

    }
}