using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Classes
{
   public class TipoQuartoModel
    {
        public int idTipoQuarto { get; set; }
        public string descricao { get; set; }
        public string observacao { get; set; }
        public decimal valor { get; set; }

        public virtual List<QuartoModel> Quarto { get; set; }
        
    }
}
