using Dominio.Interfaces;
using Dominio.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IGerenciadorServicos : IRepositorio<ServicoModel>
    {
        int Cadastrar(ServicoModel servico);
        void Atualizar(ServicoModel servico);
        void Remover(ServicoModel servico);
        ServicoModel ResultadoUnico(int idServico);
    }
}
