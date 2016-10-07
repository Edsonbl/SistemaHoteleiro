using Dominio.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IGerenciadorHistoricos : IRepositorio<HistoricoModel>
    {
        void Cadastrar(HistoricoModel historico);
        void Atualizar(HistoricoModel historico);
        void Remover(HistoricoModel historico);
        HistoricoModel ResultadoUnico(int idHisotirco);
    }
}
