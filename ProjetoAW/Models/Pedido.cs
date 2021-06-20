using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjetoAW.Models
{
    public class Pedido
    {
        [Key]
        public Guid codPedido { get; set; }
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        public double valorTotal { get; set; }
        public int quantidadePedido { get; set; }
        public int codProduto { get; set; }
        public string produto { get; set; }
        public string imagemProduto { get; set; }
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        public double valorProduto { get; set; }
        public int codCli { get; set; }
        public int codVenda { get; set; }
    }
}