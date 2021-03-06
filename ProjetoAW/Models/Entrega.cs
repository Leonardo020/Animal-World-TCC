using System;
using System.ComponentModel.DataAnnotations;

namespace ProjetoAW.Models
{
    public class Entrega
    {
        [Display(Name = "Número da Venda")]
        public int codPedido { get; set; }
        [Display(Name = "Código da Entrega")]
        public int codEntrega { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data da Entrega")]
        public DateTime dataEntrega { get; set; }
        [Display(Name = "Frete")]
        public decimal valorEntrega { get; set; }
        [Display(Name = "Animal")]
        public int codAnimal { get; set; }
        [Display(Name = "Cliente")]
        public int codCli { get; set; }

        //atributos extras
        public string nomeAnimal { get; set; }
        public string nomeCli { get; set; }

    }
}