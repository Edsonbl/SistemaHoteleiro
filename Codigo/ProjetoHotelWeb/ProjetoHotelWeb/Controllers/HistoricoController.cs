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
    public class HistoricoController : Controller
    {

        public IProjectHotel IProjectHotel { get; set; }
        public IGerenciadorHistoricos IHistorico { get; set; }

        // GET: Historico
        [Authorize(Roles = "Administrador")]
        public ActionResult Index()
        {
            List<HistoricoModel> historicos = IHistorico.Listar().ToList<HistoricoModel>();
            return View(historicos);
        }
    }
}