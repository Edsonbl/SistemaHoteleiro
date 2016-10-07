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
    public class GerenciadorQuartos : IGerenciadorQuartos
    {
        private IQueryable<QuartoModel> quartos;
        private IProjectHotel unidadeTrabalho;

        private GerenciadorQuartos(IQueryable<QuartoModel> Quartos, IProjectHotel unidadeTrabalho)
        {
            this.quartos = Quartos;
            this.unidadeTrabalho = unidadeTrabalho;
        }
        public GerenciadorQuartos()
        {
         
            this.unidadeTrabalho = new ProjectHotel();
            this.quartos = unidadeTrabalho.Quartos;
        }
        public GerenciadorQuartos(IProjectHotel iHotelWeb, IProjectHotel unidadeTrabalho) :
            this(iHotelWeb.Quartos, unidadeTrabalho) { }

        public int Cadastrar(QuartoModel Quarto)
        {
            unidadeTrabalho.RegistrarNovo(Quarto);
            unidadeTrabalho.Salvar();
            return quartos.Max(p => p.idQuarto);
        }

        public void Atualizar(QuartoModel Quarto)
        {
            unidadeTrabalho.RegistrarAlterado(Quarto);
            unidadeTrabalho.Salvar();
        }

        public void Remover(QuartoModel Quarto)
        {
            unidadeTrabalho.RegistrarRemovido(Quarto);
            unidadeTrabalho.Salvar();
        }

        public QuartoModel ResultadoUnico(int idQuarto)
        {
            return quartos.SingleOrDefault(c => c.idQuarto == idQuarto);
        }

        public ICollection<QuartoModel> Listar()
        {
            return quartos.OrderBy(p => p.descricao).ToList();
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
