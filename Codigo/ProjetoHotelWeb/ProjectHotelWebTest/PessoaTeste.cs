using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Infraestrutura.Repositorio;
using Dominio.Classes;

namespace ProjectHotelWebTest
{
    [TestClass]
    public class PessoaTeste
    {

        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        #region Atributos
        GerenciadorPessoas gPessoas = new GerenciadorPessoas();
        PessoaModel pessoa;


        #endregion



        [TestMethod]
        public void CadastrarFuncionarioValidoTest()
        {
            pessoa = new PessoaModel();
            pessoa.idCargo = 1;
            pessoa.nome = "Yuri Bras de Caminha";
            pessoa.cpfCnpj = "90845672634";
            pessoa.dataNascimento = DateTime.Now;
            pessoa.estadoCivil = "Solteiro";
            pessoa.sexo = "M";
            pessoa.telefoneMovel = "7999876543";
            pessoa.emailPimario = "yuribras@gmail.com";
            pessoa.salario = 780;
            pessoa.estado = "SE";
            pessoa.cidade = "Itabaiana";
            pessoa.bairro = "centro";
            pessoa.rua = "rua trancredo de neves";
            pessoa.numero = 24;
            pessoa.cep = "49500000";
            pessoa.dataCadastro = DateTime.Now;
            pessoa.ativo = true;
            pessoa.isFuncionario = true;

            gPessoas.Cadastrar(pessoa);


        }

        [TestMethod]
        public void AtualizarFuncionarioValidoTest()
        {
            pessoa = new PessoaModel();
            pessoa = gPessoas.ResultadoUnicoFuncionario(2);
            pessoa.idCargo = 2;

            gPessoas.Atualizar(pessoa);


        }
        [TestMethod]
        public void CadastrarClienteValidoTest()
        {
            pessoa = new PessoaModel();

            pessoa.nome = "Iury Bras de Caminha";
            pessoa.cpfCnpj = "90845672004";
            pessoa.dataNascimento = DateTime.Now;
            pessoa.estadoCivil = "Solteiro";
            pessoa.sexo = "M";
            pessoa.telefoneMovel = "7999876543";
            pessoa.emailPimario = "iurybras@gmail.com";

            pessoa.estado = "SE";
            pessoa.cidade = "Itabaiana";
            pessoa.bairro = "centro";
            pessoa.rua = "rua trancredo de neves";
            pessoa.numero = 24;
            pessoa.cep = "49500000";
            pessoa.dataCadastro = DateTime.Now;
            pessoa.ativo = true;
            pessoa.isFuncionario = false;

            gPessoas.Cadastrar(pessoa);


        }

        [TestMethod]
        public void AtualizarClienteValidoTest()
        {
            pessoa = new PessoaModel();
            pessoa = gPessoas.ResultadoUnico(2);
            pessoa.estado = "SC";

            gPessoas.Atualizar(pessoa);


        }
    }
}
