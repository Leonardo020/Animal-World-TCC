using ProjetoAW.Models;
using ProjetoAW.Repositorio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X.PagedList;

namespace ProjetoAW.Controllers
{
    public class ProdutoController : Controller
    {
        actionsProduto acProd = new actionsProduto();
        actionsAnimal acAnimal = new actionsAnimal();


        public void carregaGroups()
        {
            List<SelectListItem> groups = new List<SelectListItem>();
            groups.Add(new SelectListItem
            {
                Text = "Preço mais alto",
                Value = "valor_unitario desc;"
            });

            groups.Add(new SelectListItem
            {
                Text = "Preço mais baixo",
                Value = "valor_unitario asc;"
            });

            groups.Add(new SelectListItem
            {
                Text = "Novidades",
                Value = "cod_produto desc;"
            });

            ViewBag.groups = new SelectList(groups, "Value", "Text");
        }

        public void carregaFornecedores()
        {
            var fornecedores = acProd.carregaFornecedores();
            ViewBag.fornecedores = new SelectList(fornecedores, "Value", "Text");
        }

        public void carregaEspecies()
        {
            var especies = acAnimal.carregaEspecie();
            ViewBag.especies = new SelectList(especies, "Value", "Text");
        }

        public void carregaCategorias()
        {
            var categorias = acProd.carregaCategorias();
            ViewBag.categorias = new SelectList(categorias, "Value", "Text");
        }

        public ActionResult Vitrine(int? pagina, int id = 0, string searchField = "")
        {
            carregaGroups();
            int paginaNumero = (pagina ?? 1);
            if (id != 0)
            {
                var produtos = acProd.consultaProdutoPorCategoria(id).OrderBy(p => p.codProduto).ToPagedList(paginaNumero, 10);
                if (Session["descontoProd"] != null)
                {
                    foreach (var produto in produtos)
                    {
                        produto.descontoProd = produto.valorUnitario - produto.valorUnitario * Convert.ToDecimal(Session["descontoProd"]);
                    }
                }
                return View(produtos);
            }

            else if (searchField != "")
            {
                var produtos = acProd.consultaProdutoPorNome(searchField).OrderBy(p => p.codProduto).ToPagedList(paginaNumero, 10);
                if (Session["descontoProd"] != null)
                {
                    foreach (var produto in produtos)
                    {
                        produto.descontoProd = produto.valorUnitario - produto.valorUnitario * Convert.ToDecimal(Session["descontoProd"]);
                    }
                }
                return View(produtos);
            }

            else
            {
                var produtos = acProd.consultaProduto().OrderBy(p => p.codProduto).ToPagedList(paginaNumero, 10);
                if (Session["descontoProd"] != null)
                {
                    foreach (var produto in produtos)
                    {
                        produto.descontoProd = produto.valorUnitario - produto.valorUnitario * Convert.ToDecimal(Session["descontoProd"]);
                    }
                }
                return View(produtos);
            }

        }

        [HttpPost]
        public ActionResult Vitrine(FormCollection frm)
        {
            var filtro = frm["filtro"];
            var produtos = acProd.consultaProduto(filtro);
            carregaGroups();
            return View(produtos);
        }

        public ActionResult FiltrarProduto(string filtro)
        {
            var produtoFiltrado = acProd.consultaProduto(filtro);
            return View(produtoFiltrado);
        }

        public ActionResult DetalheProd(/*int id*/)
        {
            var produto = acProd.selecionaProdutoPorId(id);
            return View(produto);
            return View();
        }

        public ActionResult CadastroProd()
        {
            if ((Session["usuario"] == null) || (Session["senha"] == null))
            {
                TempData["warning"] = "Esteja logado para cadastrar um produto!";
                return RedirectToAction("Login", "Home");
            }
            carregaEspecies();
            carregaFornecedores();
            carregaCategorias();
            return View();
        }

        [HttpPost]
        public ActionResult CadastroProd(Produto prod, HttpPostedFileBase file, FormCollection frm)
        {
            carregaEspecies();
            carregaFornecedores();
            carregaCategorias();
            try
            {
                string arquivo = Path.GetFileName(file.FileName);
                string file2 = "/img/" + arquivo;
                string path = Path.Combine(Server.MapPath("~/img"), arquivo);
                file.SaveAs(path);
                prod.imagemProduto = file2;
                prod.descProduto = frm["descProduto"];
                prod.codFornecedor = Convert.ToInt32(Request["fornecedores"]);
                prod.codCategoria = Convert.ToInt32(Request["categorias"]);
                prod.codEspecie = Convert.ToInt32(Request["especies"]);
                acProd.cadastraProduto(prod);

                TempData["success"] = "Cadastro do produto efetuado com sucesso";
            }

            catch (Exception e)
            {
                TempData["error"] = "Falha ao cadastrar produto" + e;
            }

            return View();
        }

