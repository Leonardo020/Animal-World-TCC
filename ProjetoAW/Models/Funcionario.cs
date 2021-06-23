using System.ComponentModel.DataAnnotations;

namespace ProjetoAW.Models
{
    public class Funcionario
    {
        [Display(Name = "Código do Funcionário")]
        public int codFunc { get; set; }
        [Required(ErrorMessage = "Informe o nome do funcionário")]
        [Display(Name = "Nome do Funcionário")]
        public string nomeFunc { get; set; }
        [Required(ErrorMessage = "Informe o e-mail do funcionário")]
        [Display(Name = "E-mail do Funcionário")]
        public string emailFunc { get; set; }
        [Required(ErrorMessage = "Informe o usuário para que o funcionário possa efetuar login")]
        [Display(Name = "Usuário do Funcionário")]
        public string usuarioFunc { get; set; }
        [Required(ErrorMessage = "Informe a senha para que o funcionário possa efetuar login")]
        [Display(Name = "Senha do Funcionário")]
        public string senhaFunc { get; set; }
        [Display(Name = "Nível")]
        public int nivelFunc { get; set; }
    }
}