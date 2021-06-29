using ProjetoAW.Models;
using ProjetoAW.Repositorio;
using System;
using System.Text.RegularExpressions;
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
            cli.cpfCli = Regex.Replace(cli.cpfCli, "[^0-9]", string.Empty);
            cli.telefoneCli = Regex.Replace(cli.telefoneCli, "[^0-9]", string.Empty);
            cli.cepCli = Regex.Replace(cli.cepCli, "[^0-9]", string.Empty);
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
                cli.cpfCli = Regex.Replace(cli.cpfCli, "[^0-9]", string.Empty);
                cli.telefoneCli = Regex.Replace(cli.telefoneCli, "[^0-9]", string.Empty);
                cli.cepCli = Regex.Replace(cli.cepCli, "[^0-9]", string.Empty);
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

        [HttpPost]
        public ActionResult ListaClientes(string search)
        {
            var clientes = acCli.consultaClientePorNome(search);
            return View(clientes);
        }

        public ActionResult ListaAgendamento()
        {
            int sessao = Convert.ToInt32(Session["Cliente"]);
            var agendamentos = acAgenda.selecionaAgendamentoPorCliente(sessao);
            return View(agendamentos);
        }

        public ActionResult AtualizaCliente(int id)
        {
            var cliente = acCli.selecionaClientePorId(id);
            return View(cliente);
        }
    }
}