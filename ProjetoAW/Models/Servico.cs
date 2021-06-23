using System.ComponentModel.DataAnnotations;

namespace ProjetoAW.Models
{
    public class Servico
    {
        [Display(Name = "Código do Serviço")]
        public int codServico { get; set; }
        [Required(ErrorMessage = "Informe o nome do serviço")]
        [Display(Name = "Nome do Serviço")]
        public string nomeServico { get; set; }
        [Required(ErrorMessage = "Informe o valor do serviço")]
        [Display(Name = "Valor do Serviço")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal valorServico { get; set; }
        [Required(ErrorMessage = "Informe a descrição do serviço")]
        [Display(Name = "Descrição do Serviço")]
        public string descServico { get; set; }
        [Display(Name = "Imagem do Serviço")]
        public string imagemServico { get; set; }
        [Required(ErrorMessage = "Informe o tempo estimado do serviço")]
        [Display(Name = "Tempo estimado de Serviço")]
        public string horaServico { get; set; }
    }
}