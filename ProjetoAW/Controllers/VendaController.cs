using ProjetoAW.Models;
using ProjetoAW.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using X.PagedList;

namespace ProjetoAW.Controllers
{
    public class VendaController : Controller
    {
        actionsEntrega acEntrega = new actionsEntrega();
        actionsPedido acPedido = new actionsPedido();
        actionsVenda acVenda = new actionsVenda();
        actionsProduto acProd = new actionsProduto();
        actionsCliente acCli = new actionsCliente();

        Venda carrinho = new Venda();
        Produto prod = new Produto();
        Pedido itemPedido = new Pedido();
        Entrega entrega = new Entrega();

        public void carregaPagamentos()
        {
            var pagamentos = acVenda.consultaPagamentos();
            ViewBag.pagamentos = new SelectList(pagamentos, "Value", "Text");

        }

        public ActionResult AdicionarItemCarrinho(int id, int qntDet = 0)
        {
            carrinho = Session["Carrinho"] != null ? (Venda)Session["Carrinho"] : new Venda();
            var produto = acProd.selecionaProdutoPorId(id);
            var desconto = acProd.verificaDesconto();

            var qtdTotal = 0;

            if(carrinho.qtdItensVenda > produto.quantidadeEstoque || carrinho.qtdItensVenda < produto.quantidadeEstoque)
            {
                TempData["warning"] = "Quantidade em estoque incompatível com a alterada";
                return RedirectToAction("MeuCarrinho");
            }

            if (produto != null)
            {
                itemPedido.codPedido = Guid.NewGuid();
                itemPedido.codProduto = id;
                itemPedido.produto = produto.nomeProduto;
                if (qntDet != 0)
                {
                    itemPedido.quantidadePedido = qntDet;
                }

                else
                {
                    itemPedido.quantidadePedido = 1;
                }

                if (Session["descontoProd"] != null)
                {
                    itemPedido.valorProduto = produto.valorUnitario - produto.valorUnitario * Convert.ToDecimal(Session["descontoProd"]);

                }
                else
                {
                    itemPedido.valorProduto = produto.valorUnitario;
                }
                itemPedido.imagemProduto = produto.imagemProduto;

                List<Pedido> pedidos = carrinho.itemPedido.FindAll(p => p.codProduto == itemPedido.codProduto);

                if (pedidos.Count != 0)
                {
                    var pedido = carrinho.itemPedido.FirstOrDefault(p => p.codProduto == produto.codProduto);
                    pedido.quantidadePedido += 1;
                    itemPedido.valorProduto *= itemPedido.quantidadePedido;
                    carrinho.valorTotal += itemPedido.valorProduto;
                    pedido.valorTotal = pedido.quantidadePedido * itemPedido.valorProduto;
                    itemPedido.valorTotal = pedido.valorTotal;

                    foreach (var itens in carrinho.itemPedido)
                    {
                        qtdTotal += itens.quantidadePedido;
                    }

                    carrinho.qtdItensVenda = qtdTotal;
                }

                else
                {
                    itemPedido.valorProduto *= itemPedido.quantidadePedido;
                    carrinho.valorTotal += itemPedido.valorProduto;
                    carrinho.itemPedido.Add(itemPedido);
                    itemPedido.valorTotal = itemPedido.valorProduto;

                    foreach (var itens in carrinho.itemPedido)
                    {
                        qtdTotal += itens.quantidadePedido;
                    }

                    carrinho.qtdItensVenda = qtdTotal;
                }

                Session["Carrinho"] = carrinho;
                Session["qtdCarrinho"] = qtdTotal;
            }

            TempData["success"] = "Produto adicionado ao carrinho!";
            return RedirectToAction("MeuCarrinho");
        }

