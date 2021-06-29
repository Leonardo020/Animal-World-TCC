using System.ComponentModel.DataAnnotations;

namespace ProjetoAW.Models
{
    public class Produto
    {
        [Display(Name = "Código do Produto")]
        public int codProduto { get; set; }
        [Required(ErrorMessage = "Informe o nome do produto")]
        [Display(Name = "Nome do Produto")]
        public string nomeProduto { get; set; }
        [Required(ErrorMessage = "Informe a descrição do produto")]
        [Display(Name = "Descrição do Produto")]
        public string descProduto { get; set; }
        [Required(ErrorMessage = "Informe a quantidade em estoque do produto")]
        [Display(Name = "Quantidade em Estoque")]
        public int quantidadeEstoque { get; set; }
        [Required(ErrorMessage = "Informe o valor do produto")]
        [Display(Name = "Valor unitário")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal valorUnitario { get; set; }
        [Display(Name = "Imagem do Produto")]
        public string imagemProduto { get; set; }
        [Required(ErrorMessage = "Informe o categoria do produto")]
        [Display(Name = "Categoria")]
        public int codCategoria { get; set; }
        [Required(ErrorMessage = "Informe o fornecedor do produto")]
        [Display(Name = "Fornecedor")]
        public int codFornecedor { get; set; }
        [Required(ErrorMessage = "Informe a espécie a que se destina o produto")]
        [Display(Name = "Espécie")]
        public int codEspecie { get; set; }
        //atributos extras
        public string nomeCategoria { get; set; }
        public string nomeFornecedor { get; set; }
        public string nomeEspecie { get; set; }
        public bool isFavorite { get; set; }
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal descontoProd { get; set; }
    }
}