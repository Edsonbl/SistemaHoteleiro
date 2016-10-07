using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Classes
{
   public class HistoricoModel
    {
       public int idHistorico { get; set; }
       public int idUsuario { get; set; }
       public string descricao { get; set; }
       public string observacao { get; set; }
       public DateTime dataCadastro { get; set; }

       public virtual PessoaModel Usuario { get; set; }

    }
}