        public ActionResult DeletaItemCarrinho(int id)
        {
            carrinho = Session["Carrinho"] != null ? (Venda)Session["Carrinho"] : new Venda();
            var produto = acProd.selecionaProdutoPorId(id);

            var qtdTotal = 0;

            if (produto != null)
            {
                itemPedido.codPedido = Guid.NewGuid();
                itemPedido.codProduto = id;
                itemPedido.produto = produto.nomeProduto;
                itemPedido.quantidadePedido = 1;
                itemPedido.valorProduto = produto.valorUnitario;
                itemPedido.imagemProduto = produto.imagemProduto;

                List<Pedido> pedidos = carrinho.itemPedido.FindAll(p => p.codProduto == itemPedido.codProduto);

                if (pedidos.Count != 0)
                {
                    var pedido = carrinho.itemPedido.FirstOrDefault(p => p.codProduto == produto.codProduto);

                    pedido.quantidadePedido -= 1;
                    itemPedido.valorProduto *= itemPedido.quantidadePedido;
                    carrinho.valorTotal -= itemPedido.valorProduto;
                    pedido.valorTotal = pedido.quantidadePedido * itemPedido.valorProduto;
                    itemPedido.valorTotal = pedido.valorTotal;

                    foreach (var itens in carrinho.itemPedido)
                    {
                        qtdTotal += itens.quantidadePedido;
                    }

                    carrinho.qtdItensVenda = qtdTotal;
                }

                else
                {
                    ExcluirItemCarrinho(itemPedido.codPedido);
                }

                Session["Carrinho"] = carrinho;
                Session["qtdCarrinho"] = qtdTotal;
            }

            TempData["success"] = "Produto retirado do carrinho";
            return RedirectToAction("MeuCarrinho");
        }

        public ActionResult MeuCarrinho()
        {
            carrinho = Session["Carrinho"] != null ? (Venda)Session["Carrinho"] : new Venda();
            return View(carrinho);
        }

        public ActionResult ExcluirItemCarrinho(Guid id)
        {
            carrinho = Session["Carrinho"] != null ? (Venda)Session["Carrinho"] : new Venda();
            var itemExclusao = carrinho.itemPedido.Find(p => p.codPedido == id);
            carrinho.valorTotal -= itemExclusao.valorProduto;
            carrinho.qtdItensVenda -= itemExclusao.quantidadePedido;
            carrinho.itemPedido.Remove(itemExclusao);
            /*if (carrinho.qtdItensVenda == 1)
            {
            }*/
            Session["Carrinho"] = carrinho;
            Session["qtdCarrinho"] = carrinho.qtdItensVenda;

            return RedirectToAction("MeuCarrinho");
        }

        public ActionResult DadosEntrega(Venda venda)
        {
            if ((Session["usuario"] == null) || (Session["senha"] == null))
            {
                TempData["warning"] = "Esteja logado para acessar os dados da entrega!";
                return RedirectToAction("Login", "Home");
            }
            carrinho = Session["Carrinho"] != null ? (Venda)Session["Carrinho"] : new Venda();
            var sessao = Convert.ToInt32(Session["Cliente"]);
            var cliente = acCli.selecionaClientePorId(sessao);

            carrinho.codCli = cliente.codCli;
            carrinho.nomeCli = cliente.nomeCli;
            carrinho.cpfCli = cliente.cpfCli;
            carrinho.cep = cliente.cepCli;
            carrinho.logradouro = cliente.logradouroCli;
            carrinho.numero = cliente.numeroCli;
            carrinho.bairro = cliente.bairroCli;
            carrinho.estado = cliente.estadoCli;
            carrinho.cidade = cliente.cidadeCli;

            carregaPagamentos();

            return View(carrinho);
        }

        public ActionResult DadosPedido()
        {
            if ((Session["usuario"] == null) || (Session["senha"] == null))
            {
                TempData["warning"] = "Esteja logado para acessar os dados do pedido!";
                return RedirectToAction("Login", "Home");
            }
            var sessao = Convert.ToInt32(Session["Cliente"]);
            var vendas = acVenda.selecionaVendaPorCliente(sessao);
            foreach (var venda in vendas)
            {
                var pedidos = acVenda.selecionaItensPorVenda(venda.codVenda);
                foreach (var itemVenda in pedidos)
                {
                    venda.itemPedido.Add(itemVenda);
                }
            }

            carregaPagamentos();
            return View(vendas);
        }

