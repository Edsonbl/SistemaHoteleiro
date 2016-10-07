using Dominio.Classes;
using Dominio.Interfaces;
using Infraestrutura.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectHotelWeb.ClassesEspeciais;
using Dominio.Classes_Especiais;

namespace ProjectHotelWeb.Controllers
{
    public class PagamentoController : Controller
    {

        public IProjectHotel IProjectHotel { get; set; }
        public IGerenciadorPagamentos IPagamento { get; set; }
        public IGerenciadorFormaPagamentos IFormaPagamentos { get; set; }
        public IGerenciadorHospedagens IHospedagens { get; set; }
        public IGerenciadorPacoteHospedagens IPacoteHospedagens { get; set; }
        public IGerenciadorQuartos IQuartos { get; set; }
        public IGerenciadorControleClientes IControleClientes { get; set; }

        public IGerenciadorPessoas IPessoas { get; set; }
        // GET: Pagamento
        public ActionResult Index()
        {
            List<PagamentoModel> pagamento = IPagamento.Listar().ToList<PagamentoModel>();
            return View(pagamento);
        }

        //Detalhar 
        public ActionResult Detalhar(int id)
        {
            PagamentoModel pagamento = IPagamento.ResultadoUnico(id);
            return View(pagamento);
        }

        //Cadastrar
        public ActionResult Cadastrar()
        {
            return View();
        }

        [ActionName("CadastrarPagamento")]
        public ActionResult Cadastrar(PagamentoModel pagamento)
        {
            IPagamento.Cadastrar(pagamento);
            return RedirectToAction("Index");
        }

        //Atualizar 
        public ActionResult Atualizar(int id)
        {
            PagamentoModel pagamento = IPagamento.ResultadoUnico(id);
            return View(pagamento);
        }

        [ActionName("AtualizarPagamento")]
        public ActionResult Atualizar(PagamentoModel pagamento)
        {
            IPagamento.Atualizar(pagamento);
            return RedirectToAction("Index");
        }

        //Excluir
        public ActionResult Excluir(int id)
        {
            PagamentoModel pagamento = IPagamento.ResultadoUnico(id);
            return View(pagamento);
        }

        [ActionName("ExcluirPagamento")]
        public ActionResult Excluir(PagamentoModel pagamento)
        {
            IPagamento.Remover(pagamento);
            return RedirectToAction("Index");
        }


        public ActionResult Pagamento(string hospedagensSelect)
        {





            decimal valorTotal = 0;
            if (hospedagensSelect != null)
            {
                string[] hospedagens = hospedagensSelect.Split(',');

                foreach (var item in hospedagens)
                {

                    HospedagemModel hospedagem = new HospedagemModel();
                    hospedagem = null;
                    hospedagem = IHospedagens.ResultadoUnico(Convert.ToInt32(item));
                    if (!PagamentoGlobal.superPagamento.hospedagensIniciais.Contains(hospedagem))
                        PagamentoGlobal.superPagamento.hospedagensIniciais.Add(hospedagem);

                }



                foreach (var hospedagem in PagamentoGlobal.superPagamento.hospedagensIniciais)
                {
                    foreach (var cCliente in hospedagem.ControleCliente)
                    {
                        if (cCliente.isResponsavel)
                        {

                            if (!PagamentoGlobal.superPagamento.pessoasSelecionadas.Contains(cCliente.Pessoa))
                                PagamentoGlobal.superPagamento.pessoasSelecionadas.Add(cCliente.Pessoa);
                            break;
                        }
                    }
                }
                foreach (var item in PagamentoGlobal.superPagamento.hospedagensIniciais)
                {
                    valorTotal += item.valorHospedagem;
                }

                PagamentoGlobal.superPagamento.valorTotal = valorTotal;
            }





            ViewBag.Hospedagens = PagamentoGlobal.superPagamento.hospedagensIniciais;
            ViewBag.Clientes = PagamentoGlobal.superPagamento.pessoasSelecionadas;
            ViewBag.FormaPagamento = IFormaPagamentos.Listar();
            ViewBag.Valor = PagamentoGlobal.superPagamento.valorTotal;
            ViewBag.FormaPagamentoAtuais = PagamentoGlobal.pagamentos;
            ViewBag.ValorPago = PagamentoGlobal.valorPago;
            ViewBag.Troco = PagamentoGlobal.valorPago - PagamentoGlobal.superPagamento.valorTotal;
            return View();

        }

