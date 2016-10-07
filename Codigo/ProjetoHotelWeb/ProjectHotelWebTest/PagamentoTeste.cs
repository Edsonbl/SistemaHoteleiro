using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Infraestrutura.Repositorio;
using Dominio.Classes;

namespace ProjectHotelWebTest
{
    /// <summary>
    /// Summary description for PagamentoTeste
    /// </summary>
    [TestClass]
    public class PagamentoTeste
    {
        public PagamentoTeste()
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
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void CadastrarPagamentoTest()
        {


            GerenciadorFormaPagamentos gFormaPagamento = new GerenciadorFormaPagamentos();
            GerenciadorPagamentos gPagamentos = new GerenciadorPagamentos();
            GerenciadorPacoteHospedagens gPacoteHospedagem = new GerenciadorPacoteHospedagens();

            PacoteHospedagemModel pacoteHospedagem = gPacoteHospedagem.ResultadoUnico(1);
            FormaPagamentoModel formaPagamento = gFormaPagamento.ResultadoUnico(10);
            List<PagamentoModel> listaPagamento = new List<PagamentoModel>();

            int quantidadeParcelas = formaPagamento.numeroMaximoParcela;

            for (int i = 0; i < quantidadeParcelas; i++)
            {
                PagamentoModel unidadePagamento = new PagamentoModel();
                unidadePagamento.dataCadastro = DateTime.Now.Date;
                unidadePagamento.dataPrevista = unidadePagamento.dataCadastro.AddMonths(i + 1);

                unidadePagamento.idPacoteHospedagem = pacoteHospedagem.idPacoteHospedagem;

                unidadePagamento.idFormaPagamento = formaPagamento.idFormaPagamento;
                unidadePagamento.status = "P";
                unidadePagamento.valorParcela = pacoteHospedagem.valorTotal / quantidadeParcelas;


                gPagamentos.Cadastrar(unidadePagamento);

                listaPagamento.Add(unidadePagamento);

            }

            PacoteHospedagemModel pacoteHospedagemAtual = gPacoteHospedagem.ResultadoUnico(1);
            List<PagamentoModel> pagamentoPacoteHospedagem = pacoteHospedagemAtual.Pagamento;
            int indexUltimoPagamento = pacoteHospedagem.Pagamento.Count;

            for (int i = 0; i < pacoteHospedagemAtual.Pagamento.Count; i++)
            {

                PagamentoModel pagamentoAtualPacoteHospedagem = gPagamentos.ResultadoUnico(pacoteHospedagemAtual.Pagamento[i].numeroParcela);
                PagamentoModel pagamentoAtualListaExpected = listaPagamento[i];
                Assert.IsNotNull(pagamentoAtualPacoteHospedagem);
                Assert.IsInstanceOfType(pagamentoAtualPacoteHospedagem, typeof(PagamentoModel));
                Assert.AreEqual(pagamentoAtualListaExpected.dataCadastro, pagamentoAtualPacoteHospedagem.dataCadastro);
                Assert.AreEqual(pagamentoAtualListaExpected.dataPagamento, pagamentoAtualPacoteHospedagem.dataPagamento);
                Assert.AreEqual(pagamentoAtualListaExpected.dataPrevista, pagamentoAtualPacoteHospedagem.dataPrevista);
                Assert.AreEqual(pagamentoAtualListaExpected.idFormaPagamento, pagamentoAtualPacoteHospedagem.idFormaPagamento);
                Assert.AreEqual(pagamentoAtualListaExpected.numeroParcela, pagamentoAtualPacoteHospedagem.numeroParcela);
                Assert.AreEqual(pagamentoAtualListaExpected.status, pagamentoAtualPacoteHospedagem.status);
                Assert.AreEqual(pagamentoAtualListaExpected.valorParcela, pagamentoAtualPacoteHospedagem.valorParcela);

            }



        }

        [TestMethod]
        public void AtualizarPagamentoTest()
        {

            GerenciadorFormaPagamentos gFormaPagamento = new GerenciadorFormaPagamentos();
            GerenciadorPagamentos gPagamentos = new GerenciadorPagamentos();
            GerenciadorPacoteHospedagens gPacoteHospedagem = new GerenciadorPacoteHospedagens();

            PacoteHospedagemModel pacoteHospedagem = gPacoteHospedagem.ResultadoUnico(1);
            FormaPagamentoModel formaPagamento = gFormaPagamento.ResultadoUnico(10);

            int quantidadeParcelas = formaPagamento.numeroMaximoParcela;
            int idPagamento = pacoteHospedagem.Pagamento[0].numeroParcela;

            PagamentoModel unidadePagamento = gPagamentos.ResultadoUnico(idPagamento);
            unidadePagamento.dataCadastro = DateTime.Now.Date;
            unidadePagamento.dataPrevista = unidadePagamento.dataCadastro.AddMonths(1);
            unidadePagamento.dataPagamento = unidadePagamento.dataPrevista.AddDays(-2);
            unidadePagamento.idPacoteHospedagem = pacoteHospedagem.idPacoteHospedagem;

            unidadePagamento.idFormaPagamento = formaPagamento.idFormaPagamento;
            unidadePagamento.status = "C";
            unidadePagamento.valorParcela = pacoteHospedagem.valorTotal / quantidadeParcelas;

            gPagamentos.Atualizar(unidadePagamento);

            PagamentoModel pagamentoAtualPacoteHospedagem = gPagamentos.ResultadoUnico(idPagamento);
            Assert.IsNotNull(pagamentoAtualPacoteHospedagem);
            Assert.IsInstanceOfType(pagamentoAtualPacoteHospedagem, typeof(PagamentoModel));
            Assert.AreEqual(unidadePagamento.dataCadastro, pagamentoAtualPacoteHospedagem.dataCadastro);
            Assert.AreEqual(unidadePagamento.dataPagamento, pagamentoAtualPacoteHospedagem.dataPagamento);
            Assert.AreEqual(unidadePagamento.dataPrevista, pagamentoAtualPacoteHospedagem.dataPrevista);
            Assert.AreEqual(unidadePagamento.idFormaPagamento, pagamentoAtualPacoteHospedagem.idFormaPagamento);
            Assert.AreEqual(unidadePagamento.numeroParcela, pagamentoAtualPacoteHospedagem.numeroParcela);
            Assert.AreEqual(unidadePagamento.status, pagamentoAtualPacoteHospedagem.status);
            Assert.AreEqual(unidadePagamento.valorParcela, pagamentoAtualPacoteHospedagem.valorParcela);

        }
    }
}
