using Dominio.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IGerenciadorItens : IRepositorio<ItemModel>
    {
        int Cadastrar(ItemModel item);
        int CadastrarNovo(ItemModel item);
        void Atualizar(ItemModel item);
        void Remover(ItemModel item);
        ItemModel ResultadoUnico(int idItem);
        ItemModel ResultadoUnicoHospedagem(int idHospedagem, int idProduto);
        ICollection<ItemModel> ListarItemPorHspedagem(int idHospedagem);

    }
}
