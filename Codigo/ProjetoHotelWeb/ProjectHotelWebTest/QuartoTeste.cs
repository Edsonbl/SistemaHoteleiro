using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Infraestrutura.Repositorio;
using Dominio.Classes;

namespace ProjectHotelWebTest
{
    [TestClass]
    public class QuartoTeste
    {

        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }


        [TestMethod]
        public void CadastrarQuartoTest()
        {
            int idQuartoModelExpected = 0;
            GerenciadorQuartos gQuartos = new GerenciadorQuartos();
            QuartoModel quartoModelExpected = new QuartoModel();

            quartoModelExpected.ativo = true;
            quartoModelExpected.capacidade = 3;
            quartoModelExpected.descricao = "Quarto 302";
            quartoModelExpected.idTipoQuarto = 1;
            quartoModelExpected.observacao = "Quarto modelo conforto";
            quartoModelExpected.reservado = false;
            quartoModelExpected.status = "L";

            idQuartoModelExpected = gQuartos.Cadastrar(quartoModelExpected);

            QuartoModel quartoCadastrado = gQuartos.ResultadoUnico(idQuartoModelExpected);
            Assert.IsNotNull(quartoCadastrado);
            Assert.IsInstanceOfType(quartoCadastrado, typeof(QuartoModel));
            Assert.AreEqual(quartoModelExpected, quartoCadastrado);
        }

        [TestMethod]
        public void AtualizarQuartoTest()
        {
            GerenciadorQuartos gQuartos = new GerenciadorQuartos();
            QuartoModel quarto = gQuartos.ResultadoUnico(1);

            quarto.idQuarto = quarto.idQuarto;
            quarto.idTipoQuarto = 2;
            quarto.observacao = quarto.observacao;
            quarto.reservado = true;
            quarto.status = "O";

            gQuartos.Atualizar(quarto);

            QuartoModel quartoAtualizado = gQuartos.ResultadoUnico(1); 
            Assert.AreEqual(quartoAtualizado.idQuarto ,  quarto.idQuarto);
            Assert.AreEqual(quartoAtualizado.idTipoQuarto ,  2);
            Assert.AreEqual(quartoAtualizado.observacao ,  quarto.observacao);
            Assert.AreEqual(quartoAtualizado.reservado ,  true);
            Assert.AreEqual(quartoAtualizado.status ,  "O");

        }

    }
}
