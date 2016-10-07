using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Classes
{
    public class ProdutoModel
    {
        public int idProduto { get; set; }

        [Required(ErrorMessage = "O campo Tamanho é de Preenchimento obrigatório")]
        public string tamanho { get; set; }
        [Required(ErrorMessage = "O campo Quantidade é de Preenchimento obrigatório")]
        public int quantidade { get; set; }
        [Required(ErrorMessage = "O campo Descricao é de Preenchimento obrigatório")]
        public string descricao { get; set; }
        public string observacao { get; set; }
        [Required(ErrorMessage = "O campo Valor é de Preenchimento obrigatório")]
        public decimal valor { get; set; }

        public virtual List<ItemModel> Item { get; set; }
    }
}
