using Dominio.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Classes_Especiais;

namespace Dominio.Interfaces
{
    public interface IGerenciadorHospedagens : IRepositorio<HospedagemModel>
    {
        int Cadastrar(HospedagemModel hospedagem);
        void Atualizar(HospedagemModel hospedagem);
        void Remover(HospedagemModel hospedagem);
        HospedagemModel ResultadoUnico(int idHospedagem);
        ICollection<HospedagemMes> RelatorioHospedagemMes();
    }
}
