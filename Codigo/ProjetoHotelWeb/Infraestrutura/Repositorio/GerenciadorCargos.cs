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
    public class GerenciadorCargos : IGerenciadorCargos
    {
        private IQueryable<CargoModel> cargos;
        private IProjectHotel unidadeTrabalho;

        public GerenciadorCargos()
        {
            this.unidadeTrabalho = new ProjectHotel();
            this.cargos = unidadeTrabalho.Cargos;
        }

        private GerenciadorCargos(IQueryable<CargoModel> Cargos, IProjectHotel unidadeTrabalho)
        {
            this.cargos = Cargos;
            this.unidadeTrabalho = unidadeTrabalho;
        }

        public GerenciadorCargos(IProjectHotel iHotelWeb, IProjectHotel unidadeTrabalho) :
            this(iHotelWeb.Cargos, unidadeTrabalho) { }

        public int Cadastrar(CargoModel Cargo)
        {
            unidadeTrabalho.RegistrarNovo(Cargo);
            unidadeTrabalho.Salvar();
            return cargos.Max(p => p.idCargo);
        }

        public void Atualizar(CargoModel Cargo)
        {
            unidadeTrabalho.RegistrarAlterado(Cargo);
            unidadeTrabalho.Salvar();
        }

        public void Remover(CargoModel Cargo)
        {
            unidadeTrabalho.RegistrarRemovido(Cargo);
            unidadeTrabalho.Salvar();
        }

        public CargoModel ResultadoUnico(int idCargo)
        {
            return cargos.SingleOrDefault(c => c.idCargo == idCargo);
        }

        public ICollection<CargoModel> Listar()
        {
            return cargos.OrderBy(p => p.descricao).ToList();
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
