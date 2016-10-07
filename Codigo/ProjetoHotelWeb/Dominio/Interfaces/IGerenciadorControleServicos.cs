using Dominio.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Classes_Especiais;

namespace Dominio.Interfaces
{
   public interface IGerenciadorControleServicos : IRepositorio<ControleServicoModel>
    {
       void Cadastrar(ControleServicoModel controleServico);
       void Atualizar(ControleServicoModel controleServico);
       void Remover(ControleServicoModel controleServico);
       ControleServicoModel ResultadoUnico(int idServico);
       ICollection<ControleServicoModel> ListarServicosIndividualmente(int idHospedagem);
       ICollection<ServicoHospedagem> ListarServicoHospedagem(int id);
    }
}
