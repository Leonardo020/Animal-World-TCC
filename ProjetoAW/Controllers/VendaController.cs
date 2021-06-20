using ProjetoAW.Models;
using ProjetoAW.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

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

        public void carregaPagamentos()
        {
            var pagamentos = acVenda.consultaPagamentos();
            ViewBag.pagamentos = new SelectList(pagamentos, "Value", "Text");

        }

        public ActionResult AdicionarItemCarrinho(int id)
        {
            carrinho = Session["Carrinho"] != null ? (Venda)Session["Carrinho"] : new Venda();
            var produto = acProd.selecionaProdutoPorId(id);

            var qtdTotal = 0;

            if(produto != null)
            {
                itemPedido.codPedido = Guid.NewGuid();
                itemPedido.codProduto = id;
                itemPedido.produto = produto.nomeProduto;
                itemPedido.quantidadePedido = 1;
                itemPedido.valorProduto = produto.valorUnitario;
                itemPedido.imagemProduto = produto.imagemProduto;

                List<Pedido> pedidos = carrinho.itemPedido.FindAll(p => p.codProduto == itemPedido.codProduto);

                if(pedidos.Count != 0)
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
            Session["Carrinho"] = carrinho;
            Session["qtdCarrinho"] = carrinho.qtdItensVenda;

            return RedirectToAction("MeuCarrinho");
        }

        public ActionResult DadosEntrega()
        {
            carrinho = Session["Carrinho"] != null ? (Venda)Session["Carrinho"] : new Venda();
            var sessao = Convert.ToInt32(Session["Cliente"]);
            var cliente = acCli.selecionaClientePorId(sessao);

            carrinho.codCli = cliente.codCli;
            carrinho.nomeCli = cliente.nomeCli;
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
            carrinho = Session["Carrinho"] != null ? (Venda)Session["Carrinho"] : new Venda();
            carrinho.codCli = Convert.ToInt32(Session["Cliente"]);
            //acVenda.selecionaVendaPorCliente(sessao);
            carregaPagamentos();
            return View();
        }

        public ActionResult CadastrarVenda(Venda venda)
        {
            carrinho = Session["Carrinho"] != null ? (Venda)Session["Carrinho"] : new Venda();

            venda.dataVenda = DateTime.Now.ToLocalTime();
            venda.codCli = Convert.ToInt32(Session["Cliente"]);
            venda.codPagamento = Convert.ToInt32(Request["pagamentos"]);
            venda.qtdItensVenda = Convert.ToInt32(Session["qtdCarrinho"]);
            venda.valorTotal = carrinho.valorTotal;

            acVenda.cadastraVenda(venda);

            venda = acVenda.selecionaIdVenda();

            foreach(var item in carrinho.itemPedido)
            {
                itemPedido.codVenda = venda.codVenda;
                itemPedido.codProduto = item.codProduto;
                itemPedido.quantidadePedido = item.quantidadePedido;
                itemPedido.valorProduto = item.valorProduto;

                acVenda.cadastraItemVenda(itemPedido);

            }

            carrinho.valorTotal = 0;
            carrinho.itemPedido.Clear();

            return RedirectToAction("VendaRealizada");
        }

        public ActionResult VendaRealizada()
        {
            return View();
        }

        /*public ActionResult ListaPedido()
        {
            var pedidos = acPedido.consultaPedido();
            return View(pedidos);
        }*/
    }
}