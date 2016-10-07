using Dominio.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IGerenciadorFormaPagamentos : IRepositorio<FormaPagamentoModel>
    {
        void Cadastrar(FormaPagamentoModel formaPagamento);
        void Atualizar(FormaPagamentoModel formaPagamento);
        void Remover(FormaPagamentoModel formaPagamento);
        FormaPagamentoModel ResultadoUnico(int idFormaPagamento);
        List<FormaPagamentoModel> ResultadoUnicoTipo(string tipo);
    }
}
