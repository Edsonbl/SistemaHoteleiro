using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Infraestrutura.Repositorio;
using Dominio.Classes;

namespace ProjectHotelWebTest
{
    [TestClass]
    public class ItemTeste
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
            int idItemExpected = 0;
            int idProduto = 1;
            int idHospedagem = 1;
            GerenciadorItens itens = new GerenciadorItens();
            ItemModel itemExpected = new ItemModel();

            itemExpected.quantidade = 4;
            itemExpected.dataCadastro = DateTime.Now;
            itemExpected.idProduto = idProduto;
            itemExpected.idHospedagem = idHospedagem;
            itemExpected.quantidade = 2;
            itemExpected.valorTotal = 100;

            idItemExpected = itens.Cadastrar(itemExpected);

            ItemModel itemCadastrado = itens.ResultadoUnico(idItemExpected);
            Assert.IsNotNull(itemCadastrado);
            Assert.IsInstanceOfType(itemCadastrado, typeof(ItemModel));
            Assert.AreEqual(itemExpected, itemCadastrado);

        }

        [TestMethod]
        public void AtualizarTeste()
        {
            int idItem = 1;
            GerenciadorItens itens = new GerenciadorItens();
            ItemModel item = itens.ResultadoUnico(idItem);
            Assert.IsNotNull(item);
            item.quantidade = 3;
            item.valorTotal = 150;
            itens.Atualizar(item);

            ItemModel itemAtualizado = itens.ResultadoUnico(idItem);
            Assert.AreEqual(item, itemAtualizado);
        }
    }
}
