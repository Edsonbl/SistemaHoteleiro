using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Classes
{
   public class QuartoModel
    {
       public int idQuarto { get; set; }
       public int idTipoQuarto { get; set; }
       public string descricao { get; set; }
       public string observacao { get; set; }
       public int capacidade { get; set; }
       public bool ativo { get; set; }
       public bool reservado { get; set; }
       public string status { get; set; }

       

       public virtual List<HospedagemModel> Hospedagem { get; set; }
       public virtual TipoQuartoModel TipoQuarto { get; set; }
    }
}
