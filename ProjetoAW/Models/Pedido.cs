using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjetoAW.Models
{
    public class Pedido
    {
        [Key]
        public Guid codPedido { get; set; }
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "Subtotal")]
        public decimal valorTotal { get; set; }
        [Display(Name = "Quantidade comprada")]
        public int quantidadePedido { get; set; }
        public int codProduto { get; set; }
        [Display(Name = "Nome do produto")]
        public string produto { get; set; }
        [Display(Name = "Imagem do produto")]
        public string imagemProduto { get; set; }
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal valorProduto { get; set; }
        public int codCli { get; set; }
        public int codVenda { get; set; }
    }
}