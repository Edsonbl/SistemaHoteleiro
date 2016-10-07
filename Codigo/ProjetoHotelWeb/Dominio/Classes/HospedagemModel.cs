using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Classes
{
   public class HospedagemModel
    {
       public int idHospedagem { get; set; }
       public int idPacoteHospedagem { get; set; }
       public int idQuarto { get; set; }
       public DateTime dataAbertura { get; set; }
       public DateTime dataLiberacao { get; set; }
       public string placaVeiculo { get; set; }
       public string observacao { get; set; }
       public decimal valorHospedagem { get; set; }
       public bool aberto { get; set; }


       public virtual List<ControleClienteModel> ControleCliente { get; set; }
       public virtual List<ItemModel> Item { get; set; }
       public virtual List<ControleServicoModel> ControleServico { get; set; }
       
       
       
       
       public virtual PacoteHospedagemModel PacoteHospedagem { get; set; }
       public virtual QuartoModel Quarto { get; set; }

       


    }
}
