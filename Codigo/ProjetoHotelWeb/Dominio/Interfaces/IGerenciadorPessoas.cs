using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Classes;
using Dominio.Interfaces;



namespace Dominio.Interfaces
{
    public interface IGerenciadorPessoas : IRepositorio<PessoaModel>
    {
        void Cadastrar(PessoaModel pessoa);
        void Atualizar(PessoaModel pessoa);
        void Remover(PessoaModel pessoa);
        PessoaModel ResultadoUnico(int idPessoa);
        ICollection<PessoaModel> ListarFuncionario();
        ICollection<PessoaModel> ListarPorCpfCnpj(string cpfCnpj);
        ICollection<PessoaModel> ListarPorNome(string nome);
        ICollection<PessoaModel> ListarPorTelefone(string telefone);

        ICollection<PessoaModel> ListarFuncionarioPorNome(string nome);
        ICollection<PessoaModel> ListarFuncionarioPorCpfCnpj(string cpfCnpj);
        ICollection<PessoaModel> ListarFuncionarioPorTelefone(string telefone);


        PessoaModel ResultadoUnicoFuncionario(int idPessoa);


    }
}
