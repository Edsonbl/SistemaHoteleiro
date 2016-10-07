using Dominio.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IGerenciadorCargos : IRepositorio<CargoModel>
    {
        int Cadastrar(CargoModel cargo);
        void Atualizar(CargoModel cargo);
        void Remover(CargoModel cargo);
        CargoModel ResultadoUnico(int idCargo);
    }
}
