using Dominio.Classes;
using Dominio.Classes_Especiais;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IGerenciadorTipoQuartos : IRepositorio<TipoQuartoModel>
    {
        int Cadastrar(TipoQuartoModel tipoQuarto);
        void Atualizar(TipoQuartoModel tipoQuarto);
        void Remover(TipoQuartoModel tipoQuarto);
        TipoQuartoModel ResultadoUnico(int idTipoQuarto);
        ICollection<int> ListaQuartosTipo(string tipoQuartoPesquisa, string pessoasPesquisa);
    }
}
