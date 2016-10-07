using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Infraestrutura.Repositorio;
using Dominio.Classes;

namespace ProjectHotelWebTest
{
    [TestClass]
    public class CheckoutTeste
    {

        private TestContext testContextInstance;
        private GerenciadorPacoteHospedagens gPacoteHospedagem { get; set; }
        private PacoteHospedagemModel pacoteHospedagem { get; set; }
        private GerenciadorQuartos gQuartos { get; set; }
        private QuartoModel quarto { get; set; }
        private GerenciadorHospedagens gHospedagens { get; set; }
        private HospedagemModel hospedagem { get; set; }

        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        [TestInitialize]
        public void ObtemPacoteHospedagem()
        {
            int idHospedagem = 0;
            int idQuarto = 0;
            GerenciadorPacoteHospedagens gPacoteHospedagem = new GerenciadorPacoteHospedagens();
            GerenciadorQuartos gQuartos = new GerenciadorQuartos();
            GerenciadorHospedagens gHospedagens = new GerenciadorHospedagens();

            this.gPacoteHospedagem = gPacoteHospedagem;
            this.gQuartos = gQuartos;
            this.gHospedagens = gHospedagens;

            PacoteHospedagemModel pacoteHospedagem = gPacoteHospedagem.ResultadoUnico(4);
            this.pacoteHospedagem = pacoteHospedagem;

            idHospedagem = pacoteHospedagem.Hospedagem[0].idHospedagem;
            HospedagemModel hospedagem = gHospedagens.ResultadoUnico(idHospedagem);
            this.hospedagem = hospedagem;

            idQuarto = hospedagem.idQuarto;
            QuartoModel quarto = gQuartos.ResultadoUnico(idQuarto);
            this.quarto = quarto;

        }

