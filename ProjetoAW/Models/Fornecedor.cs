using System.ComponentModel.DataAnnotations;

namespace ProjetoAW.Models
{
    public class Fornecedor
    {
        public int codFornecedor { get; set; }
        [Display(Name="Nome do Fornecedor")]
        public string nomeFornecedor { get; set; }
        [Display(Name = "E-mail do Fornecedor")]
        public string emailFornecedor { get; set; }
        [Display(Name = "Imagem do Fornecedor")]
        public string imagemFornecedor { get; set; }
    }
}