using System.ComponentModel.DataAnnotations;

namespace ProjetoAW.Models
{
    public class Servico
    {
        [Display(Name = "Código do Serviço")]
        public int codServico { get; set; }
        [Display(Name = "Nome do Serviço")]
        public string nomeServico { get; set; }
        [Display(Name = "Valor do Serviço")]
        public double valorServico { get; set; }
        [Display(Name = "Descrição do Serviço")]
        public string descServico { get; set; }
        [Display(Name = "Imagem do Serviço")]
        public string imagemServico { get; set; }
        [Display(Name = "Tempo estimado de Serviço")]
        public string horaServico { get; set; }
    }
}