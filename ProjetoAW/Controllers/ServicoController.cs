using ProjetoAW.Models;
using ProjetoAW.Repositorio;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace ProjetoAW.Controllers
{
    public class ServicoController : Controller
    {
        actionsServico acServico = new actionsServico();
        actionsAgenda acAgenda = new actionsAgenda();
        actionsAnimal acAnimal = new actionsAnimal();

        Agenda agenda = new Agenda();

        public void carregaAnimais()
        {
            var sessao = Convert.ToInt32(Session["Cliente"]);
            var animais = acAnimal.carregaAnimais(sessao);

            ViewBag.animais = new SelectList(animais, "Value", "Text");
        }
        public ActionResult TelaAgendamento(int idServico)
        {
            carregaAnimais();
            var servico = acServico.selecionaServicoPorId(idServico);
            agenda.codServico = servico.codServico;
            agenda.nomeServico = servico.nomeServico;
            agenda.valorServico = servico.valorServico;
            return View(agenda);
        }

        [HttpPost]
        public ActionResult CadAgendamento(Agenda agenda)
        {
            carregaAnimais();
            try
            {
                acAgenda.verificaAgendamento(agenda);
                if (agenda.confAgendamento == 1)
                {
                    TempData["warning"] = "Já existe um agendamento marcado nesta data e hora.";
                }
                else
                {
                    var sessao = Convert.ToInt32(Session["Cliente"]);
                    agenda.codCli = sessao;
                    agenda.codAnimal = Convert.ToInt32(Request["animais"]);
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

        public ActionResult CadServico()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CadServico(Servico servico, HttpPostedFileBase file, FormCollection frm)
        {
            try
            {
                string arquivo = Path.GetFileName(file.FileName);
                string file2 = "/img/" + arquivo;
                string path = Path.Combine(Server.MapPath("~/img"), arquivo);
                file.SaveAs(path);
                servico.imagemServico = file2;
                servico.descServico = frm["descServico"];
                acServico.cadastraServico(servico);
                TempData["success"] = "Cadastro do serviço realizado com sucesso";
            }
            catch
            {
                TempData["error"] = "Ocorreu um erro ao cadastrar o serviço";
            }
            return View();
        }

        public ActionResult AlterarServico(int id)
        {
            var servico = acServico.selecionaServicoPorId(id);
            return View(servico);
        }

        [HttpPost]
        public ActionResult AlterarServico(Servico servico, HttpPostedFileBase file, FormCollection frm)
        {
            try
            {
                if (file != null)
                {
                    string arquivo = Path.GetFileName(file.FileName);
                    string file2 = "/img/" + arquivo;
                    string path = Path.Combine(Server.MapPath("~/img"), arquivo);
                    file.SaveAs(path);
                    servico.imagemServico = file2;
                }
                servico.descServico = frm["descServico"];
                acServico.atualizaServico(servico);
                TempData["success"] = "Alteração do serviço realizada com sucesso";
            }
            catch (Exception e)
            {
                TempData["error"] = "Ocorreu um erro ao tentar alterar o serviço";
            }
            var servicoAtualizado = acServico.selecionaServicoPorId(servico.codServico);
            ViewBag.desc = servicoAtualizado.descServico;
            ViewBag.imagem = servicoAtualizado.imagemServico;
            return View(servicoAtualizado);
        }


        public ActionResult CancelaAgendamento(int id)
        {
            acAgenda.cancelaAgendamento(id);
            return RedirectToAction("ListaAgendamento", "Cliente");
        }

        public ActionResult ListaServicos()
        {
            var servicos = acServico.consultaServico();
            return View(servicos);
        }
    }
}