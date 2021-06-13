using System;
using System.ComponentModel.DataAnnotations;

namespace ProjetoAW.Models
{
    public class Venda
    {
        [Display(Name = "Código da Venda")]
        public int codVenda { get; set; }
        [Display(Name = "Situação")]
        public string situacao { get; set; }
        [Display(Name = "Data da Venda")]
        public DateTime dataVenda { get; set; }
        [Display(Name = "Tipo de Pagamento")]
        public int codPagamento { get; set; }
        [Display(Name = "Cliente")]
        public int codCli { get; set; }
        [Display(Name = "Serviço")]
        public int codServico { get; set; }
        [Display(Name = "Produto")]
        public int codProduto { get; set; }
    }
}