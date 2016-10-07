using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Infraestrutura.Repositorio;
using Dominio.Classes;

namespace ProjectHotelWebTest
{
    [TestClass]
    public class FormaPagamentoTeste
    {
        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        [TestMethod]
        public void CadastrarTeste()
        {
            int idFormaPagamentoExpected = 0;
            GerenciadorFormaPagamentos formaPagamento = new GerenciadorFormaPagamentos();
            FormaPagamentoModel formaPagamentoExpected = new FormaPagamentoModel();

            formaPagamentoExpected.descricao = "Avista";
            formaPagamentoExpected.observacao = "Avista";
            formaPagamentoExpected.tipo = "Avista";

            idFormaPagamentoExpected = formaPagamento.Cadastrar(formaPagamentoExpected);

            FormaPagamentoModel formaPagamentoCadastrado = formaPagamento.ResultadoUnico(idFormaPagamentoExpected);
            Assert.IsNotNull(formaPagamentoCadastrado);
            Assert.IsInstanceOfType(formaPagamentoCadastrado, typeof(FormaPagamentoModel));
            Assert.AreEqual(formaPagamentoExpected, formaPagamentoCadastrado);

        }

        [TestMethod]
        public void AtualizarTeste()
        {
            int idFormaPagamento = 10;
            GerenciadorFormaPagamentos formaPagamentos = new GerenciadorFormaPagamentos();
            FormaPagamentoModel formaPagamento = formaPagamentos.ResultadoUnico(idFormaPagamento);
            Assert.IsNotNull(formaPagamento);
            formaPagamento.descricao = "A vista";
            formaPagamento.observacao = "A vista";
            formaPagamentos.Atualizar(formaPagamento);

            FormaPagamentoModel formaPagamentoAtualizado = formaPagamentos.ResultadoUnico(idFormaPagamento);
            Assert.AreEqual(formaPagamento, formaPagamentoAtualizado);
        }
    }
}
