using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Infraestrutura.Repositorio;
using Dominio.Classes;


namespace ProjectHotelWebTest
{
    /// <summary>
    /// Summary description for TipoQuartoTest
    /// </summary>
    [TestClass]
    public class TipoQuartoTest
    {

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
        #region atibutos
        TipoQuartos gTipoQuartos = new TipoQuartos();
        TipoQuarto newTipoQuarto;

        #endregion


        [TestMethod]
        public void CadastrarTipoQuartoValidoTest()
        {

            newTipoQuarto = new TipoQuarto();
            newTipoQuarto.descricao = "EXTRA SUPER LUXO";
            newTipoQuarto.valor = 56;
            try
            {
                newTipoQuarto.idTipoQuarto = gTipoQuartos.Cadastrar(newTipoQuarto);

            }
            catch (Exception ex)
            {

                Assert.IsInstanceOfType(ex, typeof(Exception));

            }

            TipoQuarto tipoQuartoCadastrado = gTipoQuartos.ResultadoUnico(newTipoQuarto.idTipoQuarto);
            Assert.IsNotNull(tipoQuartoCadastrado);
            Assert.IsInstanceOfType(tipoQuartoCadastrado, typeof(TipoQuarto));
            Assert.AreEqual(newTipoQuarto, tipoQuartoCadastrado);

        }

        [TestMethod]
        public void AtualizarTipoQuartoValidoTest()
        {
            newTipoQuarto = new TipoQuarto();
            newTipoQuarto = gTipoQuartos.ResultadoUnico(1);
            newTipoQuarto.valor = 9000;
            try
            {
                gTipoQuartos.Atualizar(newTipoQuarto);

            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(Exception));
            }
            TipoQuarto tipoQuartoCadastrado = gTipoQuartos.ResultadoUnico(newTipoQuarto.idTipoQuarto);
            Assert.IsNotNull(tipoQuartoCadastrado);
            Assert.IsInstanceOfType(tipoQuartoCadastrado, typeof(TipoQuarto));
            Assert.AreEqual(newTipoQuarto, tipoQuartoCadastrado);

        }

        [TestMethod]
        public void ExcluirTipoQuartoInvalidoTest()
        {
            newTipoQuarto = new TipoQuarto();
            newTipoQuarto = gTipoQuartos.ResultadoUnico(1);
            
            try
            {
                gTipoQuartos.Remover(newTipoQuarto);

            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(Exception));
            }
            TipoQuarto tipoQuartoCadastrado = gTipoQuartos.ResultadoUnico(newTipoQuarto.idTipoQuarto);
            Assert.IsNotNull(tipoQuartoCadastrado);
            
        }



    }
}
