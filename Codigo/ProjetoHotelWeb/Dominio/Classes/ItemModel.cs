using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Classes
{
    public class ItemModel
    {
        public int idItem { get; set; }        
        public int idHospedagem { get; set; }
        public int idProduto { get; set; }
        [Required(ErrorMessage="O campo quantidade é de Preenchimento obrigatório")]
        public int quantidade { get; set; }
        public decimal valorTotal{ get; set; }
        public DateTime dataCadastro { get; set; }
        public bool cancelado{ get; set; }


        public virtual ProdutoModel Produto { get; set; }
        public virtual HospedagemModel Hospedagem { get; set; }
    }
}
