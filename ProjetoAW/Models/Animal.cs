using System.ComponentModel.DataAnnotations;

namespace ProjetoAW.Models
{
    public class Animal
    {
        [Display(Name = "Código do Animal")]
        public int codAnimal { get; set; }
        [Required(ErrorMessage = "Informe o nome do seu pet")]
        [Display(Name = "Nome do Animal")]
        public string nomeAnimal { get; set; }
        [Display(Name = "Descrição do Animal")]
        public string descricaoAnimal { get; set; }
        [Display(Name = "Idade")]
        [Required(ErrorMessage = "Informe a idade do seu pet")]
        public int idade { get; set; }
        [Required(ErrorMessage = "Informe o temperamento do seu pet")]
        [Display(Name = "Temperamento")]
        public string temperamento { get; set; }
        [Display(Name = "Imagem do Animal")]
        public string imagemAnimal { get; set; }
        [Required(ErrorMessage = "Informe o sexo do seu pet")]
        [Display(Name = "Sexo")]
        public string sexo { get; set; }
        [Required(ErrorMessage = "Informe a espécie do seu pet")]
        [Display(Name = "Espécie")]
        public int codEspecie { get; set; }
        [Required(ErrorMessage = "Informe a raça do seu pet")]
        [Display(Name = "Raça")]
        public int codRaca { get; set; }
        [Display(Name = "Cliente")]
        public int codCli { get; set; }
        [Required(ErrorMessage = "Informe o porte do seu pet")]
        [Display(Name = "Porte")]
        public int codPorte { get; set; }

        //atributos extras
        public string nomeEspecie { get; set; }
        public string nomeRaca { get; set; }
        public string nomeCli { get; set; }
        public string nomePorte { get; set; }
    }
}