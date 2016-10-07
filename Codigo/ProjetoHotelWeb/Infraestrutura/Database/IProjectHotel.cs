
using Dominio.Classes;
using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Database
{
    public interface IProjectHotel : IUnidadeTrabalho
    {
        DbSet<PessoaModel> Funcionarios { get; }
        DbSet<PessoaModel> Clientes { get; set; }
        DbSet<ServicoModel> Servicos { get; }
        DbSet<ControleServicoModel> ControleServicos { get; }

        DbSet<ProdutoModel> Produtos { get; }
        DbSet<HistoricoModel> Historicos { get; }
        DbSet<CargoModel> Cargos { get; }

        DbSet<HospedagemModel> Hospedagens { get; }
        DbSet<ControleClienteModel> ControleClientes { get;  }
        DbSet<UsuarioModel> Usuarios { get; }

        DbSet<PagamentoModel> Pagamentos { get; }
        DbSet<QuartoModel> Quartos { get; }
        DbSet<FormaPagamentoModel> FormaPagamentos { get; }

        DbSet<TipoQuartoModel> TipoQuartos { get; }
        DbSet<ItemModel> Itens { get; }
        DbSet<HistoricoAcessoModel> HistoricoAcessos { get; }

        DbSet<PacoteHospedagemModel> PacoteHospedagens { get;  }
    }
}
