using Dominio.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Classes_Especiais
{
    public class SuperPagamento
    {
        public List<HospedagemModel> hospedagensIniciais { get; set; }
        public List<FormaPagamentoModel> formaPagamentosSelecionados { get; set; }
        
        public List<PessoaModel> pessoasSelecionadas { get; set; }
        public decimal valorTotal { get; set; }

        public SuperPagamento()
        {
            hospedagensIniciais = new List<HospedagemModel>();
            formaPagamentosSelecionados = new List<FormaPagamentoModel>();
            pessoasSelecionadas = new List<PessoaModel>();
            valorTotal =  0;
            

        }
    }
}
