using Dominio.Classes;
using Dominio.Interfaces;
using Dominio.Classes_Especiais;
using Infraestrutura.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Repositorio
{
    public class GerenciadorTipoQuartos : IGerenciadorTipoQuartos
    {
        private IQueryable<TipoQuartoModel> tipoQuartos;
        private IProjectHotel unidadeTrabalho;
        private IQueryable<QuartoModel> quartos;
        private IQueryable<PacoteHospedagemModel> pacoteHospedagem;
        private IQueryable<HospedagemModel> hospedagem;
        private IQueryable<ControleClienteModel> cliente;

        
        public GerenciadorTipoQuartos()
        {
            this.unidadeTrabalho = new ProjectHotel();
            this.tipoQuartos = unidadeTrabalho.TipoQuartos;
        }

        private GerenciadorTipoQuartos(IQueryable<TipoQuartoModel> TipoQuartos, IProjectHotel unidadeTrabalho)
        {
            this.tipoQuartos = TipoQuartos;
            this.unidadeTrabalho = unidadeTrabalho;
            quartos = unidadeTrabalho.Quartos;
            pacoteHospedagem = unidadeTrabalho.PacoteHospedagens;
            hospedagem = unidadeTrabalho.Hospedagens;
            cliente = unidadeTrabalho.ControleClientes;
        }

        public GerenciadorTipoQuartos(IProjectHotel iHotelWeb, IProjectHotel unidadeTrabalho) :
            this(iHotelWeb.TipoQuartos, unidadeTrabalho) { }


        public int Cadastrar(TipoQuartoModel TipoQuarto)
        {
            unidadeTrabalho.RegistrarNovo(TipoQuarto);
            unidadeTrabalho.Salvar();
            return tipoQuartos.Max(t => t.idTipoQuarto);
        }

        public void Atualizar(TipoQuartoModel TipoQuarto)
        {
            unidadeTrabalho.RegistrarAlterado(TipoQuarto);
            unidadeTrabalho.Salvar();
        }

        public void Remover(TipoQuartoModel TipoQuarto)
        {
            unidadeTrabalho.RegistrarRemovido(TipoQuarto);
            unidadeTrabalho.Salvar();
        }

        public TipoQuartoModel ResultadoUnico(int idTipoQuarto)
        {
            return tipoQuartos.SingleOrDefault(c => c.idTipoQuarto == idTipoQuarto);
        }

        public ICollection<TipoQuartoModel> Listar()
        {
            return tipoQuartos.OrderBy(p => p.descricao).ToList();
        }    

        public ICollection<int> ListaQuartosTipo(string idTipoQuartoPesquisa, string pessoasPesquisa)
        {
            List<int> indicesQuartos = new List<int>();

            IQueryable<int> consultaIdQuartosTipo = from tipo_quarto in tipoQuartos
                                                    join quarto in quartos on tipo_quarto.idTipoQuarto equals quarto.idTipoQuarto
                                                    where quarto.status.Equals("L") && tipo_quarto.idTipoQuarto.ToString().Equals(idTipoQuartoPesquisa)
                                                    && quarto.capacidade.ToString().Equals(pessoasPesquisa)
                                                    select quarto.idQuarto;


            indicesQuartos = consultaIdQuartosTipo.ToList();

            return indicesQuartos;
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
