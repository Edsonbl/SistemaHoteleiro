using Dominio.Classes;
using Dominio.Classes_Especiais;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IGerenciadorPacoteHospedagens : IRepositorio<PacoteHospedagemModel>
    {
        int Cadastrar(PacoteHospedagemModel pacoteHospedagem);
        void Atualizar(PacoteHospedagemModel pacoteHospedagem);
        void Remover(PacoteHospedagemModel pacoteHospedagem);
        ICollection<PacoteHospedagemModel> ListarReserva();
        ICollection<PacoteHospedagemModel> ListarCheckin();
        ICollection<PacoteHospedagemModel> ListarCheckout();
        PacoteHospedagemModel ResultadoUnico(int idPacoteHospedagem);
        PacoteHospedagemModel ResultadoUnicoReserva(int idReserva);
        PacoteHospedagemModel ResultadoUnicoCheckin(int idReserva);
        PacoteHospedagemModel ResultadoUnicoCheckout(int idReserva);
        ICollection<QuartosLivresReserva> ConsultaPeriodo(string tipoQuartoPesquisa, string pessoasPesquisa, DateTime dataInicio, DateTime dataFim);
        ICollection<QuartosLivresReserva> ListaLivres(string tipoQuartoPesquisa, string pessoasPesquisa, DateTime dataInicio, DateTime dataFim);
        ICollection<QuartosLivresReserva> ListaTiposReservadosPacote(int idPacoteHospedagem);
        ICollection<PacoteHospedagemModel> ListarReservaPorCliente(string nomeCliente);
        ICollection<PacoteHospedagemModel> ListarReservaPorCpf(string cpfCliente);
        ICollection<PacoteHospedagemModel> ListarReservaPorDataInicio(string dataInicio);
        ICollection<PacoteHospedagemModel> ListarReservaPorDataFim(string dataFim);
    }
}
