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
    public class GerenciadorPagamentos : IGerenciadorPagamentos
    {
        private IQueryable<PagamentoModel> pagamentos;
        private IProjectHotel unidadeTrabalho;

        
        public GerenciadorPagamentos()
        {
            this.unidadeTrabalho = new ProjectHotel();
            this.pagamentos = unidadeTrabalho.Pagamentos;
        }

        private GerenciadorPagamentos(IQueryable<PagamentoModel> Pagamentos, IProjectHotel unidadeTrabalho)
        {
            this.pagamentos = Pagamentos;
            this.unidadeTrabalho = unidadeTrabalho;
        }

        public GerenciadorPagamentos(IProjectHotel iHotelWeb, IProjectHotel unidadeTrabalho) :
            this(iHotelWeb.Pagamentos, unidadeTrabalho) { }

        public void Cadastrar(PagamentoModel Pagamento)
        {
            unidadeTrabalho.RegistrarNovo(Pagamento);
            unidadeTrabalho.Salvar();
        }

        public void Atualizar(PagamentoModel Pagamento)
        {
            unidadeTrabalho.RegistrarAlterado(Pagamento);
            unidadeTrabalho.Salvar();
        }

        public void Remover(PagamentoModel Pagamento)
        {
            unidadeTrabalho.RegistrarRemovido(Pagamento);
            unidadeTrabalho.Salvar();
        }

        public PagamentoModel ResultadoUnico(int idPagamento)
        {
            return pagamentos.SingleOrDefault(c => c.numeroParcela== idPagamento);
        }

        public ICollection<PagamentoModel> Listar()
        {
            return pagamentos.OrderBy(p => p.status).ToList();
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
