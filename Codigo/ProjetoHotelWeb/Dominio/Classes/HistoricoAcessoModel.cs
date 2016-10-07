using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Classes
{
   public class HistoricoAcessoModel
    {
        public int idHistoricoAcesso { get; set; }
        public int idUsuario { get; set; }
        public int idPacoteHospedagem { get; set; }        
        public string observacao { get; set; }
        public DateTime dataCadastro { get; set; }

        public virtual PessoaModel Usuario { get; set; }
        public virtual PacoteHospedagemModel PacoteHospedagem { get; set; }
    }
}
