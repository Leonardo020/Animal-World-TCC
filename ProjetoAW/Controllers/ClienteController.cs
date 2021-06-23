using ProjetoAW.Models;
using ProjetoAW.Repositorio;
using System;
using System.Web.Mvc;

namespace ProjetoAW.Controllers
{
    public class ClienteController : Controller
    {
        actionsCliente acCli = new actionsCliente();
        actionsProduto acProd = new actionsProduto();
        actionsAgenda acAgenda = new actionsAgenda();
        public ActionResult DadosPessoais()
        {
            int sessao = Convert.ToInt32(Session["Cliente"]);
            var cliente = acCli.selecionaClientePorId(sessao);
            return View(cliente);
        }

        [HttpPost]
        public ActionResult DadosPessoais(Cliente cli)
        {
            acCli.atualizaCliente(cli);
            ViewBag.Message = "Dados atualizados com sucesso";
            return View();
        }

        public ActionResult CadCliente()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CadCliente(Cliente cli)
        {
            try
            {
                acCli.cadastraCliente(cli);
                TempData["success"] = "Seu cadastro foi efetuado com sucesso! ^^";
            }

            catch (Exception e)
            {
                ViewBag.Message = "Não foi possível te cadastrar pelo seguinte motivo: " + e;
                TempData["error"] = "Ocorreu um erro ao tentar se cadastrar :(";
            }

            return View();
        }

        public ActionResult Favoritos()
        {
            int sessao = Convert.ToInt32(Session["Cliente"]);
            var produtos = acProd.selecionaFavoritos(sessao);
            return View(produtos);
        }

        public ActionResult ListaClientes()
        {
            var clientes = acCli.consultaCliente();
            return View(clientes);
        }

        public ActionResult ListaAgendamento()
        {
            int sessao = Convert.ToInt32(Session["Cliente"]);
            var agendamentos = acAgenda.selecionaAgendamentoPorCliente(sessao);
            return View(agendamentos);
        }
    }
}