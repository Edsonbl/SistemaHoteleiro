using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Infraestrutura.Repositorio;
using Dominio.Classes;

namespace ProjectHotelWebTest
{
    [TestClass]
    public class ServicoTeste
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
            int idServicoExpected = 0;
            GerenciadorServicos servicos = new GerenciadorServicos();
            ServicoModel servicoExpected = new ServicoModel();

            servicoExpected.descricao = "Lavanderia";
            servicoExpected.observacao = "Observação";
            servicoExpected.valor = 100;

            idServicoExpected = servicos.Cadastrar(servicoExpected);

            ServicoModel servicoCadastrado = servicos.ResultadoUnico(idServicoExpected);
            Assert.IsNotNull(servicoCadastrado);
            Assert.IsInstanceOfType(servicoCadastrado, typeof(ServicoModel));
            Assert.AreEqual(servicoExpected, servicoCadastrado);

        }

        [TestMethod]
        public void AtualizarTeste()
        {
            int idServico = 1;
            GerenciadorServicos servicos = new GerenciadorServicos();
            ServicoModel servico = servicos.ResultadoUnico(idServico);
            Assert.IsNotNull(servico);
            servico.descricao = "Lavanderia Refinada";
            servico.valor = 123;
            servicos.Atualizar(servico);

            ServicoModel servicoAtualizado = servicos.ResultadoUnico(idServico);
            Assert.AreEqual(servico, servicoAtualizado);
        }
    }
}
