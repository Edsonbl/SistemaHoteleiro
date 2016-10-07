

using Dominio.Classes;
using Dominio.Interfaces;
using Infraestrutura.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Repositorio
{
    public class GerenciadorPessoas : IGerenciadorPessoas
    {
        private IQueryable<PessoaModel> pessoas;
        private IProjectHotel unidadeTrabalho;

        
      

        private GerenciadorPessoas(IQueryable<PessoaModel> pessoas, IProjectHotel unidadeTrabalho)
        {
            this.pessoas = pessoas;
            this.unidadeTrabalho = unidadeTrabalho;
        }
        public GerenciadorPessoas()
        {

            this.unidadeTrabalho = new ProjectHotel();
            this.pessoas = unidadeTrabalho.Funcionarios;
        }

        public GerenciadorPessoas(IProjectHotel iHotelWeb, IProjectHotel unidadeTrabalho) :
            this(iHotelWeb.Funcionarios, unidadeTrabalho) { }

        public void Cadastrar(PessoaModel pessoa)
        {
            unidadeTrabalho.RegistrarNovo(pessoa);
            unidadeTrabalho.Salvar();
        }

        public void Atualizar(PessoaModel pessoa)
        {
            unidadeTrabalho.RegistrarAlterado(pessoa);
            unidadeTrabalho.Salvar();
        }

        public void Remover(PessoaModel pessoa)
        {
            unidadeTrabalho.RegistrarRemovido(pessoa);
            unidadeTrabalho.Salvar();
        }

        public PessoaModel ResultadoUnico(int idPessoa)
        {
            return pessoas.SingleOrDefault(p => p.idPessoa == idPessoa && p.ativo == true);
        }
        public ICollection<PessoaModel> Listar()
        {
            return pessoas.Where(p =>p.ativo == true).OrderBy(p=> p.nome).ToList();
            
        }

        public ICollection<PessoaModel> ListarPorCpfCnpj(string cpfCnpj)
        {
            return pessoas.Where(p => p.ativo == true && p.cpfCnpj.Contains(cpfCnpj)).OrderBy(p => p.nome).ToList();
            
        }

        public ICollection<PessoaModel> ListarPorNome(string nome)
        {
            
            return pessoas.Where(p => p.ativo == true && p.nome.Contains(nome)).OrderBy(p => p.nome).ToList();
            
        }
        public ICollection<PessoaModel> ListarPorTelefone(string telefone)
        {
            return pessoas.Where(p => p.telefoneMovel.Contains(telefone) && p.ativo == true).ToList();
        }

        public ICollection<PessoaModel> ListarFuncionarioPorCpfCnpj(string cpfCnpj)
        {
            return pessoas.Where(p => p.ativo == true && p.isFuncionario == true && p.cpfCnpj.Contains(cpfCnpj)).OrderBy(p => p.nome).ToList();
            
        }

        public ICollection<PessoaModel> ListarFuncionarioPorNome(string nome)
        {

            return pessoas.Where(p => p.ativo == true && p.isFuncionario == true && p.nome.Contains(nome)).OrderBy(p => p.nome).ToList();
            
        }
        public ICollection<PessoaModel> ListarFuncionarioPorTelefone(string telefone)
        {
            return pessoas.Where(p => p.telefoneMovel.Contains(telefone) && p.ativo == true && p.isFuncionario == true).ToList();
        }




        public ICollection<PessoaModel> ListarFuncionario()
        {
            return pessoas.Where(p => p.isFuncionario == true && p.ativo == true).OrderBy(p => p.nome).ToList();
            //OrderBy(p => p.nome).ToList();
        }
        public PessoaModel ResultadoUnicoFuncionario(int idPessoa)
        {
            return pessoas.SingleOrDefault(p => p.idPessoa == idPessoa  && p.ativo == true);
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
