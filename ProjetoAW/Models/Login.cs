using System.ComponentModel.DataAnnotations;

namespace ProjetoAW.Models
{
    public class Login
    {
        [Display(Name = "Código do Login")]
        public int codLogin { get; set; }
        [Required(ErrorMessage = "Informe o usuário")]
        [Display(Name = "Usuário")]
        public string usuarioLogin { get; set; }
        [Required(ErrorMessage = "Informe a senha")]
        [Display(Name = "Senha")]
        public string senhaLogin { get; set; }
        [Display(Name = "Tipo")]
        public int nivelLogin { get; set; }
        public int codCli { get; set; }
        public int codFunc { get; set; }
        public int codGerente { get; set; }
        public string nomeCli { get; set; }
        public string nomeFunc { get; set; }
        public string nomeGerente { get; set; }

    }
}