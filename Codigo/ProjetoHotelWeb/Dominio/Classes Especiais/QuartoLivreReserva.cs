using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Classes_Especiais
{
    public class QuartosLivresReserva
    {
        public int idTipoQuarto { get; set; }
        public int idQuarto { get; set; }
        public int quantidade { get; set; }
        public string descricao { get; set; }
        public int capacidade { get; set; }
        public decimal valor { get; set; }
        public decimal valorTotal { get; set; }
        public DateTime? dataInicio { get; set; }
        public DateTime? dataFim { get; set; }
    }


}