        [TestMethod]
        public void Checkout()
        {
            Assert.IsNotNull(pacoteHospedagem);
            Assert.IsNotNull(hospedagem);
            Assert.IsNotNull(quarto);

            pacoteHospedagem.ativo = true;
            pacoteHospedagem.ControleCliente = pacoteHospedagem.ControleCliente;
            pacoteHospedagem.dataCadastro = pacoteHospedagem.dataCadastro;
            pacoteHospedagem.dataEntrada = pacoteHospedagem.dataEntrada;
            pacoteHospedagem.dataLiberacao = pacoteHospedagem.dataLiberacao;
            pacoteHospedagem.dataLiberacao = pacoteHospedagem.dataSaida;
            pacoteHospedagem.HistoricoAcesso = pacoteHospedagem.HistoricoAcesso;
            pacoteHospedagem.Hospedagem.Remove(hospedagem);
            pacoteHospedagem.Hospedagem = pacoteHospedagem.Hospedagem;
            pacoteHospedagem.idPacoteHospedagem = pacoteHospedagem.idPacoteHospedagem;
            pacoteHospedagem.observacao = pacoteHospedagem.observacao;
            pacoteHospedagem.Pagamento = pacoteHospedagem.Pagamento;
            pacoteHospedagem.tipoPacote = pacoteHospedagem.tipoPacote;
            pacoteHospedagem.TipoQuarto = pacoteHospedagem.TipoQuarto;
            pacoteHospedagem.subTotal = pacoteHospedagem.subTotal;
            pacoteHospedagem.valorTotal = pacoteHospedagem.valorTotal;

            hospedagem.aberto = false;
            hospedagem.ControleCliente = hospedagem.ControleCliente;
            hospedagem.ControleServico = hospedagem.ControleServico;
            hospedagem.dataAbertura = hospedagem.dataAbertura;
            hospedagem.dataLiberacao = hospedagem.dataLiberacao;
            hospedagem.idHospedagem = hospedagem.idHospedagem;
            hospedagem.idPacoteHospedagem = hospedagem.idPacoteHospedagem;
            hospedagem.idQuarto = hospedagem.idQuarto;
            hospedagem.Item = hospedagem.Item;
            hospedagem.observacao = hospedagem.observacao;
            hospedagem.PacoteHospedagem = hospedagem.PacoteHospedagem;
            hospedagem.placaVeiculo = hospedagem.placaVeiculo;
            hospedagem.Quarto = hospedagem.Quarto;
            hospedagem.valorHospedagem = hospedagem.valorHospedagem;

            quarto.ativo = quarto.ativo;
            quarto.capacidade = quarto.capacidade;
            quarto.descricao = quarto.descricao;
            quarto.Hospedagem = quarto.Hospedagem;
            quarto.idQuarto = quarto.idQuarto;
            quarto.idTipoQuarto = quarto.idTipoQuarto;
            quarto.observacao = quarto.observacao;
            quarto.reservado = quarto.reservado;
            quarto.status = "L";
            quarto.TipoQuarto = quarto.TipoQuarto;

            gPacoteHospedagem.Atualizar(pacoteHospedagem);
            gHospedagens.Atualizar(hospedagem);
            gQuartos.Atualizar(quarto);

            PacoteHospedagemModel pacoteHospedagemAtualizado = gPacoteHospedagem.ResultadoUnico(pacoteHospedagem.idPacoteHospedagem);
            HospedagemModel hospedagemAtualizado = gHospedagens.ResultadoUnico(hospedagem.idHospedagem);
            QuartoModel quartoAtualizado = gQuartos.ResultadoUnico(quarto.idQuarto);

            Assert.AreEqual(pacoteHospedagemAtualizado.ativo, true);
            Assert.AreEqual(pacoteHospedagemAtualizado.ControleCliente, pacoteHospedagem.ControleCliente);
            Assert.AreEqual(pacoteHospedagemAtualizado.dataCadastro, pacoteHospedagem.dataCadastro);
            Assert.AreEqual(pacoteHospedagemAtualizado.dataEntrada, pacoteHospedagem.dataEntrada);
            Assert.AreEqual(pacoteHospedagemAtualizado.dataLiberacao, pacoteHospedagem.dataLiberacao);
            Assert.AreEqual(pacoteHospedagemAtualizado.dataLiberacao, pacoteHospedagem.dataSaida);
            Assert.AreEqual(pacoteHospedagemAtualizado.HistoricoAcesso, pacoteHospedagem.HistoricoAcesso);
            Assert.AreEqual(pacoteHospedagemAtualizado.Hospedagem, pacoteHospedagem.Hospedagem);
            Assert.AreEqual(pacoteHospedagemAtualizado.idPacoteHospedagem, pacoteHospedagem.idPacoteHospedagem);
            Assert.AreEqual(pacoteHospedagemAtualizado.observacao, pacoteHospedagem.observacao);
            Assert.AreEqual(pacoteHospedagemAtualizado.Pagamento, pacoteHospedagem.Pagamento);
            Assert.AreEqual(pacoteHospedagemAtualizado.tipoPacote, pacoteHospedagem.tipoPacote);
            Assert.AreEqual(pacoteHospedagemAtualizado.TipoQuarto, pacoteHospedagem.TipoQuarto);
            Assert.AreEqual(pacoteHospedagemAtualizado.subTotal, pacoteHospedagem.subTotal);
            Assert.AreEqual(pacoteHospedagemAtualizado.valorTotal, pacoteHospedagem.valorTotal);

            Assert.AreEqual(hospedagemAtualizado.aberto, false);
            Assert.AreEqual(hospedagemAtualizado.ControleCliente, hospedagem.ControleCliente);
            Assert.AreEqual(hospedagemAtualizado.ControleServico, hospedagem.ControleServico);
            Assert.AreEqual(hospedagemAtualizado.dataAbertura, hospedagem.dataAbertura);
            Assert.AreEqual(hospedagemAtualizado.dataLiberacao, hospedagem.dataLiberacao);
            Assert.AreEqual(hospedagemAtualizado.idHospedagem, hospedagem.idHospedagem);
            Assert.AreEqual(hospedagemAtualizado.idPacoteHospedagem, hospedagem.idPacoteHospedagem);
            Assert.AreEqual(hospedagemAtualizado.idQuarto, hospedagem.idQuarto);
            Assert.AreEqual(hospedagemAtualizado.Item, hospedagem.Item);
            Assert.AreEqual(hospedagemAtualizado.observacao, hospedagem.observacao);
            Assert.AreEqual(hospedagemAtualizado.PacoteHospedagem, hospedagem.PacoteHospedagem);
            Assert.AreEqual(hospedagemAtualizado.placaVeiculo, hospedagem.placaVeiculo);
            Assert.AreEqual(hospedagemAtualizado.Quarto, hospedagem.Quarto);
            Assert.AreEqual(hospedagemAtualizado.valorHospedagem, hospedagem.valorHospedagem);

            Assert.AreEqual(quartoAtualizado.ativo, quarto.ativo);
            Assert.AreEqual(quartoAtualizado.capacidade, quarto.capacidade);
            Assert.AreEqual(quartoAtualizado.descricao, quarto.descricao);
            Assert.AreEqual(quartoAtualizado.Hospedagem, quarto.Hospedagem);
            Assert.AreEqual(quartoAtualizado.idQuarto, quarto.idQuarto);
            Assert.AreEqual(quartoAtualizado.idTipoQuarto, quarto.idTipoQuarto);
            Assert.AreEqual(quartoAtualizado.observacao, quarto.observacao);
            Assert.AreEqual(quartoAtualizado.reservado, quarto.reservado);
            Assert.AreEqual(quartoAtualizado.status, "L");
            Assert.AreEqual(quartoAtualizado.TipoQuarto, quarto.TipoQuarto);

        }
   }
}