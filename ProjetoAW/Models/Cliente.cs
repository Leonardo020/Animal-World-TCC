using System.ComponentModel.DataAnnotations;

namespace ProjetoAW.Models
{
    public class Cliente
    {
        [Display(Name = "Código")]
        public int codCli { get; set; }
        [Display(Name = "Nome")]
        public string nomeCli { get; set; }
        [Display(Name = "E-mail")]
        public string emailCli { get; set; }
        [Display(Name = "CPF")]
        public string cpfCli { get; set; }
        [Display(Name = "Telefone")]
        public string telefoneCli { get; set; }

        //Endereço do cliente 
        [Display(Name = "Rua")]
        public string logradouroCli { get; set; }
        [Display(Name = "Número")]
        public int numeroCli { get; set; }
        [Display(Name = "Bairro")]
        public string bairroCli { get; set; }
        [Display(Name = "CEP")]
        public string cepCli { get; set; }
        [Display(Name = "UF")]
        public string estadoCli { get; set; }
        [Display(Name = "Cidade")]
        public string cidadeCli { get; set; }

        //sessão de login do cliente
        [Display(Name = "Usuário")]
        public string usuarioCli { get; set; }
        [Display(Name = "Senha")]
        public string senhaCli { get; set; }
        [Display(Name = "Nível")]
        public int nivelCli { get; set; }
    }
}