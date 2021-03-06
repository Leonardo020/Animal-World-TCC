using ProjetoAW.Models;
using ProjetoAW.Repositorio;
using System.Web.Mvc;
using System.Web.Security;

namespace ProjetoAW.Controllers
{
    public class HomeController : Controller
    {
        actionsLogin acLogin = new actionsLogin();
        actionsProduto acProd = new actionsProduto();
        actionsServico acServico = new actionsServico();
        public ActionResult Home()
        {
            var produtos = acProd.consultaProdutoPorRelevancia();
            var desconto = acProd.verificaDesconto();
            if (desconto != null)
            {
                Session["descontoProd"] = desconto.desconto;

                foreach (var produto in produtos)
                {
                    produto.descontoProd = produto.valorUnitario - produto.valorUnitario * desconto.desconto;
                }

            }

            else
            {
                Session["descontoProd"] = null;
            }
            return View(produtos);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login login)
        {
            acLogin.testaLogin(login);

            if (login.usuarioLogin != null && login.senhaLogin != null)
            {
                FormsAuthentication.SetAuthCookie(login.usuarioLogin, false);
                Session["usuario"] = login.usuarioLogin.ToString();
                Session["senha"] = login.senhaLogin.ToString();

                if (login.nivelLogin == 1)
                {
                    acLogin.identificaUsuario(login);
                    Session["Funcionario"] = login.codFunc;
                    Session["nivel1"] = login.nivelLogin;
                }

                else if (login.nivelLogin == 2)
                {
                    acLogin.identificaUsuario(login);
                    Session["Cliente"] = login.codCli;
                    Session["nivel2"] = login.nivelLogin;
                }

                else
                {
                    acLogin.identificaUsuario(login);
                    Session["Gerente"] = login.codGerente;
                    Session["nivel0"] = login.nivelLogin;
                }
                return RedirectToAction("Home");
            }

            else
            {
                TempData["error"] = "Usuário ou senha incorretos :(";
                return View();
            }
        }

        public ActionResult Logout()
        {
            if (Session["usuario"] != null && Session["senha"] != null)
            {
                Session["usuario"] = null;
                Session["senha"] = null;
                Session["nivel0"] = null;
                Session["nivel1"] = null;
                Session["nivel2"] = null;
                Session["Carrinho"] = null;
                Session["qtdCarrinho"] = 0;
            }
            return RedirectToAction("Home");
        }


        public ActionResult Marcas()
        {
            var fornecedores = acProd.consultaFornecedores();
            return View(fornecedores);
        }

        public ActionResult Servicos()
        {
            var servicos = acServico.consultaServico();
            return View(servicos);
        }
    }
}