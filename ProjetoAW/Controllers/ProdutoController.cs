using ProjetoAW.Models;
using ProjetoAW.Repositorio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            var fornecedores = acProd.consultaFornecedores();
            ViewBag.fornecedores = new SelectList(fornecedores, "Value", "Text");
        }  
        
        public void carregaEspecies()
        {
            var especies = acAnimal.carregaEspecie();
            ViewBag.especies = new SelectList(especies, "Value", "Text");
        }

        public void carregaCategorias()
        {
            var categorias = acProd.consultaCategorias();
            ViewBag.categorias = new SelectList(categorias, "Value", "Text");
        }

        public ActionResult Vitrine()
        {
            carregaGroups();
            var produtos = acProd.consultaProduto();
            return View(produtos);
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

        public ActionResult DetalheProd(int id)
        {
            var produto = acProd.selecionaProdutoPorId(id);
            return View(produto);
        }

        public ActionResult CadastroProd()
        {
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
                ViewBag.Message = "Cadastro do produto efetuado com sucesso";
            }

            catch (Exception e)
            {
                ViewBag.Message = "Falha ao cadastrar produto: " + e;
            }

            return View();
        }

        public ActionResult AlterarProd(int id)
        {
            var produto = acProd.selecionaProdutoPorId(id);
            return View(produto);
        }

        [HttpPut]
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

                else
                {
                    prod.descProduto = frm["descProduto"];
                }
                acProd.atualizaProduto(prod);
                ViewBag.Message = "Atualização do produto efetuado com sucesso";
            }

            catch (Exception e)
            {
                ViewBag.Message = "Falha ao atualizar produto: " + e;
            }

            return View();
        }

        public ActionResult ExcluirProd()
        {
            return View();
        }

        [HttpDelete]
        public ActionResult ExcluirProd(int id)
        {
            acProd.excluiProduto(id);
            return View();
        }

        public ActionResult ListaProd()
        {
            var produtos = acProd.consultaProduto();
            return View(produtos);
        }

        public JsonResult getProdutos(string search)
        {
            var produtos = acProd.consultaProduto();

            produtos = produtos.Where(x => x.nomeProduto.ToLower().Contains(search)).Select(x => new Produto
            {
                codProduto = x.codProduto,
                nomeProduto = x.nomeProduto
            }).ToList();

            return new JsonResult { Data = produtos, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}