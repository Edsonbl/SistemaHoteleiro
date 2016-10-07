using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Classes
{
    public class PacoteHospedagemModel
    {

        public int idPacoteHospedagem { get; set; }
        public decimal valorTotal{ get; set; }
        public decimal subTotal{ get; set; }
        public string observacao { get; set; }
        public DateTime? dataLiberacao{ get; set; }
        public DateTime dataEntrada { get; set; }
        public DateTime? dataSaida{ get; set; }
        public DateTime dataCadastro{ get; set; }
        public bool ativo { get; set; }
        public string tipoPacote{ get; set; }
        

        public virtual List<HistoricoAcessoModel> HistoricoAcesso { get; set; }
        public virtual List<HospedagemModel> Hospedagem { get; set; }
        public virtual List<PagamentoModel> Pagamento { get; set; }
        public virtual List<TipoQuartoModel> TipoQuarto { get; set; }
        public virtual List<ControleClienteModel> ControleCliente { get; set; }
        

    }
}
