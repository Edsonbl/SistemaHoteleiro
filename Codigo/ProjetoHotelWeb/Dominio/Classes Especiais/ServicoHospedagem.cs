using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Classes_Especiais
{
    public class ServicoHospedagem
    {
        public int idServico { get; set; }
        public string quarto { get; set; }
        public DateTime? dataAbertura { get; set; }
        public string servico { get; set; }
        public int quantidade { get; set; }
        public decimal valor { get; set; }
        public decimal valorTotal { get; set; }


    }
}
