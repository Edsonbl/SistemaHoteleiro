using Dominio.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IGerenciadorQuartos : IRepositorio<QuartoModel>
    {
        int Cadastrar(QuartoModel quarto);
        void Atualizar(QuartoModel quarto);
        void Remover(QuartoModel quarto);
        QuartoModel ResultadoUnico(int idQuarto);
    }
}
