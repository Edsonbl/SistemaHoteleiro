using Dominio.Classes;
using Dominio.Interfaces;
using Infraestrutura.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectHotelWeb.ClassesEspeciais;

namespace ProjectHotelWeb.Controllers
{
    public class ItemController : Controller
    {
        public IProjectHotel IProjectHotel { get; set; }
        public IGerenciadorItens IItem { get; set; }
        public IGerenciadorProdutos IProduto { get; set; }
        public IGerenciadorHospedagens IHospedagem { get; set; }
        public IGerenciadorPessoas IPessoa { get; set; }
        public HospedagemModel hospedagem { get; set; }

        // GET: Item

        [Authorize(Roles = "Administrador, Gerente, Recepcionista, Convidado")]
        public ActionResult Index()
        {
            List<ItemModel> itens = IItem.Listar().ToList<ItemModel>();
            return View(itens);
        }


        [Authorize(Roles = "Administrador, Gerente, Recepcionista, Convidado")]
        public ActionResult Cadastrar(string idHospedagem)
        {
            if(idHospedagem != null)
            {
                Entidade.hospedagemGlobal = IHospedagem.ResultadoUnico(Convert.ToInt32(idHospedagem.Split('/')[0]));
            }
            else
            {
                int id = Entidade.hospedagemGlobal.idHospedagem;
                Entidade.hospedagemGlobal = new HospedagemModel();
                Entidade.hospedagemGlobal = IHospedagem.ResultadoUnico(id);
            }
            List<ProdutoModel> produtos = IProduto.Listar().ToList<ProdutoModel>();
            ViewBag.Produto = produtos;

            List<ItemModel> itens = IItem.ListarItemPorHspedagem(Entidade.hospedagemGlobal.idHospedagem).ToList<ItemModel>();
            ViewBag.Item = itens;          
            ClienteHospedagem();

            return View();
        }


        [Authorize(Roles = "Administrador, Gerente, Recepcionista, Convidado")]
        public ActionResult Detalhar(int id)
        {
            ItemModel item = IItem.ResultadoUnico(id);
            return View(item);
        }


        [Authorize(Roles = "Administrador, Gerente, Recepcionista, Convidado")]
        public ActionResult Cadastro(string idHospedagem)
        {
            hospedagem = IHospedagem.ResultadoUnico(Convert.ToInt32(idHospedagem));
            ViewBag.idHospedagem = hospedagem.idHospedagem;
            return View();
        }

		private void ClienteHospedagem()
        {
            PessoaModel cliente = new PessoaModel();
            foreach (var c in Entidade.hospedagemGlobal.ControleCliente)
            {
                if (c.isResponsavel)
                {
                    cliente = IPessoa.ResultadoUnico(Convert.ToInt32(c.idCliente));
                    break;
                }
            }
            ViewBag.Hospedagem = Entidade.hospedagemGlobal;
            ViewBag.Cliente = cliente;
        }

        [HttpPost]

        [Authorize(Roles = "Administrador, Gerente, Recepcionista, Convidado")]
        public ActionResult CadastrarItem()
        {
            string id = Request.Params.Get("group1");
            string quantidade = Request.Params.Get("quantidade");

            ProdutoModel produto = IProduto.ResultadoUnico(Convert.ToInt32(id));
            ItemModel item = new ItemModel();
            item.valorTotal = Convert.ToInt32(quantidade ) * produto.valor;
            item.quantidade = Convert.ToInt32(quantidade);
            item.idProduto = produto.idProduto;
            item.cancelado = false;
            item.dataCadastro = DateTime.Now;
            item.idHospedagem = Entidade.hospedagemGlobal.idHospedagem;
            if (produto.quantidade >= Convert.ToInt32(quantidade))
            {
                HospedagemModel hospedagemAtualizada = IHospedagem.ResultadoUnico(Entidade.hospedagemGlobal.idHospedagem);
                IItem.CadastrarNovo(item);
                hospedagemAtualizada.valorHospedagem += item.valorTotal;
                IHospedagem.Atualizar(hospedagemAtualizada);
               
			}

            List<ProdutoModel> produtos = IProduto.Listar().ToList<ProdutoModel>();
            List<ItemModel> itens = IItem.ListarItemPorHspedagem(Entidade.hospedagemGlobal.idHospedagem).ToList<ItemModel>();
			ViewBag.Produto = produtos;
            ViewBag.Item = itens;
			ClienteHospedagem();

            return RedirectToAction("Cadastrar");
	
        }


        [Authorize(Roles = "Administrador, Gerente, Recepcionista, Convidado")]
        public ActionResult Atualizar(int id)
        {
            
            ItemModel item = IItem.ResultadoUnico(id);
            return PartialView(item);
        }

        [ActionName("AtualizarItem")]

        [Authorize(Roles = "Administrador, Gerente, Recepcionista, Convidado")]
        public ActionResult Atualizar(ItemModel item)
        {
            decimal valorNovo;
            decimal valorAntigo;
            HospedagemModel _hospedagem = IHospedagem.ResultadoUnico(item.idHospedagem);
            ItemModel _item = IItem.ResultadoUnico(item.idItem);

            if(item.quantidade > _item.quantidade)
            {
                return RedirectToAction("Cadastrar", new { idHospedagem = _hospedagem.idHospedagem });
            }

            valorAntigo = _item.valorTotal;
            _item.quantidade = item.quantidade;
            valorNovo = (item.quantidade * _item.Produto.valor);
            _item.valorTotal = valorNovo;
            _hospedagem.valorHospedagem += (valorNovo - valorAntigo);
            IHospedagem.Atualizar(_hospedagem);
            IItem.Atualizar(_item);
            return RedirectToAction("Cadastrar", new { idHospedagem = _hospedagem.idHospedagem});
        }


        [Authorize(Roles = "Administrador, Gerente, Recepcionista, Convidado")]
        public ActionResult CancelarItem()
        {
            string[] itensCancelados = Request.Params.Get("checkCancelar").Split(',', ' ');
            string idHospedagem = itensCancelados[1];
            string idItem;
            ItemModel itemCancelado;
            HospedagemModel hospedagemItemCancelado = new HospedagemModel();

            for (int i = 0; i < itensCancelados.Count(); i += 2 )
            {
                idItem = itensCancelados[i];
                itemCancelado = IItem.ResultadoUnico(Convert.ToInt32(idItem));
                itemCancelado.cancelado = true;
                IItem.Atualizar(itemCancelado);

                hospedagemItemCancelado = IHospedagem.ResultadoUnico(Convert.ToInt32(idHospedagem));
                hospedagemItemCancelado.valorHospedagem -= itemCancelado.valorTotal;
                IHospedagem.Atualizar(hospedagemItemCancelado);
            }

            return RedirectToAction("Cadastrar");
        }

    }
}