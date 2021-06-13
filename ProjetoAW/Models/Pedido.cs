using System.Collections.Generic;

namespace ProjetoAW.Models
{
    public class Pedido
    {
        public int codPedido { get; set; }
        public double valorTotal { get; set; }
        public int quantidadePedido { get; set; }
        List<ItemPedido> itensPedido { get; set; }

        public int codProduto { get; set; }
        public string produto { get; set; }
        public double valorProduto { get; set; }

        public int codCli { get; set; }
    }
}