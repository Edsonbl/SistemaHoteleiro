using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Infraestrutura.Repositorio;
using Dominio.Classes;

namespace ProjectHotelWebTest
{
    [TestClass]
    public class ReservaTeste
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
            int idPacoteHospedagem = 0;
            PacoteHospedagemModel pacoteHospedagem = new PacoteHospedagemModel();
            GerenciadorPacoteHospedagens IPacoteHospedagem = new GerenciadorPacoteHospedagens();

            pacoteHospedagem.valorTotal = 0;
            pacoteHospedagem.subTotal = 0;
            pacoteHospedagem.dataCadastro = DateTime.Now;
            pacoteHospedagem.ativo = true;
            pacoteHospedagem.tipoPacote = "R";

            idPacoteHospedagem = IPacoteHospedagem.Cadastrar(pacoteHospedagem);


            PacoteHospedagemModel pacoteHospedagemCadastrado = IPacoteHospedagem.ResultadoUnicoReserva(idPacoteHospedagem);
            Assert.IsNotNull(pacoteHospedagemCadastrado);
            Assert.IsInstanceOfType(pacoteHospedagemCadastrado, typeof(PacoteHospedagemModel));
            Assert.AreEqual(pacoteHospedagem, pacoteHospedagemCadastrado);

            int idHospedagem = 0;
            int idQuarto = 1;
            HospedagemModel hospedagem = new HospedagemModel();
            GerenciadorHospedagens IHospedagem = new GerenciadorHospedagens();

            hospedagem.aberto = true;
            hospedagem.idPacoteHospedagem = idPacoteHospedagem;
            hospedagem.dataAbertura = DateTime.Now;
            hospedagem.dataLiberacao = DateTime.Now;
            hospedagem.idQuarto = idQuarto;
            hospedagem.valorHospedagem = 0;

            idHospedagem = IHospedagem.Cadastrar(hospedagem);


            HospedagemModel hospedagemCadastrada = IHospedagem.ResultadoUnico(idHospedagem);
            Assert.IsNotNull(hospedagemCadastrada);
            Assert.IsInstanceOfType(hospedagemCadastrada, typeof(HospedagemModel));
            Assert.AreEqual(hospedagem, hospedagemCadastrada);

            GerenciadorQuartos IQuarto = new GerenciadorQuartos();
            QuartoModel quarto = IQuarto.ResultadoUnico(idQuarto);
            Assert.IsNotNull(quarto);
            quarto.reservado = true;
            IQuarto.Atualizar(quarto);

            QuartoModel quartoAtualizado = IQuarto.ResultadoUnico(idQuarto);
            Assert.AreEqual(quarto, quartoAtualizado);

            GerenciadorControleClientes IControleCliente = new GerenciadorControleClientes();
            ControleClienteModel controleCliente = new ControleClienteModel();
            int idCliente = 1;

            controleCliente.idCliente = idCliente;
            controleCliente.idHospedagem = idHospedagem;
            controleCliente.idPacoteHospedagem = idPacoteHospedagem;
            controleCliente.isResponsavel = true;
            controleCliente.dataCadastro = DateTime.Now;

            IControleCliente.Cadastrar(controleCliente);

            ControleClienteModel controleClienteCadastrado = IControleCliente.ResultadoUnico(idCliente, idHospedagem);
            Assert.IsNotNull(controleClienteCadastrado);
            Assert.IsInstanceOfType(controleClienteCadastrado, typeof(ControleClienteModel));
            Assert.AreEqual(controleCliente, controleClienteCadastrado);



        }
    }
}
