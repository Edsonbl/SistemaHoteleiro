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
    public class GerenciadorControleClientes : IGerenciadorControleClientes
    {
        private IQueryable<ControleClienteModel> controleClientes;
        private IProjectHotel unidadeTrabalho;

        public GerenciadorControleClientes()
        {
            this.unidadeTrabalho = new ProjectHotel();
            this.controleClientes = unidadeTrabalho.ControleClientes;
        }

        private GerenciadorControleClientes(IQueryable<ControleClienteModel> controleClientes, IProjectHotel unidadeTrabalho)
        {
            this.controleClientes = controleClientes;
            this.unidadeTrabalho = unidadeTrabalho;
        }
        
        public GerenciadorControleClientes(IProjectHotel iHotelWeb, IProjectHotel unidadeTrabalho) :
            this(iHotelWeb.ControleClientes, unidadeTrabalho) { }

        public void Cadastrar(ControleClienteModel controleCliente)
        {
            unidadeTrabalho.RegistrarNovo(controleCliente);
            unidadeTrabalho.Salvar();
        }

        public void Atualizar(ControleClienteModel controleCliente)
        {
            unidadeTrabalho.RegistrarAlterado(controleCliente);
            unidadeTrabalho.Salvar();
        }

        public void Remover(ControleClienteModel controleCliente)
        {
            unidadeTrabalho.RegistrarRemovido(controleCliente);
            unidadeTrabalho.Salvar();
        }

        public ControleClienteModel ResultadoUnico(int idControleCliente, int idHospedagem)
        {
            return controleClientes.SingleOrDefault(c => c.idCliente == idControleCliente && c.idHospedagem == idHospedagem );
        }

        public ControleClienteModel ResultadoUnicoHospedagem(int idHospedagem)
        {
            return controleClientes.SingleOrDefault(c => c.idHospedagem == idHospedagem);
        }

        public ICollection<ControleClienteModel> Listar()
        {
            return controleClientes.OrderBy(p => p.idHospedagem).ToList();
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
