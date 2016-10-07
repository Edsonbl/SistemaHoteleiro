using Dominio.Classes;
using Dominio.Interfaces;
using Dominio.Classes_Especiais;
using Infraestrutura.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
namespace ProjectHotelWeb.Controllers
{
    public class ReservaController : Controller
    {
        public IProjectHotel IProjectHotel { get; set; }
        public IGerenciadorPacoteHospedagens IPacoteHospedagem { get; set; }
        public IGerenciadorTipoQuartos ITipoQuarto { get; set; }
        public IGerenciadorPessoas IPessoas { get; set; }
        public IGerenciadorControleClientes IControleCliente { get; set; }
        public IGerenciadorHospedagens IHospedagem { get; set; }
        public IGerenciadorQuartos IQuarto { get; set; }

        // GET: Reserva

        [Authorize(Roles = "Administrador, Gerente, Recepcionista, Convidado")]
        public ActionResult Index()
        {
            List<PacoteHospedagemModel> pacoteHospedagens = IPacoteHospedagem.ListarReserva().ToList<PacoteHospedagemModel>();
            return View(pacoteHospedagens);
        }

        public ActionResult ConsultaReservaParametro()
        {
            string pesquisa = Request.Params.Get("pesquisa");

            string parametroPesquisa = Request.Params.Get("parametroPesquisa");
            List<PacoteHospedagemModel> pacoteHospedagens = null;
            pesquisa = pesquisa.Replace(".", "").Replace("-", "");
            if (parametroPesquisa.Equals("nome"))
                pacoteHospedagens = IPacoteHospedagem.ListarReservaPorCliente(pesquisa).ToList<PacoteHospedagemModel>();
            if (parametroPesquisa.Equals("cpf"))
                pacoteHospedagens = IPacoteHospedagem.ListarReservaPorCpf(pesquisa).ToList<PacoteHospedagemModel>();
            if (parametroPesquisa.Equals("dataInicio"))
                pacoteHospedagens = IPacoteHospedagem.ListarReservaPorDataInicio(pesquisa).ToList<PacoteHospedagemModel>();
            if (parametroPesquisa.Equals("dataFim"))
                pacoteHospedagens = IPacoteHospedagem.ListarReservaPorDataFim(pesquisa).ToList<PacoteHospedagemModel>();
            return PartialView(pacoteHospedagens);
        }


        [Authorize(Roles = "Administrador, Gerente, Recepcionista, Convidado")]
        public ActionResult Detalhar(int id)
        {
            PacoteHospedagemModel pacoteHospedagem = IPacoteHospedagem.ResultadoUnicoReserva(id);
            List<QuartosLivresReserva> reservasCliente = IPacoteHospedagem.ListaTiposReservadosPacote(id).ToList();
            ViewBag.reservasPacote = reservasCliente;
            return View(pacoteHospedagem);
        }


        [Authorize(Roles = "Administrador, Gerente, Recepcionista")]
        public ActionResult Cadastrar()
        {
            List<TipoQuartoModel> tipoQuartos = ITipoQuarto.Listar().ToList<TipoQuartoModel>();
            ViewBag.tipoQuarto = tipoQuartos;

            if (TempData["quartos"] != null)
            {
                ViewBag.quartosLivres = (List<QuartosLivresReserva>)TempData["quartos"];
                ViewBag.dataInicioFim = (List<DateTime>)TempData["datas"];
            }

            if (TempData["dadosView"] != null)
            {
                int idPacoteHospedagem = Convert.ToInt32(TempData["idPacote"]);
                List<object> objetosCliente = (List<object>)TempData["dadosView"];
                ViewBag.dadosView = objetosCliente;
                //    ViewBag.listaReservaCliente = ITipoQuarto.ListaTiposReservadosCliente(Convert.ToInt32(objetosCliente[3]));
                ViewBag.idPacoteHospedagem = idPacoteHospedagem;

            }

            return View();

        }


