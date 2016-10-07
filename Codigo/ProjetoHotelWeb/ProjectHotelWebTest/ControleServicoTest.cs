using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Infraestrutura.Repositorio;
using Dominio.Classes;

namespace ProjectHotelWebTest
{
    /// <summary>
    /// Summary description for ServicoTest
    /// </summary>
    [TestClass]
    public class ControleServicoTest
    {
        public ControleServicoTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        GerenciadorControleServicos gControleServicos = new GerenciadorControleServicos();
        GerenciadorHospedagens gHospedagens = new GerenciadorHospedagens();
        GerenciadorServicos gServicos = new GerenciadorServicos();
        GerenciadorPessoas gPessoas = new GerenciadorPessoas();

        ControleServicoModel controleServico;
        HospedagemModel hospedagem;
        ServicoModel servico;
        PessoaModel funcionario;


        #endregion

        [TestMethod]
        public void CadastrarControleServicoValidoTest()
        {
            hospedagem = new HospedagemModel();
            funcionario = new PessoaModel();
            servico = new ServicoModel();
            controleServico = new ControleServicoModel();
            hospedagem = gHospedagens.ResultadoUnico(1);            
            servico = gServicos.ResultadoUnico(1);            
            funcionario = gPessoas.ResultadoUnicoFuncionario(2);

            controleServico.idHospedagem = hospedagem.idHospedagem;
            controleServico.idServico = servico.idServico;
            controleServico.idFuncionario = funcionario.idPessoa;
            controleServico.dataCadastro = DateTime.Now;
            controleServico.dataAbertura = DateTime.Now;
            controleServico.dataLiberacao = controleServico.dataCadastro.AddDays(1);
            controleServico.cancelado = false;

            gControleServicos.Cadastrar(controleServico);


        }

        [TestMethod]
        public void AtualizarControleServicoValidoTest()
        {
            hospedagem = new HospedagemModel();
            funcionario = new PessoaModel();
            servico = new ServicoModel();
            controleServico = new ControleServicoModel();
            hospedagem = gHospedagens.ResultadoUnico(1);
            servico = gServicos.ResultadoUnico(1);
            funcionario = gPessoas.ResultadoUnicoFuncionario(2);

            controleServico = gControleServicos.ResultadoUnico(1);
            controleServico.cancelado = true;

            gControleServicos.Cadastrar(controleServico);
        }

    }
}
