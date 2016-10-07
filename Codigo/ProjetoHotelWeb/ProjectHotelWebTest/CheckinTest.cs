using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dominio.Classes;
using Infraestrutura.Repositorio;

namespace ProjectHotelWebTest
{


    [TestClass]
    public class CheckinTest
    {
        private TestContext testContextInstance;
        PacoteHospedagemModel pacote;
        QuartoModel quarto;
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        [TestInitialize]
        public void ObtemPacote()
        {
            GerenciadorQuartos IQuarto = new GerenciadorQuartos();
            
            quarto = new Quarto();
            quarto = IQuarto.ResultadoUnico(1);

            if (quarto.status.Equals("L"))
            {
                GerenciadorPacoteHospedagens gPacote = new GerenciadorPacoteHospedagens();
                 
                pacote = new PacoteHospedagemModel();
                pacote.valorTotal = 0;
                pacote.subTotal = 0;
                pacote.dataCadastro = DateTime.Now;
                pacote.ativo = true;
                pacote.tipoPacote = "C";
                pacote.idPacoteHospedagem = gPacote.Cadastrar(pacote);
            }


        }



        [TestMethod]
        public void RealizarCheckinValido()
        {
            if (quarto.status.Equals("L"))
            {
            GerenciadorPacoteHospedagens gPacote = new GerenciadorPacoteHospedagens();
            GerenciadorHospedagens gHospedagens = new GerenciadorHospedagens();
            GerenciadorControleClientes gControleCliente = new GerenciadorControleClientes();
            GerenciadorQuartos gQuartos = new GerenciadorQuartos();
            HospedagemModel hospedagem = new HospedagemModel();
            ControleClienteModel controleCliente = new ControleClienteModel();
            GerenciadorPessoas gPessoas = new GerenciadorPessoas();
            hospedagem.idPacoteHospedagem = pacote.idPacoteHospedagem;
            hospedagem.idQuarto = quarto.idQuarto;
            hospedagem.dataAbertura = DateTime.Now;
            hospedagem.dataLiberacao = hospedagem.dataAbertura.AddDays(5);
            hospedagem.valorHospedagem = quarto.TipoQuarto.valor * 5;
            hospedagem.aberto = true;
           
          
                try
                {
                    hospedagem.idHospedagem = gHospedagens.Cadastrar(hospedagem);
                }
                catch (Exception e)
                {
                    Assert.IsInstanceOfType(e, typeof(Exception));

                }


                HospedagemModel hospedagemCadastrado = gHospedagens.ResultadoUnico(hospedagem.idHospedagem);
                Assert.IsNotNull(hospedagemCadastrado);
                Assert.IsInstanceOfType(hospedagemCadastrado, typeof(HospedagemModel));
                Assert.AreEqual(hospedagem, hospedagemCadastrado);

                controleCliente.idHospedagem = hospedagem.idHospedagem;
                controleCliente.idPacoteHospedagem = pacote.idPacoteHospedagem;
                controleCliente.idCliente = gPessoas.ResultadoUnico(1).idPessoa;
                controleCliente.isResponsavel = true;
                controleCliente.dataCadastro = DateTime.Now;
               
                try
                {
                    gControleCliente.Cadastrar(controleCliente);
                }
                catch (Exception e)
                {
                    Assert.IsInstanceOfType(e, typeof(Exception));

                }
                ControleClienteModel newControleCliente = gControleCliente.ResultadoUnico(controleCliente.idCliente,controleCliente.idHospedagem);
                Assert.IsNotNull(newControleCliente);
                Assert.IsInstanceOfType(newControleCliente, typeof(ControleClienteModel));
                Assert.AreEqual(controleCliente, newControleCliente);

                quarto = new QuartoModel();
               quarto = gQuartos.ResultadoUnico(hospedagem.idQuarto);
                quarto.status = "O";
                
                try
                {
                    gQuartos.Atualizar(quarto);
                }
                catch (Exception e)
                {
                    Assert.IsInstanceOfType(e, typeof(Exception));

                }


                QuartoModel QuartoAtual = gQuartos.ResultadoUnico(quarto.idQuarto);
                Assert.AreEqual(QuartoAtual.status, "O");
                

                pacote.valorTotal = pacote.subTotal = hospedagem.valorHospedagem;
                try
                {
                    gPacote.Atualizar(pacote);
                }
                catch (Exception e)
                {
                    Assert.IsInstanceOfType(e, typeof(Exception));

                }
               
                PacoteHospedagemModel pacoteHospedagem = gPacote.ResultadoUnico(pacote.idPacoteHospedagem);
                Assert.IsNotNull(pacoteHospedagem);
                Assert.IsInstanceOfType(pacoteHospedagem, typeof(PacoteHospedagemModel));
                Assert.AreEqual(pacote, pacoteHospedagem);

            }
           

        }





    }
}
