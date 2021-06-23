namespace ProjetoAW.Models
{
    public class ItemPedido
    {
        public int codItemPedido { get; set; }
        public string nomeItemPedido { get; set; }
        public int quantidadeItem { get; set; }
        public decimal valorParcial { get; set; }
    }
}