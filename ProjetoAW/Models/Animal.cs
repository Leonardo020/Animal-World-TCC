using System.ComponentModel.DataAnnotations;

namespace ProjetoAW.Models
{
    public class Animal
    {
        [Display(Name = "Código do Animal")]
        public int codAnimal { get; set; }
        [Display(Name = "Nome do Animal")]
        public string nomeAnimal { get; set; }
        [Display(Name = "Descrição do Animal")]
        public string descricaoAnimal { get; set; }
        [Display(Name = "Idade")]
        public int idade { get; set; }
        [Display(Name = "Temperamento")]
        public string temperamento { get; set; }
        [Display(Name = "Imagem do Animal")]
        public string imagemAnimal { get; set; }
        [Display(Name = "Sexo")]
        public string sexo { get; set; }
        [Display(Name = "Espécie")]
        public int codEspecie { get; set; }
        [Display(Name = "Raça")]
        public int codRaca { get; set; }
        [Display(Name = "Cliente")]
        public int codCli { get; set; }
        [Display(Name = "Porte")]
        public int codPorte { get; set; }

        //atributos extras
        public string nomeEspecie { get; set; }
        public string nomeRaca { get; set; }
        public string nomeCli { get; set; }
        public string nomePorte { get; set; }
    }
}