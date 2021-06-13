using System.ComponentModel.DataAnnotations;

namespace ProjetoAW.Models
{
    public class Funcionario
    {
        [Display(Name = "Código do Funcionário")]
        public int codFunc { get; set; }
        [Display(Name = "Nome do Funcionário")]
        public string nomeFunc { get; set; }
        [Display(Name = "E-mail do Funcionário")]
        public string emailFunc { get; set; }
        [Display(Name = "Usuário do Funcionário")]
        public string usuarioFunc { get; set; }
        [Display(Name = "Senha do Funcionário")]
        public string senhaFunc { get; set; }
        [Display(Name = "Nível")]
        public int nivelFunc { get; set; }
    }
}