        [Authorize(Roles = "Administrador, Gerente, Recepcionista")]
        public ActionResult ReservaCliente(PacoteHospedagemModel _pacoteHospedagem)
        {
            string cpf = Request.Params.Get("cpf");
            string nomeCliente = Request.Params.Get("nome");
            string telefone = Request.Params.Get("telefone");
            string idPacote = Request.Params.Get("idPacote");
            List<object> dadosView = new List<object>();
            dadosView.Add(cpf);
            dadosView.Add(nomeCliente);
            dadosView.Add(telefone);
            cpf = cpf.Replace(".", "").Replace("-", "");
            CultureInfo culture = new CultureInfo("pt-BR");
            DateTime dataInicio = Convert.ToDateTime(Request.Params.Get("dataInicio"));
            DateTime dataFim = Convert.ToDateTime(Request.Params.Get("dataFim"));
            int idPacoteHospedagem = 0;
            if (TempData["idPacote"] == null)
            {
                idPacoteHospedagem = CadastraPacoteHospedagem(dataInicio, dataFim);
            }
            else
            {
                idPacoteHospedagem = Convert.ToInt32(TempData["idPacote"]);
            }
            List<int> idsQuartos;
            string[] comboBoxQuantidadeQuartos = Request.Params.Get("quantidade").Split(',', ' ');
            string[] checkBoxQuartosSelecionados = Request.Params.Get("checkQuartos").Split(',', ' ');
            List<PessoaModel> pessoaResultado = IPessoas.ListarPorCpfCnpj(cpf).ToList();
            int idCliente;
            if (pessoaResultado.Count == 0)
            {
                CadastraCliente(cpf, nomeCliente, telefone);
            }
            idCliente = IPessoas.ListarPorCpfCnpj(cpf).ToList()[0].idPessoa;
            dadosView.Add(idCliente);

            double periodo = (dataFim - dataInicio).TotalDays;
            for (int i = 0; i < checkBoxQuartosSelecionados.Count(); i += 2)
            {
                string idTipoQuartoCheck = checkBoxQuartosSelecionados[i];
                string capacidadeQuarto = checkBoxQuartosSelecionados[i + 1];
                idsQuartos = ITipoQuarto.ListaQuartosTipo(idTipoQuartoCheck, capacidadeQuarto).ToList();
                for (int j = 0; j < comboBoxQuantidadeQuartos.Count(); j += 2)
                {
                    string idTipoQuartoCombo = comboBoxQuantidadeQuartos[j];
                    if (idTipoQuartoCheck.Equals(idTipoQuartoCombo))
                    {
                        int indiceQuarto = 0;
                        string quantidadeQuatosSelecionada = comboBoxQuantidadeQuartos[j + 1];
                        for (int k = 0; k < Convert.ToInt32(quantidadeQuatosSelecionada); k++)
                        {
                            TipoQuartoModel tipoQuarto = ITipoQuarto.ResultadoUnico(Convert.ToInt32(idTipoQuartoCombo));
                            decimal valorHospedagem = tipoQuarto.valor * Convert.ToDecimal(periodo);
                            PacoteHospedagemModel pacoteHospedagem = IPacoteHospedagem.ResultadoUnico(idPacoteHospedagem);
                            pacoteHospedagem.valorTotal = pacoteHospedagem.valorTotal + valorHospedagem;
                            pacoteHospedagem.subTotal = pacoteHospedagem.valorTotal;
                            IPacoteHospedagem.Atualizar(pacoteHospedagem);
                            int idHospedagem = CadastraHospedagem(dataInicio, dataFim, idsQuartos, valorHospedagem, idPacoteHospedagem, indiceQuarto);
                            AtualizaQuartoReservado(idsQuartos[indiceQuarto]);
                            indiceQuarto++;
                            CadastraControleCliente(idCliente, idPacoteHospedagem, idHospedagem);
                        }
                    }
                }
            }
            TempData["idPacote"] = idPacoteHospedagem;
            TempData["dadosView"] = dadosView;
            List<QuartosLivresReserva> reservasCliente = IPacoteHospedagem.ListaTiposReservadosPacote(idPacoteHospedagem).ToList();
            return PartialView(reservasCliente);
        }

        private void AtualizaQuartoReservado(int indiceQuarto)
        {
            QuartoModel quarto = IQuarto.ResultadoUnico(indiceQuarto);
            quarto.reservado = true;
            IQuarto.Atualizar(quarto);
        }


        [Authorize(Roles = "Administrador, Gerente, Recepcionista")]
        private int CadastraHospedagem(DateTime dataInicio, DateTime dataFim, List<int> idsQuartos, decimal valorHospedagem, int idPacoteHospedagem, int indiceQuarto)
        {
            int idHospedagem = IHospedagem.Cadastrar(
            new HospedagemModel()
            {
                aberto = true,
                idPacoteHospedagem = idPacoteHospedagem,
                dataAbertura = dataInicio,
                dataLiberacao = dataFim,
                idQuarto = idsQuartos[indiceQuarto],
                valorHospedagem = valorHospedagem
            });
            return idHospedagem;
        }


