using Dominio.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IGerenciadorControleClientes : IRepositorio<ControleClienteModel>
    {
        void Cadastrar(ControleClienteModel controleCliente);
        void Atualizar(ControleClienteModel controleCLiente);
        void Remover(ControleClienteModel controleCliente);
        ControleClienteModel ResultadoUnico(int idCliente, int idHospedagem);
        ControleClienteModel ResultadoUnicoHospedagem(int idHospedagem);
    }
}
