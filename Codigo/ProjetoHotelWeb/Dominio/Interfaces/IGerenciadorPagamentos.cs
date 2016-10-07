using Dominio.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IGerenciadorPagamentos : IRepositorio<PagamentoModel>
    {
        void Cadastrar(PagamentoModel pagamento);
        void Atualizar(PagamentoModel pagamento);
        void Remover(PagamentoModel pagamento);
        PagamentoModel ResultadoUnico(int idPagamento);
    }
}
