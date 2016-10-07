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
    public class CargoController : Controller
    {

        public IProjectHotel iiprojetoHotel { get; set; }
        public IGerenciadorCargos cargos { get; set; }
        // GET: Cargo


        [Authorize(Roles = "Administrador, Gerente")]
        public ActionResult Index()
        {
            
            List<CargoModel> car = cargos.Listar().ToList<CargoModel>();

            
                

            

            return View(car);
        }

        [Authorize(Roles = "Administrador, Gerente")]
        public ActionResult Insert()
        {
         
            return View();
        }


        [Authorize(Roles = "Administrador, Gerente")]
        public ActionResult Create(CargoModel cargo)
        {
            cargos.Cadastrar(cargo);
            return RedirectToAction("Index");
        }



    }
}