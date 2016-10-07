using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Classes
{
    public class FormaPagamentoModel
    {
        public int idFormaPagamento { get; set; }
        [Required]
        public string descricao { get; set; }
        public string observacao { get; set; }
        [Required]
        public int numeroMaximoParcela { get; set; }
        public bool ativo { get; set; }
        [Required]
        public string tipo { get; set; }

        public virtual List<PagamentoModel> Pagamento { get; set; }
    }
}
