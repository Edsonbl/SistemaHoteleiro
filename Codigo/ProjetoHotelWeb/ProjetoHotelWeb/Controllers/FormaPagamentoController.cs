using Dominio.Classes;
using Dominio.Interfaces;
using Infraestrutura.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectHotelWeb.Controllers
{
    public class FormaPagamentoController : Controller
    {

        public IProjectHotel IProjectHotel { get; set; }

        public IGerenciadorFormaPagamentos IFormaPagamento { get; set; }

        // GET: FormaPagamento
        public ActionResult Index()
        {
            List<FormaPagamentoModel> formaPagamento = IFormaPagamento.Listar().ToList<FormaPagamentoModel>(); 
            return View(formaPagamento);
        }

        //Detalhar
        public ActionResult Detalhar(int id)
        {
            FormaPagamentoModel formaPagamento = IFormaPagamento.ResultadoUnico(id);
            return View(formaPagamento);
        }

        //Cadastrar 
        public ActionResult Cadastrar()
        {
            return View();
        }

        [ActionName("CadastrarFormaPagamento")]
        public ActionResult Cadastrar(FormaPagamentoModel formaPagamento){

            //Seta
            formaPagamento.idFormaPagamento = 13;
            formaPagamento.ativo = true;

            //Envia o Objeto para gravação no banco. 
            IFormaPagamento.Cadastrar(formaPagamento);

            return RedirectToAction("Index");

        }

        //Atualizar 
        public ActionResult Atualizar(int id)
        {
            FormaPagamentoModel formaPagamento = IFormaPagamento.ResultadoUnico(id);
            return View(formaPagamento);
        }

        [ActionName("AtualizarFormaPagamento")]
        public ActionResult Atualizar(FormaPagamentoModel formaPagamento)
        {
            IFormaPagamento.Atualizar(formaPagamento);
            return RedirectToAction("Index");
        }

        //Excluir
        public ActionResult Excluir(int id)
        {
            FormaPagamentoModel formaPagamento = IFormaPagamento.ResultadoUnico(id);
            return View(formaPagamento);
        }

        [ActionName("ExcluirFormaPagamento")]
        public ActionResult Excluir(FormaPagamentoModel formaPagamento)
        {
            IFormaPagamento.Remover(formaPagamento);
            return RedirectToAction("Index");
        
        }

    }
}