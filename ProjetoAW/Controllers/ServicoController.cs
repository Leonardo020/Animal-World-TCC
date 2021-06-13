using ProjetoAW.Models;
using ProjetoAW.Repositorio;
using System;
using System.Web.Mvc;

namespace ProjetoAW.Controllers
{
    public class ServicoController : Controller
    {
        actionsServico acServico = new actionsServico();
        actionsAgenda acAgenda = new actionsAgenda();
        public ActionResult TelaAgendamento(int idServico)
        {
            var servico = acServico.selecionaServicoPorId(idServico);
            return View(servico);
        }

        [HttpPost]
        public ActionResult CadAgendamento(Agenda agenda)
        {
            try
            {
                acAgenda.verificaAgendamento(agenda);
                if (agenda.confAgendamento == 1)
                {
                    ViewBag.Message = "Já existe um agendamento marcado nesta data e hora.";
                }
                else
                {
                    acAgenda.realizaAgendamento(agenda);
                    ViewBag.Message = "Agendamento realizado com sucesso.";
                }
            }

            catch (Exception e)
            {
                ViewBag.Message = "Houve um erro ao tentar cadastrar: " + e;
            }
            return View();
        }

        public ActionResult ListaAgendamento()
        {
            var agendamentos = acAgenda.consultaAgendamento();
            return View(agendamentos);
        }
    }
}