        public ActionResult pagamentoAvista()
        {

            string idFormaPagamento = Request.Params.Get("group1");
            string valorPagoView = Request.Params.Get("valorPago");

            switch (Convert.ToInt32(idFormaPagamento))
            {
                case 1:

                    break;

                default:
                    break;
            }




            return View();
        }


        public ActionResult AdicionarPagamento()
        {

            string idFormaPagamento = Request.Params.Get("group1");
            string valorPagoView = Request.Params.Get("valorPago");


            switch (Convert.ToInt32(idFormaPagamento))
            {
                case 1:

                    if (PagamentoGlobal.pagamentos.Count > 0)
                    {

                        foreach (var item in PagamentoGlobal.pagamentos)
                        {
                            if (item.formaPagamentosAtuais.tipo.Equals("A"))
                            {
                                item.valorPago += Convert.ToDecimal(valorPagoView);
                                break;
                            }

                        }

                    }
                    else
                    {

                        PagamentoGlobal.pagamentos.Add(new TipoPagamento()
                        {
                            formaPagamentosAtuais = IFormaPagamentos.ResultadoUnicoTipo("A")[0],
                            valorPago = Convert.ToDecimal(valorPagoView)
                        });
                    }
                    break;

                default:
                    break;
            }



            PagamentoGlobal.valorPago += Convert.ToDecimal(valorPagoView);

            return View("Pagamento");
        }

        public ActionResult FinalizarPagamento()
        {

            if (PagamentoGlobal.valorPago >= PagamentoGlobal.superPagamento.valorTotal)
            {
                foreach (var hospedagem in PagamentoGlobal.superPagamento.hospedagensIniciais)
                {
                    foreach (var item in PagamentoGlobal.pagamentos)
                    {
                        PersistirPagamento(item, hospedagem.idPacoteHospedagem);
                    }


                    PacoteHospedagemModel pacote = IPacoteHospedagens.ResultadoUnico(hospedagem.idPacoteHospedagem);
                    pacote.valorTotal -= hospedagem.valorHospedagem;

                    IPacoteHospedagens.Atualizar(pacote);
                }
            }

            LiberarHospedagem();
            LimparAtributos();

            return RedirectToAction("Index", "Home");
        }
        private void PersistirPagamento(TipoPagamento tipo, int idPacote)
        {
            PagamentoModel pagamento = new PagamentoModel();
            List<PagamentoModel> pagamentos = new List<PagamentoModel>();

            for (int i = 0; i < tipo.formaPagamentosAtuais.numeroMaximoParcela; i++)
            {
                pagamento = new PagamentoModel();
                pagamento.dataCadastro = DateTime.Now;
                pagamento.dataPrevista = DateTime.Now;
                pagamento.dataPrevista.AddMonths(i);

                pagamento.idFormaPagamento = tipo.formaPagamentosAtuais.idFormaPagamento;
                pagamento.valorParcela = (PagamentoGlobal.valorPago / tipo.formaPagamentosAtuais.numeroMaximoParcela);
                pagamento.status = "P";
                pagamento.idPacoteHospedagem = idPacote;
                IPagamento.Cadastrar(pagamento);
            }

        }

        private void LiberarHospedagem()
        {

            foreach (var hospedagem in PagamentoGlobal.superPagamento.hospedagensIniciais)
            {
                HospedagemModel hosp = new HospedagemModel();
                hosp = IHospedagens.ResultadoUnico(hospedagem.idHospedagem);

                hosp.dataLiberacao = DateTime.Now;
                hosp.aberto = false;

                IHospedagens.Atualizar(hosp);


            }
            foreach (var hospedagem in PagamentoGlobal.superPagamento.hospedagensIniciais)
            {



                QuartoModel quarto = new QuartoModel();
                quarto = IQuartos.ResultadoUnico(hospedagem.idQuarto);
                quarto.status = "L";
                IQuartos.Atualizar(quarto);

            }
        }

        private void LimparAtributos()
        {
            PagamentoGlobal.superPagamento = new SuperPagamento();

            PagamentoGlobal.pagamentos = new List<TipoPagamento>();

            PagamentoGlobal.valorPago = 0;
        }
    }
}