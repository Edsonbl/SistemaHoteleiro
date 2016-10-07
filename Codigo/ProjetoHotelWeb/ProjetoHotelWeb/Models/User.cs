using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectHotelWeb.Models
{
    public class User : IdentityUser
    {
        public int idFuncionario { get; set; }
        public bool ativo { get; set; }
        public DateTime dataCadastro { get; set; } 
    }
}