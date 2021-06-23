﻿using System;
using System.ComponentModel.DataAnnotations;

namespace ProjetoAW.Models
{
    public class Agenda
    {
        [Display(Name = "Código da Agenda")]
        public int codAgenda { get; set; }
        [Required(ErrorMessage = "Insira uma data de agendamento")]
        [Display(Name = "Data do Agendamento")]
        public DateTime dataAgenda { get; set; }
        [Required(ErrorMessage = "Insira um horário de agendamento")]
        [Display(Name = "Horário do Agendamento")]
        public string horarioAgenda { get; set; }
        [Display(Name = "Serviço")]
        public int codServico { get; set; }
        [Display(Name = "Cliente")]
        public int codCli { get; set; }
        [Required(ErrorMessage = "Informe um animal para realizar o agendamento do serviço")]
        [Display(Name = "Animal")]
        public int codAnimal { get; set; }

        public string situacaoAgenda { get; set; }

        //atributos extras
        public string nomeServico { get; set; }
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        public decimal valorServico { get; set; }
        public string nomeCli { get; set; }
        public string nomeAnimal { get; set; }
        public int confAgendamento { get; set; }
    }
}