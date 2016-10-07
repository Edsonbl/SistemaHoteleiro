using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectHotelWeb.Models
{
    public class Register
    {
        
        [Required]
        [Display(Name="Código de funcionário")]
        public int idFuncionario { get; set; }
        
        [Required]
        [Display(Name="Nome de usuário")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name="Senha de usuário")]
        [MinLength(6, ErrorMessage="A senha deve ter no minímo 6 caracteres")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name="Confirmação de senha")]
        [Compare("Password", ErrorMessage="As senhas não conferem")]
        public string ConfirmedPassword { get; set; }
        
        [Required]
        [Display(Name="Nível de acesso")]
        public char NivelDeAcesso { get; set; }
   
    }
}