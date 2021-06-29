using ProjetoAW.Models;
using ProjetoAW.Repositorio;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace ProjetoAW.Controllers
{
    public class GerenteController : Controller
    {
        actionsFuncionario acFunc = new actionsFuncionario();
        actionsAgenda acAgenda = new actionsAgenda();
        actionsProduto acProd = new actionsProduto();
        actionsCliente acCli = new actionsCliente();
        actionsAnimal acAnimal = new actionsAnimal();
        public ActionResult HomeGerente()
        {
            return View();
        }

        public ActionResult CadastroFunc()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CadastroFunc(Funcionario func)
        {
            try
            {
                acFunc.cadastraFuncionario(func);
                TempData["success"] = "Cadastro do funcionário realizado com sucesso!";
            }

            catch
            {
                TempData["error"] = "Ocorreu um erro ao tentar cadastrar o funcionário";
            }
            return View();
        }

        public ActionResult AtualizaFunc(int id)
        {
            var func = acFunc.selecionaFuncionarioPorId(id);
            return View(func);
        }

        [HttpPost]
        public ActionResult AtualizaFunc(Funcionario func)
        {
            try
            {
                acFunc.atualizaFuncionario(func);
                TempData["success"] = "Funcionário atualizado com sucesso!";

            }

            catch (Exception e)
            {
                TempData["error"] = "Ocorreu um erro ao tentar atualizar o funcionário: " + e;
            }
            return View();
        }

        public ActionResult ExcluiFunc(int id)
        {
            try
            {
                acFunc.excluiFuncionario(id);
                TempData["success"] = "Funcionário excluído com sucesso";
            }

            catch (Exception e)
            {
                TempData["error"] = "Ocorreu um erro ao tentar excluir o funcionário: " + e;
            }

            return RedirectToAction("ListaFunc");
        }

        public JsonResult obterFunc(int id)
        {
            var func = acFunc.selecionaFuncionarioPorId(id);
            return Json(func);
        }

        public ActionResult CadastroFornecedor()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CadastroFornecedor(Fornecedor fornecedor, HttpPostedFileBase file)
        {
            try
            {
                if (file != null)
                {
                    string arquivo = Path.GetFileName(file.FileName);
                    string file2 = "/img/" + arquivo;
                    string path = Path.Combine(Server.MapPath("~/img"), arquivo);
                    file.SaveAs(path);
                    fornecedor.imagemFornecedor = file2;
                }
                acProd.cadastraFornecedor(fornecedor);
                TempData["success"] = "Fornecedor cadastrado com sucesso";
            }

            catch (Exception e)
            {
                TempData["error"] = "Ocorreu um erro ao tentar cadastrar o fornecedor: " + e;
            }

            return View();
        }

        public ActionResult CadastroDesc()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CadastroDesc(Desconto desc)
        {
            try
            {
                acProd.cadastraDesconto(desc);
                TempData["success"] = "Cadastro de desconto realizado com sucesso!";
            }

            catch
            {
                TempData["error"] = "Ocorreu um erro ao tentar cadastrar um desconto";
            }
            return View();
        }

        [HttpGet]
        public JsonResult ConsultaDescontos()
        {
            var descontos = acProd.consultaDescontos();
            return new JsonResult { Data = descontos, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult AplicarDesconto(int id)
        {

            var desconto = acProd.selecionaDescontoPorId(id);
            var verificaDesconto = acProd.verificaDesconto();

            if (verificaDesconto != null)
            {
                TempData["warning"] = "Já existe desconto aplicado!";
            }

            else
            {
                acProd.acaoDesconto(desconto);
                TempData["success"] = "Desconto aplicado com sucesso";
                Session["descontoProd"] = desconto.desconto;
            }

            return RedirectToAction("CadastroDesc");
        }

        public ActionResult DesativarDesconto(int id)
        {
            var desconto = acProd.selecionaDescontoPorId(id);
            acProd.acaoDesconto(desconto);
            Session["descontoProd"] = null;
            TempData["success"] = "Desconto desativado com sucesso";

            return RedirectToAction("CadastroDesc");
        }

        public ActionResult ExcluirDesconto(int id)
        {
            acProd.excluiDesconto(id);
            TempData["success"] = "Desconto excluído com sucesso";
            return RedirectToAction("CadastroDesc");
        }

        public ActionResult ListaAgendamentos()
        {
            var agendamentos = acAgenda.consultaAgendamento();
            return View(agendamentos);
        }

        public ActionResult ListaFunc()
        {
            var funcionarios = acFunc.consultaFuncionario();
            return View(funcionarios);
        }

        [HttpPost]
        public ActionResult ListaFunc(string search)
        {
            var funcionarios = acFunc.consultaFuncionarioPorNome(search);
            return View(funcionarios);
        }

        public ActionResult ListaPets(int id)
        {
            var animais = acAnimal.consultaAnimalPorCliente(id);
            return View(animais);

        }

        public ActionResult ArquivoMorto()
        {
            return View();
        }
    }
}