using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Dominio.Classes;
using Dominio.Interfaces;
using Infraestrutura.Database;
using ProjectHotelWeb.ClassesEspeciais;
using Dominio.Classes_Especiais;
using Microsoft.AspNet.Identity;
using ProjectHotelWeb.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using IdentitySample.Models;



namespace ProjectHotelWeb.Controllers
{
    public class ControleServicoController : Controller
    {
        public IProjectHotel IProjectHotel { get; set; }
        public IGerenciadorControleServicos IControleServicos { get; set; }
        public IGerenciadorPessoas IPessoas { get; set; }
        public IGerenciadorControleClientes ICliente { get; set; }
        public IGerenciadorHospedagens IHospedagens { get; set; }
        public IGerenciadorQuartos IQuartos { get; set; }
        public IGerenciadorServicos IServicos { get; set; }
       
        public UserManager<User> userManager;

        public ControleServicoController ()
        {
            UserDbContext db = new UserDbContext();
            UserStore<User> userStore = new UserStore<User>(db);
            userManager = new UserManager<User>(userStore);

        }
        
        // GET: ControleServico
        [Authorize(Roles = "Administrador, Gerente, Recepcionista, Convidado")]
        public ActionResult Index(string idHospedagem)
        {
            int hospedagem = Convert.ToInt32(idHospedagem.Split('/')[0]);
            //List<ServicoHospedagem> servicosHospedagens = IControleServicos.ListarServicoHospedagem(hospedagem).ToList();
            List<ControleServicoModel> servicosDaHospedagem = IControleServicos.ListarServicosIndividualmente(hospedagem).ToList<ControleServicoModel>();
            if (servicosDaHospedagem.Count > 0)
            {
                ViewBag.Quarto = servicosDaHospedagem.ElementAt(0).Hospedagem.Quarto;
            }
            ViewBag.Cliente = ICliente.ResultadoUnicoHospedagem(hospedagem);
            List<PessoaModel> func = IPessoas.ListarFuncionario().ToList<PessoaModel>();
            ViewBag.Funcionarios = IPessoas.ListarFuncionario().ToList<PessoaModel>();
            ViewBag.Servicos = IServicos.Listar().ToList<ServicoModel>();
            ViewBag.Hospedagem = hospedagem;
            return View(servicosDaHospedagem);
        }

        [Authorize(Roles = "Administrador, Gerente, Recepcionista, Convidado")]
        public ActionResult VincularServicoHospedagem(string idHospedagem)
        {
            HospedagemModel hospedagem;
            ControleClienteModel cliente;
            PessoaModel pessoaCliente;
            QuartoModel quarto;

            hospedagem = IHospedagens.ResultadoUnico(Convert.ToInt32(idHospedagem.Split('/')[0]));
            cliente = ICliente.ResultadoUnicoHospedagem(hospedagem.idHospedagem);
            List<ServicoModel> servicos = IServicos.Listar().ToList<ServicoModel>();
            pessoaCliente = IPessoas.ResultadoUnico(cliente.Pessoa.idPessoa);
            quarto = IQuartos.ResultadoUnico(hospedagem.idQuarto);


            TempData["hospedagem"] = hospedagem;


            ViewBag.Hospedagem = hospedagem;
            ViewBag.Cliente = cliente;
            ViewBag.Servicos = servicos;
            ViewBag.Pessoa = pessoaCliente;
            ViewBag.Quarto = quarto;


            return View();



        }

        [HttpPost]
        public ActionResult AdicionarServico()
        {

            string idServico = Request.Params.Get("listaServico");
            ServicoModel servico = IServicos.ResultadoUnico(Convert.ToInt32(idServico));
            return PartialView(servico);

        }
        
        public ActionResult VincularServico (string idServico)
        {
            
            HospedagemModel hospedagem = (HospedagemModel) TempData["hospedagem"];
           
            int codigoServico = Convert.ToInt32(idServico.Trim());
            int quantidade = Convert.ToInt32(Request.Params.Get("quantidade")); 
            
            DateTime dataInicio = Convert.ToDateTime(Request.Params.Get("dataInicio"));
            ControleServicoModel controleServico = new ControleServicoModel();
            controleServico.idHospedagem = hospedagem.idHospedagem;
            controleServico.idServico = codigoServico;
            string usuarioId = User.Identity.GetUserId();
            User usuario = userManager.FindById(usuarioId);
            controleServico.idFuncionario = usuario.idFuncionario;
            controleServico.dataAbertura = dataInicio;
            controleServico.dataCadastro = DateTime.Now;
            controleServico.cancelado = false;

            for (int i = 0; i < quantidade; i++ )
            {
                IControleServicos.Cadastrar(controleServico);
            }

            TempData["hospedagem"] = hospedagem;
            
            List<ServicoHospedagem> servicosHospedagens = IControleServicos.ListarServicoHospedagem(hospedagem.idHospedagem).ToList();

            return PartialView(servicosHospedagens);

        }

        [Authorize(Roles = "Administrador, Gerente, Recepcionista, Convidado")]
        public ActionResult RealizarServico (int idControleServico, int idHospedagem)
        {
            ControleServicoModel servico = IControleServicos.ResultadoUnico(idControleServico);
            servico.dataLiberacao = DateTime.Now;
            IControleServicos.Atualizar(servico);
            return RedirectToAction("Index", new { idHospedagem = idHospedagem });
        }

        [Authorize(Roles = "Administrador, Gerente, Recepcionista, Convidado")]
        
        public ActionResult CancelarServico(int idControleServico, int idHospedagem)
        {
            ControleServicoModel servico = IControleServicos.ResultadoUnico(idControleServico);
            if (servico.dataLiberacao == null)
            {
                servico.cancelado = true;
                IControleServicos.Atualizar(servico);
            }

            return RedirectToAction("Index", new { idHospedagem = idHospedagem });
        }
        
    }
}