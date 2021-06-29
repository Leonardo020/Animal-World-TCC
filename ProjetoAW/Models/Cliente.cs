using System.ComponentModel.DataAnnotations;

namespace ProjetoAW.Models
{
    public class Cliente
    {
        [Display(Name = "Código")]
        public int codCli { get; set; }
        [Required(ErrorMessage = "Informe o seu nome")]
        [Display(Name = "Nome")]
        public string nomeCli { get; set; }
        [Required(ErrorMessage = "Informe o seu e-mail")]
        [Display(Name = "E-mail")]
        public string emailCli { get; set; }
        [Required(ErrorMessage = "Informe o seu CPF")]
        [Display(Name = "CPF")]
        [DisplayFormat(DataFormatString = "{0:000\\.000\\.000-00}", ApplyFormatInEditMode = true)]
        public string cpfCli { get; set; }
        [Required(ErrorMessage = "Informe o seu celular")]
        [DisplayFormat(DataFormatString = "{0:(##) #####-####}")]
        [Display(Name = "Celular")]
        public string telefoneCli { get; set; }

        //Endereço do cliente 
        [Required(ErrorMessage = "Informe a rua em que reside")]
        [Display(Name = "Rua")]
        public string logradouroCli { get; set; }
        [Required(ErrorMessage = "Informe o número da residência")]
        [Display(Name = "Número")]
        public int numeroCli { get; set; }
        [Required(ErrorMessage = "Informe o bairro em que reside")]
        [Display(Name = "Bairro")]
        public string bairroCli { get; set; }
        [Required(ErrorMessage = "Informe o CEP da residência")]
        [Display(Name = "CEP")]
        public string cepCli { get; set; }
        [Required(ErrorMessage = "Informe o estado em que reside")]
        [Display(Name = "UF")]
        public string estadoCli { get; set; }
        [Required(ErrorMessage = "Informe a cidade em que reside")]
        [Display(Name = "Cidade")]
        public string cidadeCli { get; set; }

        //sessão de login do cliente
        [Required(ErrorMessage = "Informe um nome de usuário para efetuar login")]
        [Display(Name = "Usuário")]
        public string usuarioCli { get; set; }
        [Required(ErrorMessage = "Informe uma senha para efetuar login")]
        [Display(Name = "Senha")]
        public string senhaCli { get; set; }
        [Display(Name = "Nível")]
        public int nivelCli { get; set; }
    }
}