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
    public class ProdutoController : Controller
    {
        public IProjectHotel IProjectHotel { get; set; }
        public IGerenciadorProdutos IProduto { get; set; }

        // GET: Produto

        [Authorize(Roles = "Administrador, Gerente, Recepcionista, Convidado")]
        public ActionResult Index()
        {
            List<ProdutoModel> produtos = IProduto.Listar().ToList<ProdutoModel>();
            return View(produtos);
        }


        [Authorize(Roles = "Administrador, Gerente, Recepcionista, Convidado")]
        public ActionResult Detalhar(int id)
        {
            ProdutoModel produto = IProduto.ResultadoUnico(id);
            return View(produto);
        }


        [Authorize(Roles = "Administrador, Gerente")]
        public ActionResult Cadastrar()
        {
            return View();
        }

        [ActionName("CadastrarProduto")]
        [Authorize(Roles = "Administrador, Gerente")]
        public ActionResult Cadastrar(ProdutoModel produto)
        {
            IProduto.Cadastrar(produto);
            return RedirectToAction("Index");
        }


        [Authorize(Roles = "Administrador, Gerente")]
        public ActionResult Atualizar(int id)
        {
            ProdutoModel produtos = IProduto.ResultadoUnico(id);
            return View(produtos);
        }

        [ActionName("AtualizarProduto")]

        [Authorize(Roles = "Administrador, Gerente")]
        public ActionResult Atualizar(ProdutoModel produto)
        {
            IProduto.Atualizar(produto);
            return RedirectToAction("Index");
        }


        [Authorize(Roles = "Administrador, Gerente")]
        public ActionResult Excluir(int id)
        {
            ProdutoModel produto = IProduto.ResultadoUnico(id);
            return View(produto);
        }

        [ActionName("ExcluirProduto")]

        [Authorize(Roles = "Administrador, Gerente")]
        public ActionResult Excluir(ProdutoModel produto)
        {
            IProduto.Remover(produto);
            return RedirectToAction("Index");
        }
    }
}