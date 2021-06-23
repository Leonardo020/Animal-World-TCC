using System.ComponentModel.DataAnnotations;

namespace ProjetoAW.Models
{
    public class Desconto
    {
        public int codDesconto { get; set; }
        [Display(Name ="Desconto(em porcentagem)")]
        [Required(ErrorMessage = "Informe o desconto do produto")]
        public decimal desconto { get; set; }
        public bool isDesconto { get; set; }
    }
}