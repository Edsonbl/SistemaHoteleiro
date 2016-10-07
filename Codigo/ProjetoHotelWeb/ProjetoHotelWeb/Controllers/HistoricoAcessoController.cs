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
    public class HistoricoAcessoController : Controller
    {
        public IProjectHotel IProjectHotel { get; set; }
        public IGerenciadorHistoricoAcessos IHistoricoAcessos { get; set; }


        // GET: HistoricoAcesso
        [Authorize(Roles = "Administrador")]
        public ActionResult Index()
        {
            List<HistoricoAcessoModel> historicoAcesso = IHistoricoAcessos.Listar().ToList<HistoricoAcessoModel>();
            return View(historicoAcesso);
        }
    }
}