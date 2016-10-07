using Dominio.Classes;
using Dominio.Interfaces;
using Infraestrutura.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Repositorio
{
    public class GerenciadorItens : IGerenciadorItens
    {
        private IQueryable<ItemModel> itens;
        private IProjectHotel unidadeTrabalho;

        
        public GerenciadorItens()
        {
            this.unidadeTrabalho = new ProjectHotel();
            this.itens = unidadeTrabalho.Itens;
        }

        private GerenciadorItens(IQueryable<ItemModel> itens, IProjectHotel unidadeTrabalho)
        {
            this.itens = itens;
            this.unidadeTrabalho = unidadeTrabalho;
        }

        public GerenciadorItens(IProjectHotel iHotelWeb, IProjectHotel unidadeTrabalho) :
            this(iHotelWeb.Itens, unidadeTrabalho) { }

        public int Cadastrar(ItemModel historico)
        {
            unidadeTrabalho.RegistrarNovo(historico);
            unidadeTrabalho.Salvar();
            return itens.Max(p => p.idItem);
        }

        public int CadastrarNovo(ItemModel item)
        {
            ItemModel itemAuxiliar = null;
            itemAuxiliar = ResultadoUnicoHospedagem(item.idHospedagem, item.idProduto);
            if (itemAuxiliar != null) 
            {
                itemAuxiliar.quantidade += item.quantidade;
                itemAuxiliar.valorTotal += item.valorTotal;
                
                unidadeTrabalho.RegistrarAlterado(itemAuxiliar);
             
            }
            else
            {
                unidadeTrabalho.RegistrarNovo(item);
            }
            unidadeTrabalho.Salvar();
            return itens.Max(p => p.idItem);
        }

        public void Atualizar(ItemModel historico)
        {
            unidadeTrabalho.RegistrarAlterado(historico);
            unidadeTrabalho.Salvar();
        }

        public void Remover(ItemModel historico)
        {
            unidadeTrabalho.RegistrarRemovido(historico);
            unidadeTrabalho.Salvar();
        }

        public ItemModel ResultadoUnico(int idItem)
        {
            return itens.SingleOrDefault(c => c.idItem == idItem);
        }

        public ItemModel ResultadoUnicoHospedagem(int idHospedagem, int idProduto)
        {
            return itens.SingleOrDefault(c => c.idHospedagem == idHospedagem && c.idProduto == idProduto && c.cancelado == false);
        }

        public ICollection<ItemModel> Listar()
        {
            return itens.OrderBy(p => p.quantidade).ToList();
        }

        public ICollection<ItemModel> ListarItemPorHspedagem(int idHospedagem)
        {
            return itens.Where(p => p.cancelado == false && p.idHospedagem == idHospedagem).OrderBy(p => p.quantidade).ToList();
        }

        public bool ContemRegistro()
        {
            throw new NotImplementedException();
        }

        public int TotalRegistros()
        {
            throw new NotImplementedException();
        }
    }
}
