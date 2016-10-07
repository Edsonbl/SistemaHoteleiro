using Dominio.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IGerenciadorHistoricoAcessos : IRepositorio<HistoricoAcessoModel>
    {
        void Cadastrar(HistoricoAcessoModel historicoAcesso);
        void Atualizar(HistoricoAcessoModel historicoAcesso);
        void Remover(HistoricoAcessoModel historicoAcesso);
        HistoricoAcessoModel ResultadoUnico(int idHistoricoAcesso);
    }
}
