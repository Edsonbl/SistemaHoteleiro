
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
    public class GerenciadorServicos : IGerenciadorServicos
    {
        private IQueryable<ServicoModel> servicos;
        private IProjectHotel unidadeTrabalho;

        
        public GerenciadorServicos()
        {
            this.unidadeTrabalho = new ProjectHotel();
            this.servicos = unidadeTrabalho.Servicos;
        }

        private GerenciadorServicos(IQueryable<ServicoModel> servicos, IProjectHotel unidadeTrabalho)
        {
            this.servicos = servicos;
            this.unidadeTrabalho = unidadeTrabalho;
        }

        public GerenciadorServicos(IProjectHotel iHotelWeb, IProjectHotel unidadeTrabalho) : 
               this(iHotelWeb.Servicos, unidadeTrabalho) { }

        public int Cadastrar(ServicoModel servico)
        {
            unidadeTrabalho.RegistrarNovo(servico);
            unidadeTrabalho.Salvar();
            return servicos.Max(s => s.idServico);
        }

        public void Atualizar(ServicoModel servico)
        {
            unidadeTrabalho.RegistrarAlterado(servico);
            unidadeTrabalho.Salvar();
        }

        public void Remover(ServicoModel servico)
        {
            unidadeTrabalho.RegistrarRemovido(servico);
            unidadeTrabalho.Salvar();
        }

        public ServicoModel ResultadoUnico(int idServico)
        {
            return servicos.SingleOrDefault(s => s.idServico== idServico);
        }

        public ICollection<ServicoModel> Listar()
        {
            return servicos.OrderBy(p => p.descricao).ToList();
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
