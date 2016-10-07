
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
    public class HistoricoAcesso: IGerenciadorHistoricoAcessos
    {
        private IQueryable<HistoricoAcessoModel> historicoAcessos;
        private IProjectHotel unidadeTrabalho;


        public HistoricoAcesso()
        {
            this.unidadeTrabalho = new ProjectHotel();
            this.historicoAcessos = unidadeTrabalho.HistoricoAcessos;
        }

        private HistoricoAcesso (IQueryable<HistoricoAcessoModel> historicoAcessos, IProjectHotel unidadeTrabalho)
        {
            this.historicoAcessos = historicoAcessos;
            this.unidadeTrabalho = unidadeTrabalho;
        }

        public HistoricoAcesso(IProjectHotel iHotelWeb, IProjectHotel unidadeTrabalho) :
            this(iHotelWeb.HistoricoAcessos, unidadeTrabalho) { }

        public void Cadastrar(HistoricoAcessoModel historicoAcesso)
        {
            unidadeTrabalho.RegistrarNovo(historicoAcesso);
            unidadeTrabalho.Salvar();
        }

        public void Atualizar(HistoricoAcessoModel historicoAcesso)
        {
            unidadeTrabalho.RegistrarAlterado(historicoAcesso);
            unidadeTrabalho.Salvar();
        }

        public void Remover(HistoricoAcessoModel historicoAcesso)
        {
            unidadeTrabalho.RegistrarRemovido(historicoAcesso);
            unidadeTrabalho.Salvar();
        }

        public HistoricoAcessoModel ResultadoUnico(int idHistoricoAcesso)
        {
            return historicoAcessos.SingleOrDefault(c => c.idHistoricoAcesso == idHistoricoAcesso);
        }

        public ICollection<HistoricoAcessoModel> Listar()
        {
            return historicoAcessos.OrderBy(p => p.observacao).ToList();
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
