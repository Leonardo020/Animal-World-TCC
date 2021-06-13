using ProjetoAW.Models;
using ProjetoAW.Repositorio;
using System;
using System.Web.Mvc;

namespace ProjetoAW.Controllers
{
    public class GerenteController : Controller
    {
        actionsFuncionario acFunc = new actionsFuncionario();

        actionsProduto acProd = new actionsProduto();
        public ActionResult HomeGerente()
        {
            return View();
        }

        public ActionResult CadastroFunc(Funcionario func)
        {
            try
            {
                acFunc.cadastraFuncionario(func);
                ViewBag.Message = "Cadastro do funcionário realizado com sucesso!";
            }

            catch (Exception e)
            {
                ViewBag.Message = "Ocorreu um erro ao tentar cadastrar: " + e;
            }
            return View();
        }

        public ActionResult CadastroDesc(Desconto desc)
        {
            try
            {
                acProd.cadastraDesconto(desc);
                ViewBag.Message = "Cadastro de desconto realizado com sucesso!";
            }

            catch (Exception e)
            {
                ViewBag.Message = "Ocorreu um erro ao tentar cadastrar: " + e;
            }
            return View();
        }
    }
}