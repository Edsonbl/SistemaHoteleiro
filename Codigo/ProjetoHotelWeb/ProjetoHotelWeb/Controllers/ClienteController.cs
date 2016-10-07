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
    public class ClienteController : Controller
    {

        // GET: Funcionario
        public IProjectHotel IAcademic { get; set; }

        public IGerenciadorPessoas iPessoas { get; set; }


        // GET: Cliente
        [Authorize(Roles = "Administrador, Gerente, Recepcionista, Convidado")]
        public ActionResult Index()
        {




            return View();
        }

        [Authorize(Roles = "Administrador, Gerente, Recepcionista, Convidado")]
        public ActionResult Consultar()
        {

            string pesquisa = Request.Params.Get("pesquisa");

            string parametroPesquisa = Request.Params.Get("parametroPesquisa");
            List<PessoaModel> pessoas = null;
            pesquisa = pesquisa.Replace(".", "").Replace("-", "");
            if (parametroPesquisa.Equals("nome"))
                pessoas = iPessoas.ListarPorNome(pesquisa).ToList<PessoaModel>();
            if (parametroPesquisa.Equals("cpf"))
                pessoas = iPessoas.ListarPorCpfCnpj(pesquisa).ToList<PessoaModel>();
            if (parametroPesquisa.Equals("telefone"))
                pessoas = iPessoas.ListarPorTelefone(pesquisa).ToList<PessoaModel>();

            return PartialView(pessoas);

        }


        [Authorize(Roles = "Administrador, Gerente, Recepcionista, Convidado")]


        public ActionResult Cadastrar()
        {



            return View();

        }


        [ActionName("CadastrarCliente")]
        [Authorize(Roles = "Administrador, Gerente, Recepcionista, Convidado")]

        public ActionResult Cadastrar(PessoaModel pessoa)
        {

            pessoa.isFuncionario = false;
            pessoa.dataCadastro = DateTime.Now;
            pessoa.ativo = true;
            pessoa.dataNascimento = Convert.ToDateTime(Request.Params.Get("Nascimento"));
            pessoa.estadoCivil = Request.Params.Get("estadoCivil");
            string movel = pessoa.telefoneMovel.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
            pessoa.telefoneMovel = movel;
            pessoa.estado = Request.Params.Get("Estado");
            pessoa.sexo = Request.Params.Get("sexo");
            iPessoas.Cadastrar(pessoa);

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrador, Gerente, Recepcionista, Convidado")]

        public ActionResult Atualizar(int id)
        {


            PessoaModel pessoa = iPessoas.ResultadoUnico(id);

            return View(pessoa);
        }
        [ActionName("AtualizarCliente")]
        [Authorize(Roles = "Administrador, Gerente, Recepcionista, Convidado")]

        public ActionResult Atualizar(PessoaModel pessoa)
        {

            pessoa.ativo = Convert.ToBoolean(Request.Params.Get("ativo"));
            pessoa.isFuncionario = Convert.ToBoolean(Request.Params.Get("funcionario"));
            pessoa.dataNascimento = Convert.ToDateTime(Request.Params.Get("Nascimento"));
            pessoa.estadoCivil = Request.Params.Get("estadoCivil");
            pessoa.estado = Request.Params.Get("Estado");
            pessoa.sexo = Request.Params.Get("sexo");
            iPessoas.Atualizar(pessoa);

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrador, Gerente, Recepcionista, Convidado")]

        public ActionResult Excluir(int id)
        {
            PessoaModel pessoa = iPessoas.ResultadoUnico(id);
            return View(pessoa);
        }


        [Authorize(Roles = "Administrador, Gerente, Recepcionista, Convidado")]
        [ActionName("ExcluirCliente")]
        public ActionResult Excluir(PessoaModel pessoa)
        {
            PessoaModel newPessoa = iPessoas.ResultadoUnico(pessoa.idPessoa);
            if (newPessoa != null)
            {
                newPessoa.ativo = false;
                iPessoas.Atualizar(newPessoa);
            }
            else
                return null;

            return RedirectToAction("Index");
        }


        [Authorize(Roles = "Administrador, Gerente, Recepcionista, Convidado")]
        public ActionResult Detalhar(int id)
        {
            PessoaModel pessoa = iPessoas.ResultadoUnico(id);


            return View(pessoa);
        }


    }
}