        public ActionResult AlterarProd(int id)
        {

            if ((Session["usuario"] == null) || (Session["senha"] == null))
            {
                TempData["warning"] = "Esteja logado para alterar um produto!";
                return RedirectToAction("Login", "Home");
            }
            var produto = acProd.selecionaProdutoPorId(id);
            ViewBag.imagem = produto.imagemProduto;
            return View(produto);
        }

        [HttpPost]
        public ActionResult AlterarProd(Produto prod, HttpPostedFileBase file, FormCollection frm)
        {
            try
            {
                if (file != null)
                {
                    string arquivo = Path.GetFileName(file.FileName);
                    string file2 = "/img/" + arquivo;
                    string path = Path.Combine(Server.MapPath("~/img"), arquivo);
                    file.SaveAs(path);
                    prod.imagemProduto = file2;
                }
                prod.descProduto = frm["descProduto"];
                acProd.atualizaProduto(prod);
                TempData["success"] = "Atualização do produto efetuado com sucesso";
            }

            catch (Exception e)
            {
                TempData["error"] = "Falha ao atualizar produto" + e;
            }
            var produtoAtualizado = acProd.selecionaProdutoPorId(prod.codProduto);
            ViewBag.imagem = prod.imagemProduto;
            return View(produtoAtualizado);
        }

        [HttpPost]
        public JsonResult AtualizaFavorito(int id, bool isFavorite)
        {
            try
            {
                var sessao = Convert.ToInt32(Session["Cliente"]);
                acProd.verificaFavorito(id, sessao, isFavorite);
            }
            catch (Exception e)
            {
                TempData["warning"] = "Falha ao tentar salvar favorito " + e;
            }
            var produto = acProd.selecionaProdutoPorId(id);
            return new JsonResult { Data = produto, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        /*public JsonResult DeletaFavorito()
        {
            try
            {
                var sessao = Convert.ToInt32(Session["Cliente"]);
                acProd.cadastraFavoritoPorCliente(id, sessao, isFavorite);
            }
            catch
            {
                TempData["warning"] = "Falha ao tentar salvar favorito";
            }
            var produto = acProd.selecionaProdutoPorId(id);
            return new JsonResult { Data = produto, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }*/

        [HttpPost]
        public ActionResult ExcluiProd(int id)
        {
            try
            {
                acProd.excluiProduto(id);
                TempData["success"] = "Exclusão do produto efetuada com sucesso";
            }

            catch (Exception e)
            {
                TempData["error"] = "Falha ao excluir produto " + e;
            }
            return RedirectToAction("ListaProd");
        }

        public ActionResult ListaProd(int? pagina)
        {
            int paginaNumero = (pagina ?? 1);
            var produtos = acProd.consultaProduto().OrderBy(a => a.codProduto).ToPagedList(paginaNumero, 10);

            /*if (produtos.Exists(prod => prod.isFavorite == true))
            {

                var sessao = Convert.ToInt32(Session["Cliente"]);
                var produtoFavoritado = acProd.selecionaFavoritos(sessao);
                return View(produtoFavoritado);
            }*/

            /*else
            {
            }*/

            return View(produtos);
        }

        [HttpPost]
        public ActionResult ListaProd(string search)
        {
            var produtos = acProd.consultaProdutoPorNome(search);
            return View(produtos);
        }

        public ActionResult ArquivoMortoProduto()
        {
            var produtos = acProd.consultaLixeira();
            return View(produtos);
        }

        public ActionResult RestauraProduto(int id)
        {
            try
            {
                if ((Session["usuario"] == null) || (Session["senha"] == null))
                {
                    TempData["warning"] = "Esteja logado para restaurar um produto!";
                    return RedirectToAction("Login", "Home");
                }
                var produto = acProd.selecionaProdutoLixeira(id);
                acProd.cadastraProduto(produto);
                acProd.excluiProdutoLixeira(id);
                TempData["success"] = "Restauração do produto efetuada com sucesso";
            }

            catch (Exception e)
            {
                TempData["error"] = "Falha ao restaurar produto: " + e;
            }
            return RedirectToAction("ListaProd");
        }

        public JsonResult obterProduto(int id)
        {
            var produto = acProd.selecionaProdutoPorId(id);
            return Json(produto);
        }

        public JsonResult getProdutos(string search)
        {
            var produtos = acProd.consultaProduto();

            produtos = produtos.Where(x => x.nomeProduto.ToLower().Contains(search)).Select(prod => new Produto
            {
                codProduto = prod.codProduto,
                nomeProduto = prod.nomeProduto
            }).ToList();

            return new JsonResult { Data = produtos, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}