        public ActionResult CadastrarVenda(Venda venda)
        {
            try
            {
                if ((Session["usuario"] == null) || (Session["senha"] == null))
                {
                    TempData["warning"] = "Esteja logado para encerrar uma compra!";
                    return RedirectToAction("Login", "Home");
                }
                carrinho = Session["Carrinho"] != null ? (Venda)Session["Carrinho"] : new Venda();

                venda.dataVenda = DateTime.Now.ToLocalTime();
                venda.codCli = Convert.ToInt32(Session["Cliente"]);
                venda.codPagamento = Convert.ToInt32(Request["pagamentos"]);
                venda.qtdItensVenda = Convert.ToInt32(Session["qtdCarrinho"]);
                venda.valorTotal = carrinho.valorTotal;

                acVenda.cadastraVenda(venda);

                var idVenda = acVenda.selecionaIdVenda();

                foreach (var item in carrinho.itemPedido)
                {
                    itemPedido.codVenda = idVenda;
                    itemPedido.codProduto = item.codProduto;
                    itemPedido.quantidadePedido = item.quantidadePedido;
                    itemPedido.valorProduto = item.valorProduto;

                    acVenda.cadastraItemVenda(itemPedido);
                }

                entrega.dataEntrega = venda.dataVenda.AddDays(14);
                entrega.codCli = venda.codCli;
                entrega.codPedido = idVenda;

                acEntrega.cadastraEntrega(entrega);

                return RedirectToAction("VendaRealizada", idVenda);

            }

            catch (Exception e)
            {
                TempData["error"] = "Ocorreu um erro ao tentar efetuar a venda: " + e;
                return RedirectToAction("DadosEntrega");
            }
        }

        public ActionResult VendaRealizada(int idVenda)
        {
            acVenda.selecionaEntregaPorVenda(idVenda);
            carrinho.valorTotal = 0;
            carrinho.itemPedido.Clear();
            Session["Carrinho"] = null;
            Session["qtdCarrinho"] = 0;
            return View();
        }

        public ActionResult ListaPedido(int? pagina)
        {
            if ((Session["usuario"] == null) || (Session["senha"] == null))
            {
                TempData["warning"] = "Esteja logado para acessar a lista de pedidos!";
                return RedirectToAction("Login", "Home");
            }

            int paginaNumero = (pagina ?? 1);
            var entregaPedidos = acVenda.consultaEntrega().OrderBy(p => p.codProduto).ToPagedList(paginaNumero, 10);
            return View(entregaPedidos);
        }

        public ActionResult ConcluirVenda(int id)
        {
            try
            {
                acVenda.concluiVenda(id);
                TempData["success"] = "Venda concluída com sucesso";
            }

            catch (Exception e)
            {
                TempData["error"] = "Ocorreu um erro ao tentar concluir a venda: " + e;
            }
            return RedirectToAction("ListaPedido");
        }

        public ActionResult CancelarVenda(int id)
        {
            try
            {
                acVenda.cancelaVenda(id);
                TempData["success"] = "Venda cancelada com sucesso";
            }

            catch (Exception e)
            {
                TempData["error"] = "Ocorreu um erro ao tentar cancelar a venda: " + e;
            }
            return RedirectToAction("ListaPedido");
        }

        public ActionResult ListaItensVenda(int id)
        {
            if ((Session["usuario"] == null) || (Session["senha"] == null))
            {
                TempData["warning"] = "Esteja logado para acessar os itens da venda!";
                return RedirectToAction("Login", "Home");
            }
            var itensVenda = acVenda.selecionaItensPorVenda(id);
            return View(itensVenda);
        }
    }
}