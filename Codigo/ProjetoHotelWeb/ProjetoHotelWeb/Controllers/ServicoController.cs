using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Infraestrutura.Repositorio;
using Dominio.Interfaces;
using Dominio.Classes;


namespace ProjectHotelWeb.Controllers
{
    public class ServicoController : Controller
    {

        public IGerenciadorServicos IServicos { get; set; }


        // GET: Servico

        [Authorize(Roles = "Administrador, Gerente, Recepcionista, Convidado")]
        public ActionResult Index()
        {
            List<ServicoModel> servicos = IServicos.Listar().ToList<ServicoModel>();

            return View(servicos);
        }


        [Authorize(Roles = "Administrador, Gerente")]
        public ActionResult Cadastrar()
        {

            return View();
        }


        [Authorize(Roles = "Administrador, Gerente")]
        public ActionResult CadastrarServico(ServicoModel servico)
        {

            servico.observacao = Request.Params.Get("Observacao");
            servico.ativo = true;
            IServicos.Cadastrar(servico);
            return RedirectToAction("Index");
        }



        [Authorize(Roles = "Administrador, Gerente")]
        public ActionResult Atualizar(int id)
        {

            ServicoModel servico = IServicos.ResultadoUnico(id);


            return View(servico);
        }


        [Authorize(Roles = "Administrador, Gerente")]
        public ActionResult AtualizarServico(ServicoModel servico)
        {

            servico.observacao = Request.Params.Get("Observacao");
            IServicos.Atualizar(servico);
            return RedirectToAction("Index");
        }



        [Authorize(Roles = "Administrador, Gerente, Recepcionista, Convidado")]
        public ActionResult Detalhar(int id)
        {
            ServicoModel servico = IServicos.ResultadoUnico(id);

            return View(servico);
        }
    }
}