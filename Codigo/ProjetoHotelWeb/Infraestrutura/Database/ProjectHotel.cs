
using Dominio.Classes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Database
{
  public  class ProjectHotel: UnidadeTrabalho, IProjectHotel
    {
      public ProjectHotel()


          : base("name=dbtdinewhotelEntities")

        {

        }
        public DbSet<PessoaModel> Funcionarios { get; set; }
        public DbSet<PessoaModel> Clientes { get; set; }
        public DbSet<ServicoModel> Servicos { get; set; }
        public DbSet<ControleServicoModel> ControleServicos { get; set; }

        public DbSet<ProdutoModel> Produtos { get; set; }
        public DbSet<HistoricoModel> Historicos { get; set; }
        public DbSet<CargoModel> Cargos { get; set; }

        public DbSet<HospedagemModel> Hospedagens { get; set; }
        public DbSet<ControleClienteModel> ControleClientes { get; set; }
        public DbSet<UsuarioModel> Usuarios { get; set; }

        public DbSet<PagamentoModel> Pagamentos { get; set; }
        public DbSet<QuartoModel> Quartos { get; set; }
        public DbSet<FormaPagamentoModel> FormaPagamentos { get; set; }

        public DbSet<TipoQuartoModel> TipoQuartos { get; set; }
        public DbSet<ItemModel> Itens { get; set; }
        public DbSet<HistoricoAcessoModel> HistoricoAcessos { get; set; }

        public DbSet<PacoteHospedagemModel> PacoteHospedagens { get; set; }

    }
}
