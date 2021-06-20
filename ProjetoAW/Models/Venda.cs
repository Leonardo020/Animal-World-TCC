using System;
using System.Collections.Generic;
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
        [Display(Name = "Valor Unitário")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        public double valorProduto { get; set; }
        [Display(Name = "Quantidade")]
        public int qtdItensVenda { get; set; }
        [Display(Name = "Valor Total")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        public double valorTotal { get; set; }
        [Display(Name = "Tipo de Pagamento")]
        public int codPagamento { get; set; }
        [Display(Name = "Cliente")]
        public int codCli { get; set; }
        [Display(Name = "Serviço")]
        public int codServico { get; set; }
        [Display(Name = "Produto")]
        public int codProduto { get; set; }

        public List<Pedido> itemPedido = new List<Pedido>();
        
        //atributos extras
        public string nomeCli { get; set; }
        public string logradouro { get; set; }
        public int numero { get; set; }
        public string bairro { get; set; }
        public string estado { get; set; }
        public string cidade { get; set; }
        public string cep { get; set; }

    }
}