        [Authorize(Roles = "Administrador, Gerente, Recepcionista")]
        private void CadastraControleCliente(int idCliente, int idPacoteHospedagem, int idHospedagem)
        {
            ControleClienteModel controle = new ControleClienteModel()
            {
                idCliente = idCliente,
                idHospedagem = idHospedagem,
                idPacoteHospedagem = idPacoteHospedagem,
                isResponsavel = true,
                dataCadastro = DateTime.Now
            };
            IControleCliente.Cadastrar(controle);
        }


        [Authorize(Roles = "Administrador, Gerente, Recepcionista")]
        private void CadastraCliente(string cpf, string nome, string telefone)
        {
            PessoaModel cliente;
            cliente = new PessoaModel();
            cliente.cpfCnpj = cpf;
            cliente.nome = nome;
            cliente.telefoneMovel = telefone;
            cliente.ativo = true;
            cliente.isFuncionario = false;
            cliente.dataCadastro = DateTime.Now;
            cliente.estado = "";
            IPessoas.Cadastrar(cliente);
        }


        [Authorize(Roles = "Administrador, Gerente, Recepcionista")]
        private int CadastraPacoteHospedagem(DateTime dataInicio, DateTime dataFim)
        {
            int idPacoteHospedagem = IPacoteHospedagem.Cadastrar(
                new PacoteHospedagemModel()
                {
                    dataCadastro = DateTime.Now,
                    tipoPacote = "R",
                    ativo = true,
                    dataLiberacao = DateTime.Now,
                    dataEntrada = dataInicio,
                    dataSaida = dataFim,
                    subTotal = 0,
                    valorTotal = 0
                });
            return idPacoteHospedagem;
        }


        [Authorize(Roles = "Administrador, Gerente, Recepcionista, Convidado")]
        public ActionResult Consultar()
        {

            CultureInfo culture = new CultureInfo("pt-BR");

            DateTime dataInicio = Convert.ToDateTime(Request.Params.Get("dataInicio"), culture);
            DateTime dataFim = Convert.ToDateTime(Request.Params.Get("dataFim"), culture);
            string tipoQuarto = Request.Params.Get("tipoQuarto");
            string numeroPessoas = Request.Params.Get("pessoas");
            string numeroQuartos = Request.Params.Get("quartos");

            List<QuartosLivresReserva> quartosLivresReserva = IPacoteHospedagem.ListaLivres(tipoQuarto, numeroPessoas, dataInicio, dataFim).ToList<QuartosLivresReserva>();
            List<DateTime> dataInicioFim = new List<DateTime>();
            dataInicioFim.Add(dataInicio);
            dataInicioFim.Add(dataFim);
            List<object> dadosView = new List<object>();

            TempData["quartos"] = quartosLivresReserva;
            TempData["datas"] = dataInicioFim;

            ViewBag.dataInicioFim = dataInicioFim;

            return PartialView(quartosLivresReserva);

        }


        [Authorize(Roles = "Administrador, Gerente, Recepcionista")]
        public ActionResult Atualizar(int id)
        {
            PacoteHospedagemModel pacoteHospedagem = IPacoteHospedagem.ResultadoUnicoReserva(id);
            return View(pacoteHospedagem);
        }

        [ActionName("AtualizarReserva")]
        [Authorize(Roles = "Administrador, Gerente, Recepcionista")]
        public ActionResult Atualizar(PacoteHospedagemModel pacoteHospedagem)
        {
            pacoteHospedagem.ativo = true;
            pacoteHospedagem.tipoPacote = "R";
            IPacoteHospedagem.Atualizar(pacoteHospedagem);
            return RedirectToAction("Index");
        }


        [Authorize(Roles = "Administrador, Gerente, Recepcionista")]
        public ActionResult Excluir(int id)
        {
            PacoteHospedagemModel pacoteHospedagem = IPacoteHospedagem.ResultadoUnicoReserva(id);
            return View(pacoteHospedagem);
        }

        [ActionName("ExcluirReserva")]
        [Authorize(Roles = "Administrador, Gerente, Recepcionista")]
        public ActionResult Excluir(PacoteHospedagemModel pacoteHospedagem)
        {
            IPacoteHospedagem.Remover(pacoteHospedagem);
            return RedirectToAction("Index");
        }
    }
}