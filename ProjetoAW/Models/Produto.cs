using System.ComponentModel.DataAnnotations;

namespace ProjetoAW.Models
{
    public class Produto
    {
        [Display(Name = "Código do Produto")]
        public int codProduto { get; set; }
        [Display(Name = "Nome do Produto")]
        public string nomeProduto { get; set; }
        [Display(Name = "Descrição do Produto")]
        public string descProduto { get; set; }
        [Display(Name = "Quantidade em Estoque")]
        public int quantidadeEstoque { get; set; }
        [Display(Name = "Valor unitário")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        public double valorUnitario { get; set; }
        [Display(Name = "Imagem do Produto")]
        public string imagemProduto { get; set; }
        [Display(Name = "Categoria")]
        public int codCategoria { get; set; }
        [Display(Name = "Fornecedor")]
        public int codFornecedor { get; set; }
        [Display(Name = "Espécie")]
        public int codEspecie { get; set; }

        //atributos extras
        public string nomeCategoria { get; set; }
        public string nomeFornecedor { get; set; }
        public string nomeEspecie { get; set; }
    }
}