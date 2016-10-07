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
    public class GerenciadorFormaPagamentos : IGerenciadorFormaPagamentos
    {
        private IQueryable<FormaPagamentoModel> formaPagamentos;
        private IProjectHotel unidadeTrabalho;

        
        public GerenciadorFormaPagamentos()
        {
            this.unidadeTrabalho = new ProjectHotel();
            this.formaPagamentos = unidadeTrabalho.FormaPagamentos;
        }

        private GerenciadorFormaPagamentos(IQueryable<FormaPagamentoModel> formaPagamentos, IProjectHotel unidadeTrabalho)
        {
            this.formaPagamentos = formaPagamentos;
            this.unidadeTrabalho = unidadeTrabalho;
        }

        public GerenciadorFormaPagamentos(IProjectHotel iHotelWeb, IProjectHotel unidadeTrabalho) :
            this(iHotelWeb.FormaPagamentos, unidadeTrabalho) { }

        public void Cadastrar(FormaPagamentoModel formaPagamento)
        {
            unidadeTrabalho.RegistrarNovo(formaPagamento);
            unidadeTrabalho.Salvar();
        }

        public void Atualizar(FormaPagamentoModel formaPagamento)
        {
            unidadeTrabalho.RegistrarAlterado(formaPagamento);
            unidadeTrabalho.Salvar();
        }

        public void Remover(FormaPagamentoModel formaPagamento)
        {
            unidadeTrabalho.RegistrarRemovido(formaPagamento);
            unidadeTrabalho.Salvar();
        }

        public FormaPagamentoModel ResultadoUnico(int idFormaPagamento)
        {
            return formaPagamentos.SingleOrDefault(c => c.idFormaPagamento== idFormaPagamento);
        }
        public List<FormaPagamentoModel> ResultadoUnicoTipo(string tipo)
        {
            return formaPagamentos.Where(c => c.tipo== tipo).ToList();
        }

        public ICollection<FormaPagamentoModel> Listar()
        {
            return formaPagamentos.OrderBy(p => p.descricao).ToList();
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
