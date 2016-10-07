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
    public class QuartoController : Controller
    {
        public IProjectHotel IProjectHotel { get; set; }

        public IGerenciadorQuartos IQuarto { get; set; }

        public IGerenciadorTipoQuartos ITipoQuarto { get; set; } 

        //Metodos da class 
        public ActionResult Index()
        {
            List<QuartoModel> quarto = IQuarto.Listar().ToList<QuartoModel>();
            return View(quarto);
        }

        public ActionResult Detalhar(int id)
        {
            QuartoModel quarto = IQuarto.ResultadoUnico(id);
            return View(quarto);
        }

        //Cadastrar
        public ActionResult Cadastrar()
        {

            List<TipoQuartoModel> tiposQuartos = ITipoQuarto.Listar().ToList<TipoQuartoModel>();
            ViewBag.Tipos = tiposQuartos;

            return View();
        }

        [ActionName("CadastrarQuarto")]
        public ActionResult Cadastrar(QuartoModel quarto)
        {
            quarto.ativo = true;
            quarto.idTipoQuarto = Convert.ToInt32(Request.Params.Get("Tipo"));
            quarto.status = Request.Params.Get("Status");

            IQuarto.Cadastrar(quarto);
            return RedirectToAction("Index");
        }

        //Atualizar
        public ActionResult Atualizar(int id)
        {
            QuartoModel quarto = IQuarto.ResultadoUnico(id);

            ViewBag.TipoQuarto = ITipoQuarto.Listar();

            return View(quarto);
        }

        [ActionName("AtualizarQuarto")]
        public ActionResult Atualizar(QuartoModel quarto)
        {
            quarto.idTipoQuarto = Convert.ToInt32(Request.Params.Get("tipoquarto"));

            IQuarto.Atualizar(quarto);
            return RedirectToAction("Index");
        }

        //Excluir
        public ActionResult Excluir(int id)
        {
            QuartoModel quarto = IQuarto.ResultadoUnico(id);
            return View(quarto);
        }

        [ActionName("ExcluirQuarto")]
        public ActionResult Excluir(QuartoModel quarto)
        {
            QuartoModel newQuarto = IQuarto.ResultadoUnico(quarto.idQuarto);

            if (newQuarto != null)
            {
                newQuarto.ativo = false;
                IQuarto.Atualizar(newQuarto);
            }
            else
                return null;
            
            return RedirectToAction("Index");
        }

    }
}