using Dominio.Classes;
using Dominio.Classes_Especiais;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectHotelWeb.ClassesEspeciais
{
    public static class Entidade
    {
        public static UsuarioModel usuarioAutenticado { get; set; }
        public static List<HistoricoAcessoModel> historicoAcessos { get; set; }
        public static HospedagemModel hospedagemGlobal { get; set; }
    }

    public static class SuperCheckin
    {

        public static List<PessoaModel> pessoasAdicionadas { get; set; }
        public static List<QuartoModel> quartosListados { get; set; }
        public static int pacoteatual { get; set; }
        public static int quantidadePacote { get; set; }


    }
    public static class SuperCheckout
    {

        public static List<PessoaModel> pessoasSelecionadas { get; set; }
        public static List<HospedagemModel> hospedagemSeleionada { get; set; }
        public static List<HospedagemModel> hospedagensIniciais { get; set; }
        public static List<ControleServicoModel> controleServicosSelecionados { get; set; }
        

    }
    public static class PagamentoGlobal
    {
        public static SuperPagamento superPagamento { get; set; }
        public static List<TipoPagamento> pagamentos {get;set;}
        public static decimal valorPago { get; set; }
    }


}