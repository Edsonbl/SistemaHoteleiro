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
    public class GerenciadorHistoricos : IGerenciadorHistoricos
    {
        private IQueryable<HistoricoModel> historicos;
        private IProjectHotel unidadeTrabalho;

        
        public GerenciadorHistoricos()
        {
            this.unidadeTrabalho = new ProjectHotel();
            this.historicos = unidadeTrabalho.Historicos;
        }

        private GerenciadorHistoricos(IQueryable<HistoricoModel> historicos, IProjectHotel unidadeTrabalho)
        {
            this.historicos = historicos;
            this.unidadeTrabalho = unidadeTrabalho;
        }

        public GerenciadorHistoricos(IProjectHotel iHotelWeb, IProjectHotel unidadeTrabalho) :
            this(iHotelWeb.Historicos, unidadeTrabalho) { }

        public void Cadastrar(HistoricoModel historico)
        {
            unidadeTrabalho.RegistrarNovo(historico);
            unidadeTrabalho.Salvar();
        }

        public void Atualizar(HistoricoModel historico)
        {
            unidadeTrabalho.RegistrarAlterado(historico);
            unidadeTrabalho.Salvar();
        }

        public void Remover(HistoricoModel historico)
        {
            unidadeTrabalho.RegistrarRemovido(historico);
            unidadeTrabalho.Salvar();
        }

        public HistoricoModel ResultadoUnico(int idHistorico)
        {
            return historicos.SingleOrDefault(c => c.idHistorico == idHistorico);
        }

        public ICollection<HistoricoModel> Listar()
        {
            return historicos.OrderBy(p => p.descricao).ToList();
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
