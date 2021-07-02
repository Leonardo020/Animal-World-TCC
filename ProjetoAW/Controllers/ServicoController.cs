using ProjetoAW.Models;
using ProjetoAW.Repositorio;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X.PagedList;

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
            if ((Session["usuario"] == null) || (Session["senha"] == null))
            {
                TempData["warning"] = "Esteja logado para realizar um agendamento!";
                return RedirectToAction("Login", "Home");
            }
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

            catch
            {
                TempData["danger"] = "Houve um erro ao tentar fazer um agendamento";
            }
            return RedirectToAction("ListaAgendamento", "Cliente");
        }

        public ActionResult ListaAgendamento()
        {
            if ((Session["usuario"] == null) || (Session["senha"] == null))
            {
                TempData["warning"] = "Esteja logado para acessar a lista de agendamentos!";
                return RedirectToAction("Login", "Home");
            }
            var agendamentos = acAgenda.consultaAgendamento();
            return View(agendamentos);
        }

        public ActionResult CadServico()
        {
            if ((Session["usuario"] == null) || (Session["senha"] == null))
            {
                TempData["warning"] = "Esteja logado para cadastrar um serviço!";
                return RedirectToAction("Login", "Home");
            }
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
            if ((Session["usuario"] == null) || (Session["senha"] == null))
            {
                TempData["warning"] = "Esteja logado para alterar um serviço!";
                return RedirectToAction("Login", "Home");
            }
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
            catch
            {
                TempData["error"] = "Ocorreu um erro ao tentar alterar o serviço";
            }
            var servicoAtualizado = acServico.selecionaServicoPorId(servico.codServico);
            ViewBag.desc = servicoAtualizado.descServico;
            ViewBag.imagem = servicoAtualizado.imagemServico;
            return View(servicoAtualizado);
        }

        [HttpPost]
        public ActionResult ExcluiServico(int id)
        {
            try
            {
                acServico.excluiServico(id);
                TempData["success"] = "Exclusão do serviço realizada com sucesso";
            }

            catch
            {
                TempData["error"] = "Ocorreu um erro ao tentar excluir o serviço";
            }
            return RedirectToAction("ListaServicos");
        }

        public JsonResult obterServico(int id)
        {
            var servico = acServico.selecionaServicoPorId(id);
            return Json(servico);
        }

        public ActionResult ConcluiAgendamento(int id)
        {
            try
            {
                TempData["success"] = "Serviço concluído com sucesso!";
                acAgenda.concluiAgendamento(id);
            }

            catch
            {
                TempData["error"] = "Ocorreu um erro ao tentar concluir o serviço";
            }
            return RedirectToAction("ListaAgendamentos", "Gerente");

        }

        public ActionResult CancelaAgendamento(int id)
        {
            try
            {
                acAgenda.cancelaAgendamento(id);
                TempData["success"] = "Serviço cancelado com sucesso";
            }

            catch
            {
                TempData["error"] = "Ocorreu um erro ao tentar cancelar o serviço";
            }
            return RedirectToAction("Login", "Home");
        }

        public ActionResult ListaServicos(int? pagina)
        {
            if ((Session["usuario"] == null) || (Session["senha"] == null))
            {
                TempData["warning"] = "Esteja logado para acessar a lista de serviços!";
                return RedirectToAction("Login", "Home");
            }
            int paginaNumero = (pagina ?? 1);
            var servicos = acServico.consultaServico().OrderBy(p => p.codServico).ToPagedList(paginaNumero, 10);
            return View(servicos);
        }

        public ActionResult ArquivoMortoServico()
        {
            if ((Session["usuario"] == null) || (Session["senha"] == null))
            {
                TempData["warning"] = "Esteja logado para acessar o arquivo morto!";
                return RedirectToAction("Login", "Home");
            }
            var servicos = acServico.consultaLixeira();
            return View(servicos);
        }

        public ActionResult RestauraServico(int id)
        {
            try
            {
                var servico = acServico.selecionaServicoLixeira(id);
                acServico.cadastraServico(servico);
                acServico.excluiServicoLixeira(id);
                TempData["success"] = "Restauração do serviço efetuada com sucesso";
            }

            catch (Exception e)
            {
                TempData["error"] = "Falha ao restaurar serviço: " + e;
            }
            return RedirectToAction("ListaServicos");
        }
    }
}