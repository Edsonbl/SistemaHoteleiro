using Dominio.Classes;
using Dominio.Interfaces;
using Infraestrutura.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Classes_Especiais;
using System.Data.Entity;




namespace Infraestrutura.Repositorio
{
    public class GerenciadorControleServicos : IGerenciadorControleServicos
    {
        private IQueryable<ControleServicoModel> controleServicos;
        private IQueryable<QuartoModel> quarto;
        private IQueryable<HospedagemModel> hospedagem;
        private IQueryable<ServicoModel> servico;
        private IProjectHotel unidadeTrabalho;
        
        
        public GerenciadorControleServicos()
        {
            this.unidadeTrabalho = new ProjectHotel();
            this.controleServicos = unidadeTrabalho.ControleServicos;
        }

        private GerenciadorControleServicos(IQueryable<ControleServicoModel> controleServicos, IProjectHotel unidadeTrabalho)
        {
            this.controleServicos = controleServicos;
            this.unidadeTrabalho = unidadeTrabalho;
            this.quarto = unidadeTrabalho.Quartos;
            this.servico = unidadeTrabalho.Servicos;
            this.hospedagem = unidadeTrabalho.Hospedagens;
        }

        public GerenciadorControleServicos(IProjectHotel iHotelWeb, IProjectHotel unidadeTrabalho) :
            this(iHotelWeb.ControleServicos, unidadeTrabalho) { }

        public void Cadastrar(ControleServicoModel controleServico)
        {
            unidadeTrabalho.RegistrarNovo(controleServico);
            unidadeTrabalho.Salvar();
        }

        public void Atualizar(ControleServicoModel controleServico)
        {
            unidadeTrabalho.RegistrarAlterado(controleServico);
            unidadeTrabalho.Salvar();
        }

        public void Remover(ControleServicoModel controleServico)
        {
            unidadeTrabalho.RegistrarRemovido(controleServico);
            unidadeTrabalho.Salvar();
        }

        public ControleServicoModel ResultadoUnico(int idControleServico)
        {
            return controleServicos.SingleOrDefault(c => c.idControleServico== idControleServico);
        }

        public ICollection<ControleServicoModel> Listar()
        {
            return controleServicos.OrderBy(p => p.idHospedagem).ToList();
        }

        public ICollection<ControleServicoModel> ListarServicosIndividualmente(int idHospedagem)
        {
            return controleServicos.Where(p => p.idHospedagem == idHospedagem).OrderByDescending(p => p.dataAbertura).ToList();
        }

        public ICollection<ServicoHospedagem> ListarServicoHospedagem(int idHospedagem)
        {
            IQueryable<ServicoHospedagem> servicosHospedagem = 
                from h in hospedagem 
                join q in quarto on h.idQuarto equals q.idQuarto
                join cs in controleServicos on h.idHospedagem equals cs.idHospedagem
                join s in servico on cs.idServico equals s.idServico
                where cs.idHospedagem == idHospedagem && !cs.cancelado
                group h by new {
                    cs.idServico,
                    q.descricao,
                    data = DbFunctions.TruncateTime(
                    cs.dataAbertura),
                    s.valor,
                    servico = s.descricao 
                } into servicoHospedagem 
                select new ServicoHospedagem{
                    idServico = servicoHospedagem.Key.idServico,
                 dataAbertura = servicoHospedagem.Key.data,
                 quantidade = servicoHospedagem.Count(),
                 quarto = servicoHospedagem.Key.descricao,
                 servico = servicoHospedagem.Key.servico,
                valor = servicoHospedagem.Key.valor,
                valorTotal = (servicoHospedagem.Key.valor * servicoHospedagem.Count())
                };

            return servicosHospedagem.ToList(); 
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
