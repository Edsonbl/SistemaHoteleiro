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
    public class FuncionarioController : Controller
    {
        // GET: Funcionario
        public IProjectHotel IAcademic { get; set; }

        public IGerenciadorPessoas iPessoas { get; set; }

        public IGerenciadorCargos iCargos { get; set; }
        // GET: Pessoa
        public ActionResult Index()
        {




            return View();
        }
        [Authorize(Roles = "Administrador, Gerente")]
        public ActionResult Consultar()
        {

            string pesquisa = Request.Params.Get("pesquisa");

            string parametroPesquisa = Request.Params.Get("parametroPesquisa");
            List<PessoaModel> pessoas = null;
            pesquisa = pesquisa.Replace(".", "").Replace("-", "");
            if (parametroPesquisa.Equals("nome"))
                pessoas = iPessoas.ListarFuncionarioPorNome(pesquisa).ToList<PessoaModel>();
            if (parametroPesquisa.Equals("cpf"))
                pessoas = iPessoas.ListarFuncionarioPorCpfCnpj(pesquisa).ToList<PessoaModel>();
            if (parametroPesquisa.Equals("telefone"))
                pessoas = iPessoas.ListarFuncionarioPorTelefone(pesquisa).ToList<PessoaModel>();

            return PartialView(pessoas);

        }

        [Authorize(Roles = "Administrador, Gerente")]
        public ActionResult Cadastrar()
        {

            List<CargoModel> cargos = iCargos.Listar().ToList<CargoModel>();
            ViewBag.Cargos = cargos;
            if (cargos.Count > 0)
            {
                return View();
            }
            else
            {
                ModelState.AddModelError("", "Não exite cargos cadastrados no sistema");
            }
            return RedirectToAction("Insert", "Cargo");
        }


        [ActionName("CadastrarFuncionario")]
        [Authorize(Roles = "Administrador, Gerente")]
        public ActionResult Cadastrar(PessoaModel pessoa)
        {

            pessoa.isFuncionario = true;
            pessoa.dataCadastro = DateTime.Now;
            pessoa.ativo = true;
            pessoa.dataNascimento = Convert.ToDateTime(Request.Params.Get("Nascimento"));
            pessoa.estadoCivil = Request.Params.Get("estadoCivil");
            pessoa.idCargo = Convert.ToInt32(Request.Params.Get("Cargo"));
            pessoa.estado = Request.Params.Get("Estado");
            pessoa.sexo = Request.Params.Get("sexo");
            iPessoas.Cadastrar(pessoa);

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrador, Gerente")]
        public ActionResult Atualizar(int id)
        {


            PessoaModel pessoa = iPessoas.ResultadoUnicoFuncionario(id);
            //  pessoa.salario = Convert.ToDecimal( pessoa.salario.ToString().Replace(',', '.'));
            ViewBag.Cargos = iCargos.Listar();
            return View(pessoa);
        }

        [ActionName("AtualizarFuncionario")]
        [Authorize(Roles = "Administrador, Gerente")]
        public ActionResult Atualizar(PessoaModel pessoa)
        {
            pessoa.idCargo = Convert.ToInt32(Request.Params.Get("Cargo"));
            pessoa.ativo = Convert.ToBoolean(Request.Params.Get("ativo"));
            pessoa.isFuncionario = Convert.ToBoolean(Request.Params.Get("funcionario"));
            pessoa.dataNascimento = Convert.ToDateTime(Request.Params.Get("Nascimento"));
            pessoa.estadoCivil = Request.Params.Get("estadoCivil");
            pessoa.estado = Request.Params.Get("Estado");
            pessoa.sexo = Request.Params.Get("sexo");
            iPessoas.Atualizar(pessoa);

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrador, Gerente")]
        public ActionResult Excluir(int id)
        {
            PessoaModel pessoa = iPessoas.ResultadoUnicoFuncionario(id);
            return View(pessoa);
        }



        [ActionName("ExcluirFuncionario")]
        [Authorize(Roles = "Administrador, Gerente")]
        public ActionResult Excluir(PessoaModel pessoa)
        {
            PessoaModel newPessoa = iPessoas.ResultadoUnicoFuncionario(pessoa.idPessoa);
            if (newPessoa != null)
            {
                newPessoa.isFuncionario = false;
                iPessoas.Atualizar(newPessoa);
            }
            else
                return null;

            return RedirectToAction("Index");
        }


        [Authorize(Roles = "Administrador, Gerente")]
        public ActionResult Detalhar(int id)
        {
            PessoaModel pessoa = iPessoas.ResultadoUnicoFuncionario(id);


            return View(pessoa);
        }





    }
}