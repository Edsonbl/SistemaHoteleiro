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
    public class CheckInController : Controller
    {

        public IProjectHotel IProjectHotel { get; set; }
        public IGerenciadorPacoteHospedagens IPacoteHospedagens { get; set; }
        public IGerenciadorHospedagens IHospedagens { get; set; }
        public IGerenciadorPessoas IPessoas { get; set; }
        public IGerenciadorQuartos IQuartos { get; set; }
        public IGerenciadorControleClientes IControleCliente { get; set; }


        
         [Authorize(Roles = "Administrador, Gerente, Recepcionista, Convidado")]
       
        public ActionResult Checkin(string quartosSelect)
        {
            if (SuperCheckin.pessoasAdicionadas != null)
                ViewBag.Pessoas = SuperCheckin.pessoasAdicionadas;
            if (quartosSelect != null)
            {
                SuperCheckin.quartosListados.AddRange(carregarQuartos(quartosSelect));

            }
            ViewBag.Quartos = SuperCheckin.quartosListados;

            ViewBag.Pacote = (PacoteHospedagemModel)TempData["Pacote"];
            return View();
        }

        private List<QuartoModel> carregarQuartos(string quartosSelect)
        {
            string[] quartos = quartosSelect.Split(',');
            QuartoModel quarto;
            List<QuartoModel> ListQuartos = new List<QuartoModel>();
            foreach (var item in quartos)
            {
                quarto = new QuartoModel();
                quarto = IQuartos.ResultadoUnico(Convert.ToInt32(item));
                if (quarto.status.Equals("L"))
                {

                    if (!testarQuartosIguais(quarto))
                        ListQuartos.Add(quarto);



                }
            }
            return ListQuartos;
        }
     
        private bool testarQuartosIguais(QuartoModel quarto)
        {
            int count = 0;

            foreach (var item in SuperCheckin.quartosListados)
            {
                if (item.idQuarto == quarto.idQuarto)
                {

                    count++;
                }
            }



            return count >= 1 ? true : false;
        }

        public ActionResult AtualizarCliente()
        {
            string consulta = Request.Params.Get("Consulta");
            string filtro = Request.Params.Get("filtro");
            adicionarPessoas(consulta, filtro);
            return View("Checkin");
        }

        public ActionResult EscolherCLiente(int id)
        {
            List<PessoaModel> pessoas = new List<PessoaModel>();

            pessoas.Add(IPessoas.ResultadoUnico(id));

            TestarIguais(pessoas);


            return RedirectToAction("Checkin");
        }

        /// <summary>
        /// Adiciona as pessoas que estão cadastrardas no sistema
        /// </summary>
        /// <param name="consulta">Valor no text para a consulta</param>
        /// <param name="filtro">Combo relacionado ao tipo de dados para a consulta</param>
        private void adicionarPessoas(string consulta, string filtro)
        {
            List<PessoaModel> pessoas = FiltroConsulta(consulta, filtro);

            if (pessoas.Count > 1)
            {
                ViewBag.PessoasEscola = pessoas;
            }
            else
            {
                TestarIguais(pessoas);
            }

        }

        public ActionResult ExcluirCliente(int id)
        {

            for (int i = 0; i < SuperCheckin.pessoasAdicionadas.Count; i++)
            {
                if (SuperCheckin.pessoasAdicionadas[i].idPessoa == id)
                {
                    SuperCheckin.pessoasAdicionadas.RemoveAt(i);
                    break;

                }
            }
            return RedirectToAction("Checkin");
        }

        private void TestarIguais(List<PessoaModel> pessoas)
        {

            bool diferente = true;
            if (SuperCheckin.pessoasAdicionadas.Count > 0)
            {

                List<PessoaModel> distinctPessoas = SuperCheckin.pessoasAdicionadas;

                foreach (var item in distinctPessoas)
                {

                    if (item.idPessoa == pessoas[0].idPessoa)
                    {
                        diferente = false;
                        break;
                    }
                }

                if (diferente)
                    SuperCheckin.pessoasAdicionadas.Add(pessoas[0]);

                ViewBag.Pessoas = SuperCheckin.pessoasAdicionadas;
            }
            else
            {
                SuperCheckin.pessoasAdicionadas.AddRange(pessoas);
                ViewBag.Pessoas = SuperCheckin.pessoasAdicionadas;
            }
            ViewBag.Quartos = SuperCheckin.quartosListados;
        }

        /// <summary>
        /// Filtro da consulta para o retorno das pessoas selecionadas
        /// </summary>
        /// <param name="consulta">Valores para o campo de consulta</param>
        /// <param name="filtro">Cpf, Cnpj, Telefone ou Nome</param>
        /// <returns></returns>
        private List<PessoaModel> FiltroConsulta(string consulta, string filtro)
        {
            List<PessoaModel> pessoas = new List<PessoaModel>();
            switch (Convert.ToInt32(filtro))
            {

                case 1:
                    string cpf = consulta.Replace(".", "").Replace("-", "");
                    pessoas = IPessoas.ListarPorCpfCnpj(cpf).ToList<PessoaModel>();
                    break;
                case 2:
                    string cnpj = consulta.Replace(".", "").Replace("-", "").Replace("/", "");
                    pessoas = IPessoas.ListarPorCpfCnpj(cnpj).ToList<PessoaModel>();
                    break;
                case 3:
                    string nome = consulta;
                    pessoas = IPessoas.ListarPorNome(nome).ToList<PessoaModel>();
                    break;
                case 4:
                    string telefone = consulta.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                    pessoas = IPessoas.ListarPorTelefone(telefone).ToList<PessoaModel>();
                    break;

            }
            return pessoas;
        }

        private void removerPessoasCadastrardas(int id)
        {
            List<int> posicoes = new List<int>();
            for (int i = 0; i < SuperCheckin.pessoasAdicionadas.Count; i++)
            {
                if (SuperCheckin.pessoasAdicionadas[i].idPessoa == id)
                {
                    posicoes.Add(i);

                }
            }


            foreach (var item in posicoes)
            {
                SuperCheckin.pessoasAdicionadas.RemoveAt(item);
            }



        }

        public ActionResult RealizarCheckin()
        {

            string[] pessoasSelect = Request.Params.Get("checkPessoa").Split(',');
            int idQuarto = Convert.ToInt32(Request.Params.Get("quarto"));
            decimal valorHospedagemTotal = IQuartos.ResultadoUnico(idQuarto).TipoQuarto.valor;
            DateTime abertura = DateTime.Now;
            double periodo = Convert.ToDouble(Request.Params.Get("Quantidade"));
            DateTime dataLibert = abertura.AddDays(periodo);
            string responsavel = Request.Params.Get("group3");
            string placa = Request.Params.Get("Placa");
            if (SuperCheckin.quantidadePacote == 0)
            {
                SuperCheckin.pacoteatual = CadastrarPacoteHopedagem();

                SuperCheckin.quantidadePacote++;
            }


            int idHosp = CadastrarHospedagem(idQuarto, valorHospedagemTotal, dataLibert, SuperCheckin.pacoteatual, placa);

            CadastrarControleCliente(pessoasSelect, SuperCheckin.pacoteatual, idHosp, Convert.ToInt32(responsavel));

            AtualizarQuartoParaOcupado(idQuarto);

            AtualizarValoresPacoteHospedagem(valorHospedagemTotal, SuperCheckin.pacoteatual);

            foreach (var item in pessoasSelect)
            {
                removerPessoasCadastrardas(Convert.ToInt32(item));
            }

            removerQuartos(idQuarto, pessoasSelect.Length);

            PacoteHospedagemModel pacote = IPacoteHospedagens.ResultadoUnico(SuperCheckin.pacoteatual);

            TempData["Pacote"] = pacote;

            return RedirectToAction("Checkin");
        }

        private void AtualizarValoresPacoteHospedagem(decimal valorHospedagemTotal, int idPacote)
        {
            PacoteHospedagemModel pacoteHospedagem = IPacoteHospedagens.ResultadoUnico(idPacote);
            pacoteHospedagem.subTotal += valorHospedagemTotal;
            pacoteHospedagem.valorTotal += pacoteHospedagem.subTotal;

            IPacoteHospedagens.Atualizar(pacoteHospedagem);
        }

        private void AtualizarQuartoParaOcupado(int idQuarto)
        {
            QuartoModel quarto = IQuartos.ResultadoUnico(idQuarto);
            quarto.status = "O";
            IQuartos.Atualizar(quarto);
        }

        private void CadastrarControleCliente(string[] pessoasSelect, int idPacote, int idHosp, int responsavel)
        {

            bool isResponsible = false;
            for (int i = 0; i < pessoasSelect.Length; i++)
            {
                isResponsible = false;
                if (Convert.ToInt32(pessoasSelect[i]) == responsavel)
                {
                    isResponsible = true;

                }


                ControleClienteModel controle = new ControleClienteModel()
                {
                    idCliente = Convert.ToInt32(pessoasSelect[i]),
                    idHospedagem = idHosp,
                    idPacoteHospedagem = idPacote,
                    isResponsavel = isResponsible,
                    dataCadastro = DateTime.Now
                };
                IControleCliente.Cadastrar(controle);

            }
        }

        private int CadastrarHospedagem(int idQuarto, decimal valorHospedagemTotal, DateTime dataLibert, int idPacote, string placa)
        {
            int idHosp = IHospedagens.Cadastrar(
                 new HospedagemModel()
                 {
                     aberto = true,
                     idPacoteHospedagem = idPacote,
                     dataAbertura = DateTime.Now,
                     dataLiberacao = dataLibert,
                     idQuarto = idQuarto,
                     valorHospedagem = valorHospedagemTotal,
                     placaVeiculo = placa
                 });
            return idHosp;
        }

        private int CadastrarPacoteHopedagem()
        {
            int idPacote = IPacoteHospedagens.Cadastrar(
                new PacoteHospedagemModel()
                {
                    dataCadastro = DateTime.Now,
                    tipoPacote = "C",
                    ativo = true,
                    dataEntrada = DateTime.Now,
                    subTotal = 0,
                    valorTotal = 0
                });
            return idPacote;
        }

        private void removerQuartos(int idQuarto, int quantidadePessoas)
        {

            
                for (int i = 0; i < SuperCheckin.quartosListados.Count; i++)
                {
                    if (SuperCheckin.quartosListados[i].idQuarto == idQuarto)
                    {
                        SuperCheckin.quartosListados.RemoveAt(i);
                        break;

                    }
                }


            

        }
       
        public ActionResult FinalizarCheckin()
        {
            SuperCheckin.pacoteatual = 0;
            SuperCheckin.pessoasAdicionadas = new List<PessoaModel>();
            SuperCheckin.quantidadePacote = 0;
            SuperCheckin.quartosListados = new List<QuartoModel>();

            return RedirectToAction("Index", "Home");
        }
    }
}