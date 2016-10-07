using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Classes
{
  public  class ControleClienteModel
    {
      public int idCliente { get; set; }
      public int idHospedagem { get; set; }
      public int? idPacoteHospedagem { get; set; }
      public DateTime dataCadastro { get; set; }
      public bool isResponsavel { get; set; }

      public virtual PessoaModel Pessoa { get; set; }
      public virtual HospedagemModel Hospedagem { get; set; }
      public virtual PacoteHospedagemModel PacoteHospedagem { get; set; }
      
    }
}
